namespace UiMetadataFramework.Tests.Framework.Inputs.Checkbox;

using UiMetadataFramework.Core.Binding;

[InputComponent("checkbox")]
[HasConfiguration(typeof(CheckboxAttribute), mandatory: true)]
public class Checkbox
{
	public bool Value { get; set; }
}