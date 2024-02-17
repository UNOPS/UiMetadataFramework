namespace UiMetadataFramework.Tests.Framework.CustomProperties;

using System;
using UiMetadataFramework.Core.Binding;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
[CustomPropertyConfig(IsArray = true)]
public sealed class DocumentationAttribute : Attribute, ICustomPropertyAttribute
{
	public DocumentationAttribute(string text)
	{
		this.Text = text;
	}

	private string Text { get; }

	public string Name => "documentation";

	public object GetValue(Type type, MetadataBinder binder)
	{
		return this.Text;
	}
}