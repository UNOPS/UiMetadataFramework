namespace UiMetadataFramework.Core;

/// <summary>
/// Represents a component instance.
/// </summary>
public class Component(
	string type,
	string serverType,
	object? configuration = null)
{
	/// <summary>
	/// Configuration describing how the component should look/behave.
	/// May be null if the component does not require any configuration.
	/// </summary>
	public object? Configuration { get; } = configuration;

	/// <summary>
	/// Name of the server-side component. This is useful
	/// to distinguish between different derivatives
	/// of a component.
	/// </summary>
	public string ServerType { get; } = serverType;

	/// <summary>
	/// Indicates the component's type.
	/// </summary>
	public string Type { get; } = type;

	/// <inheritdoc />
	public override string ToString()
	{
		return this.Type;
	}
}