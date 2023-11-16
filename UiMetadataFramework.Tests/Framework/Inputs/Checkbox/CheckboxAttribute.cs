namespace UiMetadataFramework.Tests.Framework.Inputs.Checkbox;

using System.Reflection;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

public class CheckboxAttribute : InputFieldAttribute
{
	public string? Style { get; set; }

	public override InputFieldMetadata GetMetadata(
		PropertyInfo property,
		InputFieldBinding binding,
		MetadataBinder binder)
	{
		var basic = base.GetMetadata(property, binding, binder);

		return new Metadata(basic) { Style = this.Style };
	}

	public class Metadata : InputFieldMetadata
	{
		public Metadata(InputFieldMetadata basic)
			: base(basic)
		{
		}

		public Metadata(string type) : base(type)
		{
		}

		public string? Style { get; set; }
	}
}