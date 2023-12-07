using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;


public class MandatoryAttributeAnalyzerTests : Microsoft.CodeAnalysis.Testing.Fix
{
    [Fact]
    public void TestMandatoryAttributeMissing()
    {
        const string test = @"
            using System;
            public class TestClass
            {
            }";

        var expected = new DiagnosticResult
        {
            Id = MandatoryAttributeAnalyzer.DiagnosticId,
            Message = "Type must have the mandatory attribute with correct parameters",
            Severity = DiagnosticSeverity.Error,
            Locations = new[] { new DiagnosticResultLocation("Test0.cs", 5, 1) }
        };

        VerifyCSharpDiagnostic(test, expected);
    }
}
