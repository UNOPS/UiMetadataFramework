namespace UiMetadataFramework.Core.Binding;

using System.Reflection;

/// <summary>
/// Gets the function name and the method info.
/// </summary>
public class ComponentFunctionBinding
{
	/// <summary>
	/// Represents the binding between a method annotated with <see cref="ComponentFunctionAttribute"/>
	/// and its corresponding metadata, such as the method's information and the attribute instance.
	/// </summary>
	public ComponentFunctionBinding(MethodInfo method, ComponentFunctionAttribute attribute)
	{
		if (!method.IsStatic)
		{
			throw new BindingException(
				$"Method `{method.DeclaringType?.FullName}.{method.Name}` is not static." +
				"Component function must be a static method.");
		}

		this.Method = method;
		this.Attribute = attribute;
	}

	/// <summary>
	/// Attribute with information about the component function.
	/// </summary>
	public ComponentFunctionAttribute Attribute { get; }

	/// <summary>
	/// Method to be invoked when the component function is called.
	/// </summary>
	public MethodInfo Method { get; }
}