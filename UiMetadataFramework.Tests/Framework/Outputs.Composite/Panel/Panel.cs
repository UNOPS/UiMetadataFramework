namespace UiMetadataFramework.Tests.Framework.Outputs.Composite.Panel;

using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.ComplexOutput;

public abstract class Panel : ComplexOutput
{
	[OutputField]
	public string? Heading { get; set; }
}