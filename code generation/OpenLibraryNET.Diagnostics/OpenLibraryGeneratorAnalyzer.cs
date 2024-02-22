using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace OpenLibraryNET.Diagnostics
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class OpenLibraryGeneratorAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId_0 = "OLSG0";
        private static readonly LocalizableString Title_0 = "Types decorated with " + typeof(CodeGeneration_Attributes.GenerateEqualsAttribute).FullName + " implements Equals method";
        private static readonly LocalizableString MessageFormat_0 = "Type {0} decorated with " + typeof(CodeGeneration_Attributes.GenerateEqualsAttribute).FullName + " implements Equals({1}) method";
        private const string Category_0 = "Design";
#pragma warning disable RS2008 // Enable analyzer release tracking
        private static readonly DiagnosticDescriptor Rule_0 = new DiagnosticDescriptor(DiagnosticId_0, Title_0, MessageFormat_0, Category_0, DiagnosticSeverity.Error, isEnabledByDefault: true);
#pragma warning restore RS2008 // Enable analyzer release tracking

        public const string DiagnosticId_1 = "OLSG1";
#pragma warning disable RS2008 // Enable analyzer release tracking
        private static readonly DiagnosticDescriptor Rule_1 = new DiagnosticDescriptor(DiagnosticId_1, Title_0, MessageFormat_0, Category_0, DiagnosticSeverity.Error, isEnabledByDefault: true);
#pragma warning restore RS2008 // Enable analyzer release tracking

        public const string DiagnosticId_2 = "OLSG2";
        private static readonly LocalizableString Title_2 = "Types decorated with " + typeof(CodeGeneration_Attributes.GenerateGetHashCodeAttribute).FullName + " implements GetHashCode(void) method";
        private static readonly LocalizableString MessageFormat_2 = "Type {0} decorated with " + typeof(CodeGeneration_Attributes.GenerateGetHashCodeAttribute).FullName + " implements GetHashCode(void) method";
#pragma warning disable RS2008 // Enable analyzer release tracking
        private static readonly DiagnosticDescriptor Rule_2 = new DiagnosticDescriptor(DiagnosticId_2, Title_2, MessageFormat_2, Category_0, DiagnosticSeverity.Error, isEnabledByDefault: true);
#pragma warning restore RS2008 // Enable analyzer release tracking

        public const string DiagnosticId_3 = "OLSG3";
        private static readonly LocalizableString Title_3 = "Types decorated with " + typeof(CodeGeneration_Attributes.CollectionValueEqualityAttribute).FullName + " implements auto-generated method(s)";
        private static readonly LocalizableString MessageFormat_3 = "Type {0} decorated with " + typeof(CodeGeneration_Attributes.CollectionValueEqualityAttribute).FullName + " implements {1} method";
#pragma warning disable RS2008 // Enable analyzer release tracking
        private static readonly DiagnosticDescriptor Rule_3 = new DiagnosticDescriptor(DiagnosticId_3, Title_3, MessageFormat_3, Category_0, DiagnosticSeverity.Error, isEnabledByDefault: true);
#pragma warning restore RS2008 // Enable analyzer release tracking

        public const string DiagnosticId_4 = "OLSG4";
#pragma warning disable RS2008 // Enable analyzer release tracking
        private static readonly DiagnosticDescriptor Rule_4 = new DiagnosticDescriptor(DiagnosticId_4, Title_3, MessageFormat_3, Category_0, DiagnosticSeverity.Error, isEnabledByDefault: true);
#pragma warning restore RS2008 // Enable analyzer release tracking

        public const string DiagnosticId_5 = "OLSG5";
        private static readonly LocalizableString Title_5 = "Types decorated with " + typeof(CodeGeneration_Attributes.GenerateEqualsAttribute).FullName + " must be declared partial";
        private static readonly LocalizableString MessageFormat_5 = "Type {0} decorated with " + typeof(CodeGeneration_Attributes.GenerateEqualsAttribute).FullName + " declared non-partial";
