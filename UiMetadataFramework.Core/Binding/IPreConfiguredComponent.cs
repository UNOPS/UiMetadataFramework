namespace UiMetadataFramework.Core.Binding;

/// <summary>
/// Defines a pre-configured version of a component, that can be used
/// instead of the component itself.
/// </summary>
/// <typeparam name="T">Component to be pre-configured.</typeparam>
public interface IPreConfiguredComponent<T> 
{
	/// <summary>
	/// Gets or sets the component's value.
	/// </summary>
	public T? Value { get; set; }
}