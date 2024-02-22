using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeGeneration;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;


[Generator]
public class EqualsGenerator : ISourceGenerator
{
#pragma warning disable RS2008 // Enable analyzer release tracking
    private static readonly LocalizableString Title_0 = "Types decorated with " + typeof(CodeGeneration_Attributes.GenerateEqualsAttribute).FullName + " must not be partial";
    private static readonly LocalizableString MessageFormat_0 = "Type {0} decorated with " + typeof(CodeGeneration_Attributes.GenerateEqualsAttribute).FullName + " is declared non-partial";

    public static readonly DiagnosticDescriptor error = new DiagnosticDescriptor(id: "EG0",
                                                                                 Title_0,
                                                                                 MessageFormat_0,
                                                                                 category: "Design",
                                                                                 DiagnosticSeverity.Error,
                                                                                 isEnabledByDefault: true);
#pragma warning restore RS2008 // Enable analyzer release tracking

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new AttributeSyntaxReceiver("GenerateEquals", "CollectionValueEquality"));
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
            ISymbol ienumerableSymbol = context.Compilation.GetSpecialType(SpecialType.System_Collections_IEnumerable);
            ISymbol stringSymbol = context.Compilation.GetSpecialType(SpecialType.System_String);
            string partialSource = Utility.BuildSource(symbol, GetContents(symbol, ienumerableSymbol, stringSymbol));
            context.AddSource($"{symbol.Name}.g.cs", SourceText.From(partialSource, Encoding.UTF8));
        }
    }

    private string GetContents(INamedTypeSymbol symbol, ISymbol iEnumerableSymbol, ISymbol stringSymbol)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("///<summary>\n///Determines whether the specified object is equal to the current object.\n///Compares all fields by value; IEnumerables are compared by comparing the elements pairwise.\n///</summary>\n///<param name=\"other\">The object to compare with the current object.</param>\n///<returns>true if the specified object is equal to the current object; otherwise, false.</returns>\n");
        if (symbol.TypeKind == TypeKind.Struct)
        {
            sb.Append($"[System.CodeDom.Compiler.GeneratedCode(\"{Utility.ToolName}\", \"{Utility.ToolVersion}\")]\npublic bool Equals({symbol.Name} other)\n{{");
        }
        else
        {
            sb.Append($"[System.CodeDom.Compiler.GeneratedCode(\"{Utility.ToolName}\", \"{Utility.ToolVersion}\")]\npublic bool Equals({symbol.Name} other)\n{{");
        }

        List<IFieldSymbol> members = symbol.GetMembers().OfType<IFieldSymbol>().ToList();

        if (members.Count == 0)
        {
            if (symbol.TypeKind == TypeKind.Struct)
            {
                sb.Append($"\n\treturn true");
            }
            else
            {
                sb.Append($"\n\treturn other != null");
            }
        }

        bool first = true;
        for (int i = 0; i < members.Count; i++)
        {
            ISymbol assoc = members[i].AssociatedSymbol ?? members[i];

            if (assoc.GetAttributes().Any(attr => attr.AttributeClass.ToDisplayString() == typeof(CodeGeneration_Attributes.IgnoreEqualityAttribute).FullName))
                continue;

            if (first)
            {
                first = false;

                if (symbol.TypeKind == TypeKind.Struct)
                {
                    sb.Append($"\n\treturn");
                }
                else
                {
                    sb.Append($"\n\treturn other != null &&");
                }
            }
            else
            {
                sb.Append(" &&");
            }

            if (!SymbolEqualityComparer.Default.Equals(members[i].Type, stringSymbol)
                && members[i].Type.AllInterfaces.Any(sym => SymbolEqualityComparer.Default.Equals(sym, iEnumerableSymbol)))
            {
                sb.Append($"\n\t(({assoc.Name} == null && other.{assoc.Name} == null) || ({assoc.Name} != null && other.{assoc.Name} != null && {assoc.Name}.SequenceEqual(other.{assoc.Name})))");
            }
            else
            {
                sb.Append($"\n\t{assoc.Name} == other.{assoc.Name}");
            }
        }

        sb.Append(";\n}");


        // TODO
        // According to what i find online and some quick tests i did,
        // record types automatically use the Equals() method within == and != operators;
        // No overload necessary?

//        sb.Append($@"

/////<summary>
/////Determines whether two specified objects have the same value.
/////Compares all fields by value; IEnumerables are compared by comparing the elements pairwise.
/////</summary>
/////<param name=""a"">The first object to compare, or null.</param>
/////<param name=""b"">The second object to compare, or null.</param>
/////<returns>true if the value of a is the same as the value of b; otherwise false.</returns>
//[System.CodeDom.Compiler.GeneratedCode(""{Utility.ToolName}"", ""{Utility.ToolVersion}"")]
//public static bool operator ==({symbol.Name} a, {symbol.Name} b)
//{{");

//        if (symbol.TypeKind == TypeKind.Struct)
//        {
//            sb.Append($@"
//    return a.Equals(b);
//}}");
//        }
//        else
//        {
//            sb.Append($@"
//    if (a == null)
//    {{
//        return b == null;
//    }}
//    return a.Equals(b);
//}}");
//        }


//        sb.Append($@"

/////<summary>
/////Determines whether two specified objects have different values.
/////Compares all fields by value; IEnumerables are compared by comparing the elements pairwise.
/////</summary>
/////<param name=""a"">The first object to compare, or null.</param>
/////<param name=""b"">The second object to compare, or null.</param>
/////<returns>true if the value of a is different from the value of b; otherwise false.</returns>
//[System.CodeDom.Compiler.GeneratedCode(""{Utility.ToolName}"", ""{Utility.ToolVersion}"")]
//public static bool operator !=({symbol.Name} a, {symbol.Name} b)
//{{");

//        if (symbol.TypeKind == TypeKind.Struct)
//        {
//            sb.Append($@"
//    return !a.Equals(b);
//}}");
//        }
//        else
//        {
//            sb.Append($@"
//    if (a == null)
//    {{
//        return b != null;
//    }}
//    return !a.Equals(b);
//}}");
//        }


        return sb.ToString();
    }

    public static List<string> GetPropertyAndFieldNames(TypeDeclarationSyntax declarationSyntax)
    {
        List<string> memberNames = new List<string>();
        memberNames.AddRange(declarationSyntax.Members.OfType<PropertyDeclarationSyntax>().Select(p => p.Identifier.Text));
        memberNames.AddRange(declarationSyntax.Members.OfType<FieldDeclarationSyntax>().SelectMany(p => p.Declaration.Variables.Select(v => v.Identifier.Text)));
        return memberNames;
    }

}