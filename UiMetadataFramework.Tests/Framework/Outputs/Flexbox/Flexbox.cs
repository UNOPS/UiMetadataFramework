namespace UiMetadataFramework.Tests.Framework.Outputs.Flexbox;

using UiMetadataFramework.Core.Binding;

[OutputFieldType("flexbox", mandatoryCustomProperty: typeof(FlexboxAttribute))]
public class Flexbox<T>
{
	public T? Value { get; set; }
}