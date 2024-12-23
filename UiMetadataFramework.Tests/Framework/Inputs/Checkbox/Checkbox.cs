namespace UiMetadataFramework.Tests.Framework.Inputs.Checkbox;

using UiMetadataFramework.Basic.Inputs;
using UiMetadataFramework.Core.Binding;

[MyInputComponent("checkbox")]
[HasConfiguration(typeof(CheckboxAttribute), mandatory: true)]
public class Checkbox
{
	public bool Value { get; set; }
}