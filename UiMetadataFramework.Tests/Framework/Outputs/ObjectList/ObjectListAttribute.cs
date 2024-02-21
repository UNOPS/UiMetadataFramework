namespace UiMetadataFramework.Tests.Framework.Outputs.ObjectList;

using System;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

public class ObjectListAttribute : ComponentConfigurationAttribute
{
	public string? Gap { get; set; }
	public string? ListItem { get; set; }
	public string? Style { get; set; }

	public override object CreateMetadata(
		Type type,
		MetadataBinder binder,
		params ComponentConfigurationItemAttribute[] configurationItems)
	{
		var innerType = type.GenericTypeArguments[0];

		var component = binder.BindOutputComponent(innerType);

		return new Configuration(component, this.Style, this.ListItem, this.Gap);
	}

	public class Configuration(
		Component innerComponent,
		string? style,
		string? listItem,
		string? gap)
	{
		public string? Gap { get; } = gap;
		public Component InnerComponent { get; } = innerComponent;
		public string? ListItem { get; } = listItem;
		public string? Style { get; } = style;
	}
}