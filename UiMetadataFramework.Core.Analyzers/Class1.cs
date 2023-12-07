using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class MandatoryAttributeAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "MandatoryAttribute";
    internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, "Mandatory attribute is missing or parameters are not set correctly", "Type must have the mandatory attribute with correct parameters", "Usage", DiagnosticSeverity.Error, isEnabledByDefault: true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

    public override void Initialize(AnalysisContext context)
    {
        context.RegisterSyntaxNodeAction(AnalyzeSyntax, SyntaxKind.ClassDeclaration);
    }

    private static void AnalyzeSyntax(SyntaxNodeAnalysisContext context)
    {
        var classDeclaration = (ClassDeclarationSyntax)context.Node;

        // Check if the class has the mandatory attribute
        var mandatoryAttribute = GetMandatoryAttribute(classDeclaration.AttributeLists);

        if (mandatoryAttribute == null)
        {
            // Report a diagnostic if the mandatory attribute is missing
            var diagnostic = Diagnostic.Create(Rule, classDeclaration.GetLocation(), "MandatoryAttribute");
            context.ReportDiagnostic(diagnostic);
        }
        else
        {
            // Check if the parameters are set correctly
            if (!AreParametersValid(mandatoryAttribute))
            {
                // Report a diagnostic if the parameters are not set correctly
                var diagnostic = Diagnostic.Create(Rule, mandatoryAttribute.GetLocation(), "MandatoryAttribute");
                context.ReportDiagnostic(diagnostic);
            }
        }
    }

    private static AttributeSyntax GetMandatoryAttribute(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return attributeLists.SelectMany(attributeList => attributeList.Attributes)
                             .FirstOrDefault(attr => attr.Name.ToString() == "MandatoryAttribute");
    }

    private static bool AreParametersValid(AttributeSyntax attribute)
    {
        // Check if the attribute has parameters and validate them accordingly
        // Replace the following condition with your specific parameter validation logic
        return attribute.ArgumentList != null && attribute.ArgumentList.Arguments.Count > 0;
    }
}
