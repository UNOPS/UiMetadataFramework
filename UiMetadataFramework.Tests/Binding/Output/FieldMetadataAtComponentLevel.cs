namespace UiMetadataFramework.Tests.Binding.Output;

using System.Collections.Generic;
using System.Linq;
using UiMetadataFramework.Basic.Output;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.CustomProperties;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class FieldMetadataAtComponentLevel
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	[Theory]
	[InlineData(nameof(Response.MyComponent1), "p-level", "p-style", "p-docs")]
	[InlineData(nameof(Response.MyComponent2), "c-level", "c-style", "c-docs")]
	public void CanApplyFieldMetadataAtDifferentLevels(
		string fieldId,
		string label,
		string css,
		string documentation)
	{
		var fields = this.binder.Outputs.GetFields(typeof(Response));

		var field = fields.FirstOrDefault(t => t.Id == fieldId) as MyOutputFieldMetadataFactory.Metadata;

		Assert.NotNull(field);
		Assert.Equal("my-component", field!.Component.Type);
		Assert.Equal(label, field.Label);
		Assert.Equal(css, field.CssClass);
		Assert.Equal(documentation, ((List<object>?)field.CustomProperties?["documentation"])?.SingleOrDefault());
	}

	public class Response
	{
		[MyOutputField(Label = "p-level", CssClass = "p-style")]
		[Documentation("p-docs")]
		public MyComponent? MyComponent1 { get; set; }

		public MyComponent? MyComponent2 { get; set; }
	}

	[Documentation("c-docs")]
	[MyOutputField(Label = "c-level", CssClass = "c-style")]
	[MyOutputComponent("my-component")]
	public class MyComponent
	{
		public string? Value { get; set; }
	}
}