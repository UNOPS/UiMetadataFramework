using System.Threading.Tasks;
using Xunit;
using Verifier =
	Microsoft.CodeAnalysis.CSharp.Testing.XUnit.AnalyzerVerifier<
		UiMetadataFramework.Analyzers.SampleSemanticAnalyzer>;

namespace UiMetadataFramework.Analyzers.Tests;

public class SampleSemanticAnalyzerTests
{
	[Fact]
	public async Task SetSpeedHugeSpeedSpecified_AlertDiagnostic()
	{
		const string text = @"
using UiMetadataFramework.Core.Binding;

public class Program
{
    public void Main()
    {
    }

    public class Request
    {
        public TextField FirstName { get; set; }
    }
    
    [InputFieldType(""text-field"", mandatoryAttribute: typeof(TextFieldAttribute))]
    public class TextField
    {
        public string Value { get; set; }
    }

    public class TextFieldAttribute : InputFieldAttribute
    {
    }
}
";

		var expected = Verifier.Diagnostic()
			.WithLocation(12, 8)
			.WithArguments("300000000");
		
		await Verifier.VerifyAnalyzerAsync(text, expected);
	}
}