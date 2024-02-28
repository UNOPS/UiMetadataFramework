namespace UiMetadataFramework.Tests.Framework.Outputs.Icon;

using UiMetadataFramework.Core.Binding;

[OutputComponent("icon")]
[HasConfiguration(typeof(IconColorDataAttribute))]
[HasConfiguration(typeof(IconBackgroundDataAttribute))]
[HasConfiguration(typeof(IconStyleDataAttribute), isArray: true, name: "Style")]
public class Icon(string name)
{
	/// <summary>
	/// Icon to show.
	/// </summary>
	public string Name { get; set; } = name;

	public string? Tooltip { get; set; }
}