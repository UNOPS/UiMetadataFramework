namespace UiMetadataFramework.Tests.Binding
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Core.Binding;
	using UiMetadataFramework.Tests.Framework.CustomProperties;
	using UiMetadataFramework.Tests.Utilities;
	using Xunit;

	public partial class FormBindingTests
	{
		private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

		private sealed class MyFormAttribute : FormAttribute
		{
			public override IDictionary<string, object?> GetCustomProperties(Type type)
			{
				var menuAttribute = type.GetTypeInfo().GetCustomAttribute<MediatrTests.MenuAttribute>();

				return base.GetCustomProperties(type).Set("ParentMenu", menuAttribute?.ParentMenu);
			}
		}

		[StringProperty("style", "blue")]
		[IntProperty("number", 1_001)]
		[BooleanProperty("bool-false", false)]
		[BooleanProperty("bool-true", true)]
		[MyForm(Id = "Magic", Label = "Do some magic", PostOnLoad = false, CloseOnPostIfModal = true)]
		[Documentation("help 1")]
		[Documentation("help 2")]
		public class DoMagic
		{
		}

		[Fact]
		public void CanGetFormMetadata()
		{
			var formMetadata = this.binder.BindForm<DoMagic, object, object>();

			formMetadata
				.HasCustomProperty("style", "blue")
				.HasCustomProperty("number", 1_001)
				.HasCustomProperty("bool-false", false)
				.HasCustomProperty("bool-true", true);

			var docs = ((List<object>)formMetadata.CustomProperties!["documentation"]!)
				.Cast<string>()
				.ToList();

			Assert.True(docs.Count == 2);
			Assert.True(docs[0] == "help 1");
			Assert.True(docs[1] == "help 2");
		}
	}
}