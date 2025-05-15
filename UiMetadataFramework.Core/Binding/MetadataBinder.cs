namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	/// <summary>
	/// This class holds mappings between server-side types and client-side UI controls. 
	/// It provides a number of APIs to simplify creation of metadata.
	/// </summary>
	public class MetadataBinder
	{
		private static readonly ConcurrentDictionary<Type, Type?> BaseComponentCache = new();

		/// <summary>
		/// <see cref="IServiceProvider"/> instance used when/if necessary.
		/// </summary>
		public readonly IServiceProvider Container;

		/// <summary>
		/// Collection of input fields.
		/// </summary>
		public readonly FieldCollection Inputs;

		/// <summary>
		/// Collection of output fields.
		/// </summary>
		public readonly FieldCollection Outputs;

		private readonly object key = new();
		private readonly List<string> registeredAssemblies = new();

		/// <summary>
		/// Initializes a new instance of the <see cref="MetadataBinder"/> class and configures
		/// <see cref="DependencyInjectionContainer.Default"/> to be responsible for instantiating
		/// <see cref="ComponentBinding"/> when registering a new assembly.
		/// </summary>
		public MetadataBinder()
			: this(DependencyInjectionContainer.Default)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MetadataBinder"/> class and configures the given
		/// <see cref="DependencyInjectionContainer"/> to be responsible for instantiating
		/// <see cref="ComponentBinding"/> when registering a new assembly.
		/// </summary>
		public MetadataBinder(IServiceProvider container, MetadataBinderConfiguration? config = null)
		{
			this.Container = container;

			this.Config = config ?? new MetadataBinderConfiguration(
				new InputFieldMetadataFactory(),
				new DefaultFieldMetadataFactory());

			this.Inputs = new(
				this,
				container,
				this.Config.InputFieldMetadataFactory,
				ComponentCategories.Input);

			this.Outputs = new(
				this,
				container,
				this.Config.OutputFieldMetadataFactory,
				ComponentCategories.Output);
		}

		/// <summary>
		/// Configuration for this metadata binder.
		/// </summary>
		private MetadataBinderConfiguration Config { get; }

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
			return BaseComponentCache.GetOrAdd(
				component,
				_ =>
				{
					int levels = 0;

					while (true)
					{
						if (component.GetCustomAttribute<TAttribute>(inherit: false) != null)
						{
							if (levels > 1)
							{
								throw new BindingException(
									$"Derived component '{component.FullName}' cannot inherit from another derived component. " +
									$"Multi-level derived components are not supported.");
							}

							return component;
						}

						if (component.BaseType == null || component.BaseType == typeof(object))
						{
							return null;
						}

						component = component.BaseType;

						if (!component.IsAbstract)
						{
							levels += 1;
						}
					}
				});
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

			return !string.IsNullOrWhiteSpace(attribute.Id)
				? attribute.Id!
				: formType.FullName ?? throw new BindingException($"Cannot form ID for type `{formType}`.");
		}

		/// <summary>
		/// Get <see cref="FieldCollection"/> for the given field category.
		/// </summary>
		/// <param name="category">Field category (<see cref="ComponentCategories"/>).</param>
		public FieldCollection GetFieldCollection(string category)
		{
			return category switch
			{
				ComponentCategories.Input => this.Inputs,
				ComponentCategories.Output => this.Outputs,
				_ => throw new BindingException("Cannot find field collection with category `" + category + "`.")
			};
		}

		/// <summary>
		/// Scans assembly for implementations of <see cref="ComponentBinding"/>
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

			var bindings = assembly
				.GetBindings<ComponentBinding>()
				.Select(Activator.CreateInstance)
				.Cast<ComponentBinding>()
				.ToList();

			var components = assembly.GetComponents<ComponentAttribute>()
				.ToList();

			bindings
				.Where(t => t.Category == ComponentCategories.Output)
				.ForEach(t => this.Outputs.Bindings.AddBinding(t));

			bindings
				.Where(t => t.Category == ComponentCategories.Input)
				.ForEach(t => this.Inputs.Bindings.AddBinding(t));

			components
				.Where(t => t.Attribute.Category == ComponentCategories.Output)
				.ForEach(
					t => this.Outputs.Bindings.AddBinding(
						new ComponentBinding(
							t.Attribute.Category,
							[t.Type],
							t.Attribute,
							t.AllowedConfigurations)));

			components
				.Where(t => t.Attribute.Category == ComponentCategories.Input)
				.ForEach(
					t => this.Inputs.Bindings.AddBinding(
						new ComponentBinding(
							t.Attribute.Category,
							[t.Type],
							t.Attribute,
							t.AllowedConfigurations)));
		}

		/// <summary>
		/// Component categories that the framework uses.
		/// </summary>
		public static class ComponentCategories
		{
			/// <summary>
			/// Output components.
			/// </summary>
			public const string Output = "output";

			/// <summary>
			/// Input components.
			/// </summary>
			public const string Input = "input";
		}
	}
}