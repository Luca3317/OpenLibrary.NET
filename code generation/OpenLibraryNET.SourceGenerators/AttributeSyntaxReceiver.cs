using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration
{
    public class AttributeSyntaxReceiver : ISyntaxReceiver
    {
        public List<TypeDeclarationSyntax> TypeDeclarations { get; private set; }

        private readonly string[] attributeName;

        public string lastfail;

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            TypeDeclarationSyntax typeDecl;
            if ((typeDecl = syntaxNode as TypeDeclarationSyntax) == null || syntaxNode is InterfaceDeclarationSyntax)
                return;

            if (typeDecl.AttributeLists.Count == 0)
                return;

            foreach (var a in typeDecl.AttributeLists)
            {
                foreach (var b in a.Attributes)
                {
                    if (!(b.Name + "").Equals("AttributeUsage"))
                        lastfail = b.Name.ToFullString();
                }
            }
            if (!typeDecl.AttributeLists.Any(a => a.Attributes.Any(b => attributeName.Contains(b.Name.ToString()))))
                return;

            TypeDeclarations.Add(typeDecl);
        }

        public AttributeSyntaxReceiver(params string[] attributeName)
        {
            TypeDeclarations = new List<TypeDeclarationSyntax>();
            this.attributeName = attributeName;
        }
    }
}
