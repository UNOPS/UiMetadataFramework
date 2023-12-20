namespace UiMetadataFramework.Analyzers;

using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using Microsoft.CodeAnalysis.Diagnostics;

internal static class Extensions
{
    public static string GetNamespace(this SyntaxNode property)
    {
        var namespaceName = "";
        var current = property;

        while (current != null)
        {
            if (current is CompilationUnitSyntax compilationUnit)
            {
                // Property is declared at the top level
                namespaceName = "";
                break;
            }
            else if (current is NamespaceDeclarationSyntax namespaceDeclaration)
            {
                namespaceName = namespaceDeclaration.Name.ToString();
                break;
            }
            current = current.Parent;
        }
        return namespaceName;
    }
    public static string GetFullTypeName(this PropertyDeclarationSyntax property)
	{
		var namespaceName = property.GetNamespace();
        //Console.WriteLine(namespaceName);
		var typeName = property.Type.ToString();

		return string.IsNullOrWhiteSpace(namespaceName)
			? typeName
			: $"{namespaceName}.{typeName}";
	}
}