namespace UiMetadataFramework.Tests.Framework.Outputs.Flexbox;

using UiMetadataFramework.Core.Binding;

[OutputComponent("flexbox", metadataFactory: typeof(FlexboxAttribute))]
public class Flexbox<T>
{
	public T? Value { get; set; }
}