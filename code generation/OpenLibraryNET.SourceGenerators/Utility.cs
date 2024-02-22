using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeGeneration
{
    public static class Utility
    {
        public const string ToolName = "OpenLibraryNET.SourceGenerator";
        public const string ToolVersion = "1.0.0.0";

        public static string GetDeclaration(INamedTypeSymbol symbol)
        {
            SymbolDisplayFormat format = new SymbolDisplayFormat(
    typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameOnly,
    genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters | SymbolDisplayGenericsOptions.IncludeVariance | SymbolDisplayGenericsOptions.IncludeTypeConstraints,
    kindOptions: SymbolDisplayKindOptions.IncludeTypeKeyword | SymbolDisplayKindOptions.IncludeNamespaceKeyword,
    miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes
    );

            string declaration = "";

            if (symbol.DeclaredAccessibility == Accessibility.Public)
            {
                declaration += "public ";
            }
            else if (symbol.DeclaredAccessibility == Accessibility.Private)
            {
                declaration += "private ";
            }
            else if (symbol.DeclaredAccessibility == Accessibility.Internal)
            {
                declaration += "internal ";
            }
            else if (symbol.DeclaredAccessibility == Accessibility.Protected)
            {
                declaration += "protected ";
            }

            if (symbol.IsStatic)
            {
                declaration += "static ";
            }
            else
            {
                if (symbol.IsAbstract)
                {
                    declaration += "abstract ";
                }

                if (symbol.IsSealed)
                {
                    declaration += "sealed ";
                }
            }

            declaration += "partial " + symbol.ToDisplayString(format);
            return declaration;
        }

        public static List<string> GetPropertyAndFieldNames(TypeDeclarationSyntax declarationSyntax)
        {
            List<string> memberNames = new List<string>();
            memberNames.AddRange(declarationSyntax.Members.OfType<PropertyDeclarationSyntax>().Select(p => p.Identifier.Text));
            memberNames.AddRange(declarationSyntax.Members.OfType<FieldDeclarationSyntax>().SelectMany(p => p.Declaration.Variables.Select(v => v.Identifier.Text)));
            return memberNames;
        }

        public static string BuildSource(INamedTypeSymbol symbol, string contents)
        {
            StringBuilder sb = new StringBuilder();

            int indent = 0;

            if (symbol.ContainingNamespace != null)
            {
                sb.AppendLine($"namespace {symbol.ContainingNamespace.ToDisplayString()}\n{{");
                indent++;
            }

            if (symbol.ContainingType != null)
            {
                Stack<INamedTypeSymbol> containingTypes = new Stack<INamedTypeSymbol>();
                INamedTypeSymbol tmp = symbol;
                while (tmp.ContainingType != null)
                {
                    containingTypes.Push(tmp.ContainingType);
                    tmp = tmp.ContainingType;
                }

                foreach (var currentSymbol in containingTypes)
                {
                    sb.AppendIndented($"\n{GetDeclaration(currentSymbol)}\n{{", indent);
                    indent++;
                }
            }

            sb.AppendIndented($"\n{GetDeclaration(symbol)}\n{{", indent);
            indent++;
            sb.AppendIndented(contents, indent);
            indent--;

            while (indent >= 0)
            {
                sb.AppendIndented("\n}", indent);
                indent--;
            }

            return sb.ToString();
        }

        public static StringBuilder AppendIndented(this StringBuilder sb, string textBlock, int indentationLevel)
        {
            char[] chars = new char[1] { '\n' };
            foreach (var line in textBlock.TrimEnd().Split(chars, StringSplitOptions.RemoveEmptyEntries))
                if (!string.IsNullOrWhiteSpace(line))
                    sb.AppendLine($"{string.Concat(Enumerable.Repeat("\t", indentationLevel))}{line}");
            return sb;
        }

        //public static bool IsAutoProperty(this IPropertySymbol property)
        //{
        //    property.BackingField
        //    return property.GetMethod != null &&
        //           property.GetMethod.DeclaredAccessibility == Accessibility.Public &&
        //           property.GetMethod.GetBody() == null;
        //}
    }
}
