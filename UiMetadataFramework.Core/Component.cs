namespace UiMetadataFramework.Core;

using UiMetadataFramework.Core.Binding;

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

	/// <summary>
	/// Gets <see cref="Configuration"/> making sure it is of type <typeparamref name="T"/>. 
	/// </summary>
	/// <exception cref="BindingException">Thrown if <see cref="Configuration"/> is null or is
	/// not of type <typeparamref name="T"/>.</exception>
	internal T GetConfigurationOrException<T>() where T : class
	{
		if (this.Configuration is not T result)
		{
			throw new BindingException($"Component configuration is not of type '{typeof(T).FullName}'.");
		}

		return result;
	}

	/// <summary>
	/// Gets <see cref="Configuration"/> making sure it is not null.
	/// </summary>
	/// <exception cref="BindingException">Thrown if <see cref="Configuration"/> is null.</exception>
	internal object GetConfigurationOrException()
	{
		if (this.Configuration == null)
		{
			throw new BindingException("Component does not have any component configuration.");
		}

		return this.Configuration;
	}
}