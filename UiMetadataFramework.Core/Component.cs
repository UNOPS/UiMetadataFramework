namespace UiMetadataFramework.Core;

/// <summary>
/// Represents a component instance.
/// </summary>
public class Component(string type, object? configuration = null)
{
	/// <summary>
	/// Configuration describing how the component should look/behave.
	/// May be null if the component does not require any configuration.
	/// </summary>
	public object? Configuration { get; } = configuration;

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