#pragma warning disable RS2008 // Enable analyzer release tracking
        private static readonly DiagnosticDescriptor Rule_5 = new DiagnosticDescriptor(DiagnosticId_5, Title_5, MessageFormat_5, Category_0, DiagnosticSeverity.Error, isEnabledByDefault: true);
#pragma warning restore RS2008 // Enable analyzer release tracking

        public const string DiagnosticId_6 = "OLSG6";
        private static readonly LocalizableString Title_6 = "Types decorated with " + typeof(CodeGeneration_Attributes.GenerateGetHashCodeAttribute).FullName + " must be declared partial";
        private static readonly LocalizableString MessageFormat_6 = "Type {0} decorated with " + typeof(CodeGeneration_Attributes.GenerateGetHashCodeAttribute).FullName + " declared non-partial";
#pragma warning disable RS2008 // Enable analyzer release tracking
        private static readonly DiagnosticDescriptor Rule_6 = new DiagnosticDescriptor(DiagnosticId_6, Title_6, MessageFormat_6, Category_0, DiagnosticSeverity.Error, isEnabledByDefault: true);
#pragma warning restore RS2008 // Enable analyzer release tracking

        public const string DiagnosticId_7 = "OLSG7";
        private static readonly LocalizableString Title_7 = "Types decorated with " + typeof(CodeGeneration_Attributes.CollectionValueEqualityAttribute).FullName + " must be declared partial";
        private static readonly LocalizableString MessageFormat_7 = "Type {0} decorated with " + typeof(CodeGeneration_Attributes.CollectionValueEqualityAttribute).FullName + " declared non-partial";
#pragma warning disable RS2008 // Enable analyzer release tracking
        private static readonly DiagnosticDescriptor Rule_7 = new DiagnosticDescriptor(DiagnosticId_7, Title_7, MessageFormat_7, Category_0, DiagnosticSeverity.Error, isEnabledByDefault: true);
#pragma warning restore RS2008 // Enable analyzer release tracking

        public const string DiagnosticId_8 = "OLSG8";
        private static readonly LocalizableString Title_8 = "Fields and properties decorated with " + typeof(CodeGeneration_Attributes.IgnoreEqualityAttribute).FullName + " should be contained in decorated type";
        private static readonly LocalizableString MessageFormat_8 = "Field {0} decorated with " + typeof(CodeGeneration_Attributes.IgnoreEqualityAttribute).FullName + " is not contained in decorated type";
        private static readonly LocalizableString Description_8 = $@"The {typeof(CodeGeneration_Attributes.IgnoreEqualityAttribute).FullName} has no effect if the containing type is not decorated with at least one of the following attributes:
    {typeof(CodeGeneration_Attributes.GenerateEqualsAttribute).FullName}
    {typeof(CodeGeneration_Attributes.GenerateGetHashCodeAttribute).FullName}
    {typeof(CodeGeneration_Attributes.CollectionValueEqualityAttribute).FullName}";
#pragma warning disable RS2008 // Enable analyzer release tracking
        private static readonly DiagnosticDescriptor Rule_8 = new DiagnosticDescriptor(DiagnosticId_8, Title_8, MessageFormat_8, Category_0, DiagnosticSeverity.Warning, isEnabledByDefault: true, Description_8);
