namespace UiMetadataFramework.Tests.Framework.Outputs.Flexbox;

using UiMetadataFramework.Core.Binding;

public class FlexboxAttribute() : CustomPropertyAttribute(PropertyName)
{
	public const string PropertyName = "flexbox";

	public string? Style { get; set; }

	public override object GetValue()
	{
		return new { this.Style };
	}
}