namespace UiMetadataFramework.Tests.Framework.Outputs.Flexbox;

using System.Reflection;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

public class FlexboxAttribute : OutputFieldAttribute
{
	public override OutputFieldMetadata GetMetadata(
		PropertyInfo property,
		OutputFieldBinding? binding,
		MetadataBinder binder)
	{
		var basic = base.GetMetadata(property, binding, binder);

		return new Metadata(basic);
	}

	public class Metadata(OutputFieldMetadata basic) : OutputFieldMetadata(basic);
}