namespace UiMetadataFramework.Tests.Binding
{
	using System;
	using UiMetadataFramework.Core.Binding;

	public partial class BinderTests
	{
		[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
		[CustomPropertyConfig(IsArray = true)]
		public class DocumentationAttribute : Attribute, ICustomPropertyAttribute
		{
			public DocumentationAttribute(string text)
			{
				this.Text = text;
			}

			public string Text { get; set; }

			public string Name => "documentation";

			public object GetValue()
			{
				return this.Text;
			}
		}
	}
}