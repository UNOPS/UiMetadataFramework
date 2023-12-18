namespace UiMetadataFramework.Analyzers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class Extensions
{
	public static string GetNamespace(this SyntaxNode property)
	{
		var namespaceName = "";

		var parent = property.Parent;

		while (parent != null)
		{
			if (parent is NamespaceDeclarationSyntax namespaceDeclaration)
			{
				namespaceName = namespaceDeclaration.Name.ToString();
				break;
			}

			parent = parent.Parent;
		}

		return namespaceName;
	}

	public static string GetFullTypeName(this PropertyDeclarationSyntax property)
	{
		var namespaceName = property.GetNamespace();
		var typeName = property.Type.ToString();

		return string.IsNullOrWhiteSpace(namespaceName)
			? typeName
			: $"{namespaceName}.{typeName}";
	}
}