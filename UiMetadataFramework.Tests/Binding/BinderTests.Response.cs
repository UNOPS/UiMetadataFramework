namespace UiMetadataFramework.Tests.Binding
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using UiMetadataFramework.Basic.Output;
	using UiMetadataFramework.Basic.Output.FormLink;
	using UiMetadataFramework.Basic.Output.PaginatedData;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	public partial class BinderTests
	{
		public class Response : FormResponse
		{
			public IList<string>? Categories { get; set; }

			[IntProperty("secret", 123)]
			[StringProperty("style", "beatiful")]
			[OutputField(Label = "DoB", OrderIndex = 2)]
			public DateTime DateOfBirth { get; set; }

			[OutputField(Label = "First name", OrderIndex = 1)]
			public string? FirstName { get; set; }

			[OutputField(Hidden = true)]
			public int Height { get; set; }

			public IList<FormLink>? Links { get; set; }

			[PaginatedData(nameof(Request.MainPeoplePaginator))]
			[StringProperty("style", "main")]
			public PaginatedData<Person>? MainPeople { get; set; }

			[IntProperty("secret", 321)]
			[StringProperty("style", "cool")]
			[Documentation("1")]
			[Documentation("2")]
			public IList<Person>? OtherPeople { get; set; }

			[IntProperty("number", 456)]
			[StringProperty("help", "this is help text")]
			[CustomOutputField(Hidden = true, Style = "fancy-output")]
			[OutputFieldEventHandler]
			public decimal Weight { get; set; }
		}
	}

	public class CustomOutputFieldAttribute : OutputFieldAttribute
	{
		public string? Style { get; set; }

		public override OutputFieldMetadata GetMetadata(
			PropertyInfo property,
			OutputFieldBinding? binding,
			MetadataBinder binder)
		{
			var basic = base.GetMetadata(property, binding, binder);

			return new Metadata(basic)
			{
				Style = this.Style
			};
		}

		public class Metadata : OutputFieldMetadata
		{
			public Metadata(OutputFieldMetadata basic)
				: base(basic)
			{
			}

			public Metadata(string type) : base(type)
			{
			}

			public string? Style { get; set; }
		}
	}
}