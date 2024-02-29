namespace UiMetadataFramework.Tests.Framework.Outputs.Flexbox;

using UiMetadataFramework.Core.Binding;

[OutputComponent("flexbox")]
[HasConfiguration(typeof(FlexboxAttribute), mandatory: true)]
public class Flexbox<T>
{
	public T? Value { get; set; }
}