#pragma warning restore RS2008 // Enable analyzer release tracking

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule_0, Rule_1, Rule_2, Rule_3, Rule_4, Rule_5, Rule_6, Rule_7, Rule_8); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxNodeAction(AnalyzeGenerateEquals, SyntaxKind.MethodDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeGenerateGetHashCode, SyntaxKind.MethodDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeCollectionValueEquality, SyntaxKind.MethodDeclaration);

            context.RegisterSyntaxNodeAction(AnalyzeGenerateEquals_Partial, SyntaxKind.ClassDeclaration, SyntaxKind.RecordDeclaration, SyntaxKind.StructDeclaration, SyntaxKind.RecordStructDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeGenerateGetHashCode_Partial, SyntaxKind.ClassDeclaration, SyntaxKind.RecordDeclaration, SyntaxKind.StructDeclaration, SyntaxKind.RecordStructDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeCollectionValueEquality_Partial, SyntaxKind.ClassDeclaration, SyntaxKind.RecordDeclaration, SyntaxKind.StructDeclaration, SyntaxKind.RecordStructDeclaration);

            context.RegisterSymbolAction(AnalyzeIgnoreEquality, SymbolKind.Field, SymbolKind.Property);
        }

        private static void AnalyzeIgnoreEquality(SymbolAnalysisContext context)
        {
            var symbol = context.Symbol;

            if (!symbol.GetAttributes().Any(a => a.AttributeClass?.ToDisplayString() == typeof(CodeGeneration_Attributes.IgnoreEqualityAttribute).FullName))
            {
                return;
            }

            var containing = symbol.ContainingType;
            if (containing == null)
            {
                return;
            }

            string[] names = new string[3] { typeof(CodeGeneration_Attributes.GenerateEqualsAttribute).FullName, typeof(CodeGeneration_Attributes.GenerateGetHashCodeAttribute).FullName, typeof(CodeGeneration_Attributes.CollectionValueEqualityAttribute).FullName };
            if (!containing.GetAttributes().Select(attr => attr.AttributeClass?.ToDisplayString()).Any(str => names.Contains(str)))
            {
                var diagnostic = Diagnostic.Create(Rule_8, symbol.Locations[0], symbol.Name);
                context.ReportDiagnostic(diagnostic);
            }
        }

        private static void AnalyzeGenerateEquals_Partial(SyntaxNodeAnalysisContext context)
        {
            var typeDeclaration = context.Node as TypeDeclarationSyntax;
            INamedTypeSymbol symbol = context.SemanticModel.GetDeclaredSymbol(typeDeclaration);

            if (!symbol.GetAttributes().Any(a => a.AttributeClass?.ToDisplayString() == typeof(CodeGeneration_Attributes.GenerateEqualsAttribute).FullName))
            {
                return;
            }

            if (typeDeclaration.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.PartialKeyword)))
            {
                return;
            }

            var diagnostic = Diagnostic.Create(Rule_5, typeDeclaration.GetLocation(), symbol.Name);
            context.ReportDiagnostic(diagnostic);
        }

        private static void AnalyzeGenerateGetHashCode_Partial(SyntaxNodeAnalysisContext context)
        {
            var typeDeclaration = context.Node as TypeDeclarationSyntax;
            INamedTypeSymbol symbol = context.SemanticModel.GetDeclaredSymbol(typeDeclaration);

            if (!symbol.GetAttributes().Any(a => a.AttributeClass?.ToDisplayString() == typeof(CodeGeneration_Attributes.GenerateGetHashCodeAttribute).FullName))
            {
                return;
            }

            if (typeDeclaration.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.PartialKeyword)))
            {
                return;
            }

            var diagnostic = Diagnostic.Create(Rule_6, typeDeclaration.GetLocation(), symbol.Name);
            context.ReportDiagnostic(diagnostic);
        }

        private static void AnalyzeCollectionValueEquality_Partial(SyntaxNodeAnalysisContext context)
        {
            var typeDeclaration = context.Node as TypeDeclarationSyntax;
            INamedTypeSymbol symbol = context.SemanticModel.GetDeclaredSymbol(typeDeclaration);

            if (!symbol.GetAttributes().Any(a => a.AttributeClass?.ToDisplayString() == typeof(CodeGeneration_Attributes.CollectionValueEqualityAttribute).FullName))
            {
                return;
            }

            if (typeDeclaration.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.PartialKeyword)))
            {
                return;
            }

            var diagnostic = Diagnostic.Create(Rule_7, typeDeclaration.GetLocation(), symbol.Name);
            context.ReportDiagnostic(diagnostic);
        }

        private static void AnalyzeGenerateEquals(SyntaxNodeAnalysisContext context)
        {
            var methodDeclaration = (MethodDeclarationSyntax)context.Node;
            var method = context.SemanticModel.GetDeclaredSymbol(methodDeclaration);

            if (!method.Name.Equals("Equals"))
            {
                return;
            }

            if (method.Parameters.Length > 1)
            {
                return;
            }

            INamedTypeSymbol symbol = method.ContainingType;
            if (symbol == null)
            {
                return;
            }

            if (!symbol.GetAttributes().Any(a => a.AttributeClass.ToDisplayString() == typeof(CodeGeneration_Attributes.GenerateEqualsAttribute).FullName))
            {
                return;
            }

            if (SymbolEqualityComparer.Default.Equals(method.Parameters[0].Type, symbol))
            {
                var diagnostic = Diagnostic.Create(Rule_0, methodDeclaration.GetLocation(), symbol.Name, symbol.Name);
                context.ReportDiagnostic(diagnostic);
                return;
            }

            if (SymbolEqualityComparer.Default.Equals(symbol, context.SemanticModel.Compilation.GetSpecialType(SpecialType.System_Object)))
            {
                var diagnostic = Diagnostic.Create(Rule_1, methodDeclaration.GetLocation(), symbol.Name, "object");
                context.ReportDiagnostic(diagnostic);
            }
        }

        private static void AnalyzeGenerateGetHashCode(SyntaxNodeAnalysisContext context)
        {
            var methodDeclaration = (MethodDeclarationSyntax)context.Node;
            var method = context.SemanticModel.GetDeclaredSymbol(methodDeclaration);

            if (!method.Name.Equals("GetHashCode"))
            {
                return;
            }

            if (method.Parameters.Length != 0)
            {
                return;
            }

            INamedTypeSymbol symbol = method.ContainingType;
            if (symbol == null)
            {
                return;
            }

            if (!symbol.GetAttributes().Any(a => a.AttributeClass.ToDisplayString() == typeof(CodeGeneration_Attributes.GenerateGetHashCodeAttribute).FullName))
            {
                return;
            }

            var diagnostic = Diagnostic.Create(Rule_2, methodDeclaration.GetLocation(), symbol.Name);
            context.ReportDiagnostic(diagnostic);
        }

        private static void AnalyzeCollectionValueEquality(SyntaxNodeAnalysisContext context)
        {
            var methodDeclaration = (MethodDeclarationSyntax)context.Node;
            var method = context.SemanticModel.GetDeclaredSymbol(methodDeclaration);

            INamedTypeSymbol symbol = method.ContainingType;
            if (symbol == null)
            {
                return;
            }

            CheckGetHashCode();
            CheckEquals();

            void CheckGetHashCode()
            {
                if (!method.Name.Equals("GetHashCode"))
                {
                    return;
                }

                if (method.Parameters.Length != 0)
                {
                    return;
                }

                if (!symbol.GetAttributes().Any(a => a.AttributeClass.ToDisplayString() == typeof(CodeGeneration_Attributes.CollectionValueEqualityAttribute).FullName))
                {
                    return;
                }

                var diagnostic = Diagnostic.Create(Rule_3, methodDeclaration.GetLocation(), symbol.Name, method.ToDisplayString());
                context.ReportDiagnostic(diagnostic);
            }

            void CheckEquals()
            {
                if (!method.Name.Equals("Equals"))
                {
                    return;
                }

                if (method.Parameters.Length > 1)
                {
                    return;
                }

                if (!symbol.GetAttributes().Any(a => a.AttributeClass.ToDisplayString() == typeof(CodeGeneration_Attributes.CollectionValueEqualityAttribute).FullName))
                {
                    return;
                }

                if (SymbolEqualityComparer.Default.Equals(method.Parameters[0].Type, symbol))
                {
                    var diagnostic = Diagnostic.Create(Rule_4, methodDeclaration.GetLocation(), symbol.Name, method.ToDisplayString());
                    context.ReportDiagnostic(diagnostic);
                }

                if (SymbolEqualityComparer.Default.Equals(symbol, context.SemanticModel.Compilation.GetSpecialType(SpecialType.System_Object)))
                {
                    var diagnostic = Diagnostic.Create(Rule_4, methodDeclaration.GetLocation(), symbol.Name, method.ToDisplayString());
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
