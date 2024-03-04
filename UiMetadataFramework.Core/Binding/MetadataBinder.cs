namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	/// <summary>
	/// This class holds mappings between server-side types and client-side UI controls. 
	/// It provides a number of APIs to simplify creation of metadata.
	/// </summary>
	public class MetadataBinder
	{
		/// <summary>
		/// <see cref="IServiceProvider"/> instance used when/if necessary.
		/// </summary>
		public readonly IServiceProvider Container;

		/// <summary>
		/// Collection of input fields.
		/// </summary>
		public readonly FieldCollection<InputFieldAttribute, InputFieldMetadata, InputComponentBinding> Inputs;

		/// <summary>
		/// Collection of output fields.
		/// </summary>
		public readonly FieldCollection<OutputFieldAttribute, OutputFieldMetadata, OutputComponentBinding> Outputs;

		private readonly object key = new();
		private readonly List<string> registeredAssemblies = new();

		/// <summary>
		/// Initializes a new instance of the <see cref="MetadataBinder"/> class and configures
		/// <see cref="DependencyInjectionContainer.Default"/> to be responsible for instantiating
		/// <see cref="InputComponentBinding"/> and <see cref="OutputComponentBinding"/> when registering
		/// a new assembly.
		/// </summary>
		public MetadataBinder()
			: this(DependencyInjectionContainer.Default)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MetadataBinder"/> class and configures the given
		/// <see cref="DependencyInjectionContainer"/> to be responsible for instantiating
		/// <see cref="InputComponentBinding"/> and <see cref="OutputComponentBinding"/> when registering
		/// a new assembly.
		/// </summary>
		public MetadataBinder(IServiceProvider container)
		{
			this.Container = container;
			this.Inputs = new(this, container);
			this.Outputs = new(this, container);
		}

		/// <summary>
		/// Looks into the inheritance chain of <paramref name="component"/> and tries to find
		/// the class which has <typeparamref name="TAttribute"/> attribute applied directly to it. If
		/// <paramref name="component"/> itself has the attribute then it will be returned.
		/// </summary>
		/// <param name="component">Component class or a class deriving from a component class.</param>
		/// <returns>Class that has <typeparamref name="TAttribute"/> attribute applied directly to it and
		/// thereby represents a component.</returns>
		public static Type? GetBaseComponent<TAttribute>(Type component) where TAttribute : ComponentAttribute
		{
			while (true)
			{
				if (component.GetCustomAttribute<TAttribute>(inherit: false) != null)
				{
					return component;
				}

				if (component.BaseType == null || component.BaseType == typeof(object))
				{
					return null;
				}

				component = component.BaseType;
			}
		}

		/// <summary>
		/// Gets id of the form.
		/// </summary>
		/// <param name="formType">Type representing the form.</param>
		/// <returns>Id of the form.</returns>
		public static string GetFormId(Type formType)
		{
			var attribute = formType.GetTypeInfo().GetCustomAttributeSingleOrDefault<FormAttribute>();

			if (attribute == null)
			{
				throw new BindingException(
					$"Type '{formType.FullName}' does not have mandatory " +
					$"attribute '{typeof(FormAttribute).FullName}'.");
			}

			return GetFormId(formType, attribute);
		}

		/// <summary>
		/// Gets component property encapsulated inside <see cref="IPreConfiguredComponent{T}"/>.
		/// If <paramref name="type"/> is not a <see cref="IPreConfiguredComponent{T}"/>, then null is returned. 
		/// </summary>
		/// <param name="type">Type that potentially implements <see cref="IPreConfiguredComponent{T}"/>.</param>
		/// <returns><see cref="PropertyInfo"/> representing
		/// <see cref="IPreConfiguredComponent{T}"/>.<see cref="IPreConfiguredComponent{T}.Value"/> or null
		/// if <paramref name="type"/> is not a <see cref="IPreConfiguredComponent{T}"/>.</returns>
		/// <exception cref="BindingException"></exception>
		public static PropertyInfo? GetInnerComponent(Type type)
		{
			if (type.ImplementsType(typeof(IPreConfiguredComponent<>)))
			{
				var property = type.GetProperty(nameof(IPreConfiguredComponent<object>.Value))!;

				if (property.PropertyType.ImplementsType(typeof(IPreConfiguredComponent<>)))
				{
					throw new BindingException(
						$"Pre-configured component '{type.FullName}' cannot have nested pre-configured " +
						$"component '{property.PropertyType.FullName}'. Nesting pre-configured components " +
						$"is not supported.");
				}

				return property;
			}

			return null;
		}

		/// <summary>
		/// Gets form metadata for the specified form.
		/// </summary>
		/// <typeparam name="TForm">Type representing the form.</typeparam>
		/// <typeparam name="TRequest">Type representing request for the form. 
		/// <see cref="FormMetadata.InputFields"/> will be deduced from this class.</typeparam>
		/// <typeparam name="TResponse">Type representing response of the form. 
		/// <see cref="FormMetadata.OutputFields"/> will be deduced from this class.</typeparam>
		/// <returns><see cref="FormMetadata"/> instance.</returns>
		public FormMetadata BuildForm<TForm, TRequest, TResponse>()
		{
			return this.BuildForm(typeof(TForm), typeof(TRequest), typeof(TResponse));
		}

		/// <summary>
		/// Gets form metadata for the specified form.
		/// </summary>
		/// <param name="formType"> name="TForm">Type representing the form.</param>
		/// <param name="requestType">Type representing request for the form. 
		/// <see cref="FormMetadata.InputFields"/> will be deduced from this class.</param>
		/// <param name="responseType">Type representing response of the form. 
		/// <see cref="FormMetadata.OutputFields"/> will be deduced from this class.</param>
		/// <returns><see cref="FormMetadata"/> instance.</returns>
		public FormMetadata BuildForm(
			Type formType,
			Type requestType,
			Type responseType)
		{
			return new FormMetadata(this, formType, requestType, responseType);
		}

		/// <summary>
		/// Scans assembly for implementations of <see cref="OutputComponentBinding"/>, <see cref="InputComponentBinding"/>
		/// and registers them in this instance of <see cref="MetadataBinder"/>.
		/// </summary>
		/// <param name="assembly">Assembly to scan.</param>
		public void RegisterAssembly(Assembly assembly)
		{
			// Avoid registering the same assembly twice.
			lock (this.key)
			{
				if (this.registeredAssemblies.Contains(assembly.FullName))
				{
					return;
				}

				this.registeredAssemblies.Add(assembly.FullName);
			}

			assembly
				.GetBindings<OutputComponentBinding>()
				.Select(t => this.Container.GetService(t))
				.Cast<OutputComponentBinding>()
				.ForEach(t => this.Outputs.Bindings.AddBinding(t));

			assembly.GetComponents<OutputComponentAttribute>()
				.ForEach(t => this.Outputs.Bindings.AddBinding(new OutputComponentBinding(t.Type, t.Attribute, t.AllowedConfigurations)));

			assembly
				.GetBindings<InputComponentBinding>()
				.Select(t => this.Container.GetService(t))
				.Cast<InputComponentBinding>()
				.ForEach(t => this.Inputs.Bindings.AddBinding(t));

			assembly.GetComponents<InputComponentAttribute>()
				.ForEach(t => this.Inputs.Bindings.AddBinding(new InputComponentBinding(t.Type, t.Attribute, t.AllowedConfigurations)));
		}

		internal static string GetFormId(Type formType, FormAttribute formAttribute)
		{
			return !string.IsNullOrWhiteSpace(formAttribute.Id)
				? formAttribute.Id!
				: formType.FullName ?? throw new BindingException($"Cannot form ID for type `{formType}`.");
		}
	}
}