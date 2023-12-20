using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace UiMetadataFramework.Analyzers;

using System;
using System.Reflection;
using UiMetadataFramework.Analyzers.Domain;
using UiMetadataFramework.Core.Binding;

/// <summary>
/// A sample analyzer that reports the company name being used in class declarations.
/// Traverses through the Syntax Tree and checks the name (identifier) of each class node.
/// </summary>

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class SampleSyntaxAnalyzer : DiagnosticAnalyzer
{
	public const string AnalyzerName = "InputFieldBindingAnalyzer";

	// Preferred format of DiagnosticId is Your Prefix + Number, e.g. CA1234.
	public const string DiagnosticId = "IFB001";

	// Feel free to use raw strings if you don't need localization.
	private static readonly LocalizableString Title = new LocalizableResourceString(
		nameof(Resources.IFB001Title),
		Resources.ResourceManager,
		typeof(Resources));

	// The message that will be displayed to the user.
	private static readonly LocalizableString MessageFormat =
		new LocalizableResourceString(
			nameof(Resources.IFB001MessageFormat),
			Resources.ResourceManager,
			typeof(Resources));

	private static readonly LocalizableString Description =
		new LocalizableResourceString(
			nameof(Resources.IFB001Description),
			Resources.ResourceManager,
			typeof(Resources));

	private static readonly DiagnosticDescriptor Rule = new(
		DiagnosticId,
		Title,
		MessageFormat,
		Categories.Usage,
		DiagnosticSeverity.Warning,
		isEnabledByDefault: true,
		description: Description);

	// Keep in mind: you have to list your rules here.
	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
		ImmutableArray.Create(Rule);
	public override void Initialize(AnalysisContext context)
	{
		// You must call this method to avoid analyzing generated code.
		context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
		// You must call this method to enable the Concurrent Execution.
		context.EnableConcurrentExecution();
		// Subscribe to the Syntax Node with the appropriate 'SyntaxKind' (ClassDeclaration) action.
		context.RegisterSyntaxNodeAction(AnalyzeSyntax, SyntaxKind.PropertyDeclaration);
	}
	/// <summary>
	/// Executed for each Syntax Node with 'SyntaxKind' is 'ClassDeclaration'.
	/// </summary>
	/// <param name="context">Operation context.</param>
	private void AnalyzeSyntax(SyntaxNodeAnalysisContext context)
	{
		var propertyDeclaration = (PropertyDeclarationSyntax)context.Node;
		//var semanticModel = context.SemanticModel; 

		//var propertyTypeName = propertyDeclaration.GetFullTypeName(); // "MyApp.TextField"
		//var propertyType = Type.GetType(propertyTypeName); // TextField : Type

		Attribute inputFieldTypeAttribute = null;


        //var namespaceName = GetNamespaceAtDeclaration(propertyDeclaration, semanticModel);
        //Console.WriteLine(namespaceName);
        //var typeName = propertyDeclaration.Type.ToString();
	}

	private static bool ContainsInputFieldTypeAttribute(IParameterSymbol parameter)
	{
		return parameter.Type.AllInterfaces.Any(i => i.Name == "InputFieldTypeAttribute");
	}
}


// Extra

// propertyType?.Attributes
// 	.FirstOrDefault(t => t.AttributeType.Name == "InputFieldTypeAttribute");
//if (inputFieldTypeAttribute != null)
//{
    // if (inputFieldTypeAttribute.MandatoryAttribute != null)
    // {
    // 	var mandatoryAttribute = propertyDeclaration.AttributeLists
    // 		.FirstOrDefault(t => t.ToString() == inputFieldTypeAttribute.MandatoryAttribute.FullName);
    //
    // 	if (mandatoryAttribute == null)
    // 	{
    // 		var diagnostic = Diagnostic.Create(
    // 			Rule,
    // 			propertyDeclaration.GetLocation());
    // 		context.ReportDiagnostic(diagnostic);
    // 	}
    // }
//}

// if (objectType?.Name == "InputFieldBinding") 
// {
// 	var constructor = context.SemanticModel.GetSymbolInfo(propertyDeclaration).Symbol as IMethodSymbol;
// 	if (constructor != null && 
// 		constructor.Parameters.Length == 2 && 
// 		!ContainsInputFieldTypeAttribute(constructor.Parameters[1]))
// 	{
//               var diagnostic = Diagnostic.Create(
// 			Rule,
// 			propertyDeclaration.GetLocation());
//               context.ReportDiagnostic(diagnostic);
//           }
// }