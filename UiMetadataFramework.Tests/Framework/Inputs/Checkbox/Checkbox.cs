namespace UiMetadataFramework.Tests.Framework.Inputs.Checkbox;

using UiMetadataFramework.Core.Binding;

[InputFieldType("checkbox", typeof(CheckboxAttribute))]
public class Checkbox
{
	public bool Value { get; set; }
}