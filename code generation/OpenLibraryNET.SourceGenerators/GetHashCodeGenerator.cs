using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeGeneration;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;


[Generator]
public class GetHashCodeGenerator : ISourceGenerator
{
#pragma warning disable RS2008 // Enable analyzer release tracking
    private static readonly LocalizableString Title_0 = "Types decorated with " + typeof(CodeGeneration_Attributes.GenerateEqualsAttribute).FullName + " must not be partial";
    private static readonly LocalizableString MessageFormat_0 = "Type {0} decorated with " + typeof(CodeGeneration_Attributes.GenerateEqualsAttribute).FullName + " is declared non-partial";

    public static readonly DiagnosticDescriptor error = new DiagnosticDescriptor(id: "GHCG0",
                                                                                 Title_0,
                                                                                 MessageFormat_0,
                                                                                 category: "Design",
                                                                                 DiagnosticSeverity.Error,
                                                                                 isEnabledByDefault: true);
#pragma warning restore RS2008 // Enable analyzer release tracking

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new AttributeSyntaxReceiver("GenerateGetHashCode", "CollectionValueEquality"));
    }

    public void Execute(GeneratorExecutionContext context)
    {
        AttributeSyntaxReceiver receiver = context.SyntaxReceiver as AttributeSyntaxReceiver;

        if (receiver == null) return;

        foreach (var typeDecl in receiver.TypeDeclarations)
        {
            // Get semantic model to allow for more strictly checking attribute
            var model = context.Compilation.GetSemanticModel(typeDecl.SyntaxTree);
            var symbol = model.GetDeclaredSymbol(typeDecl);

            // Check whether attribute actually is the correct attribute
            bool isDecorated = false;
            foreach (var attributeData in symbol.GetAttributes())
            {
                var attClass = attributeData.AttributeClass;

                if (attClass.ToDisplayString() == typeof(CodeGeneration_Attributes.GenerateGetHashCodeAttribute).FullName
                    || attClass.ToDisplayString() == typeof(CodeGeneration_Attributes.CollectionValueEqualityAttribute).FullName)
                {
                    isDecorated = true;
                    break;
                }
            }

            if (!isDecorated) continue;

            // Check whether the type is declared partial; if not, error => attribute effectively only allowed on partial type declarations
            // Could theoretically move to SyntaxReceiver; wont be able to throw an error though (there might be false positives)
            if (!typeDecl.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword)))
            {
                context.ReportDiagnostic(Diagnostic.Create(error, typeDecl.GetLocation(), symbol.Name + " is not declared partial!"));
            }


            // Create the source
            string partialSource = Utility.BuildSource(symbol, GetContents(context, symbol));
            context.AddSource($"{symbol.Name}.g.cs", SourceText.From(partialSource, Encoding.UTF8));
        }
    }

    private string GetContents(GeneratorExecutionContext context, INamedTypeSymbol symbol)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("///<summary>\n///Serves as the default hash function.\n///IEnumerables are hashed element-wise.\n///</summary>\n///<returns>A hash code for the current object.</returns>\n");
        sb.Append($"[System.CodeDom.Compiler.GeneratedCode(\"{Utility.ToolName}\", \"{Utility.ToolVersion}\")]\npublic override int GetHashCode()\n{{\n\tHashCode hash = new HashCode();");

        ISymbol iEnumerableSymbol = context.Compilation.GetSpecialType(SpecialType.System_Collections_IEnumerable);
        ISymbol stringSymbol = context.Compilation.GetSpecialType(SpecialType.System_String);

        List<IFieldSymbol> members = symbol.GetMembers().OfType<IFieldSymbol>().ToList();
        for (int i = 0; i < members.Count; i++)
        {
            ISymbol assoc = members[i].AssociatedSymbol ?? members[i];

            if (assoc.GetAttributes().Any(attr => attr.AttributeClass.ToDisplayString() == typeof(CodeGeneration_Attributes.IgnoreEqualityAttribute).FullName))
                continue;

            if (!SymbolEqualityComparer.Default.Equals(members[i].Type, stringSymbol)
                && members[i].Type.AllInterfaces.Any(sym => SymbolEqualityComparer.Default.Equals(sym, iEnumerableSymbol)))
            {
                sb.Append($"\n\tif ({assoc.Name} != null)\n\t{{\n\t\tforeach (var element in {assoc.Name})\n\t\t{{\n\t\t\thash.Add(element);\n\t\t}}\n\t}}\n\telse\n\t{{\n\t\thash.Add({assoc.Name});\n\t}}");
            }
            else
            {
                sb.Append($"\n\thash.Add(this.{assoc.Name});");
            }
        }

        sb.Append("\n\treturn hash.ToHashCode();\n}");
        return sb.ToString();
    }
}