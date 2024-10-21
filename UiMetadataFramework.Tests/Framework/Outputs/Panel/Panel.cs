namespace UiMetadataFramework.Tests.Framework.Outputs.Panel;

using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.ComplexOutput;

public abstract class Panel : ComplexOutput
{
	[OutputField]
	public string? Heading { get; set; }
}