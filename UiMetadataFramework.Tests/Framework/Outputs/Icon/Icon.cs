namespace UiMetadataFramework.Tests.Framework.Outputs.Icon;

using UiMetadataFramework.Core.Binding;

[OutputComponent("icon")]
[HasConfiguration(typeof(IconColorAttribute))]
[HasConfiguration(typeof(IconBackgroundAttribute))]
[HasConfiguration(typeof(IconStyleAttribute), isArray: true, name: "Style")]
public class Icon(string name)
{
	/// <summary>
	/// Icon to show.
	/// </summary>
	public string Name { get; set; } = name;

	public string? Tooltip { get; set; }
}