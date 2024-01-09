namespace UiMetadataFramework.Tests.Framework.Outputs.Flexbox;

using UiMetadataFramework.Core.Binding;

[OutputFieldType("flexbox", mandatoryAttribute: typeof(FlexboxAttribute))]
public class Flexbox<T>
{
	public T Value { get; set; }
}