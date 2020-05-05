// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace UiMetadataFramework.Basic.Input.Typeahead
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// <see cref="InputFieldBinding"/> for <see cref="TypeaheadValue{T}"/>.
	/// </summary>
	public class TypeaheadInputFieldBinding : InputFieldBinding
	{
		public TypeaheadInputFieldBinding(Type serverType, string clientType, DependencyInjectionContainer container)
			: base(serverType, clientType)
		{
			this.Container = container;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeaheadInputFieldBinding"/> class.
		/// </summary>
		/// <param name="container">Instance of <see cref="DependencyInjectionContainer"/>.</param>
		public TypeaheadInputFieldBinding(DependencyInjectionContainer container)
			: this(typeof(TypeaheadValue<>), "typeahead", container)
		{
		}

		private DependencyInjectionContainer Container { get; }

		/// <summary>
		/// Gets custom properties for the input field.
		/// </summary>
		/// <param name="attribute"><see cref="InputFieldAttribute"/> decorating the <paramref name="property"/>.</param>
		/// <param name="property">Represents an input field.</param>
		/// <returns>Custom properties.</returns>
		public override IDictionary<string, object> GetCustomProperties(InputFieldAttribute attribute, PropertyInfo property)
		{
			// ReSharper disable once UsePatternMatching
			var typeaheadInputFieldAttribute = attribute as TypeaheadInputFieldAttribute;

			if (typeaheadInputFieldAttribute == null)
			{
				throw new BindingException($"Mandatory attribute '{typeof(TypeaheadInputFieldAttribute).FullName}' " +
					$"is missing on '{property.DeclaringType.FullName}.{property.Name}'.");
			}

			if (typeaheadInputFieldAttribute.Source.GetInterfaces(typeof(ITypeaheadRemoteSource)).Any())
			{
				return base.GetCustomProperties(attribute, property)
					.Set("Source", typeaheadInputFieldAttribute.Source.GetFormId())
					.Set("Parameters", typeaheadInputFieldAttribute.Parameters);
			}

			var inlineSource = typeaheadInputFieldAttribute.Source.GetInterfaces(typeof(ITypeaheadInlineSource<>)).SingleOrDefault();
			if (inlineSource != null)
			{
				var source = this.Container.GetInstance(typeaheadInputFieldAttribute.Source);
				var items = typeaheadInputFieldAttribute.Source.GetTypeInfo().GetMethod(nameof(ITypeaheadInlineSource<int>.GetItems)).Invoke(source, null);

				return base.GetCustomProperties(attribute, property)
					.Set("Source", items);
			}

			throw new BindingException($"Field '{property.DeclaringType.FullName}.{property.Name}' is bound to an invalid typeahead source.");
		}
	}

	/// <summary>
	/// Input field type for typeahead client control.
	/// </summary>
	/// <typeparam name="T">Type of values retrieved by the typeahead.</typeparam>
	public class TypeaheadValue<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TypeaheadValue{T}"/> class.
		/// </summary>
		public TypeaheadValue()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeaheadValue{T}"/> class.
		/// </summary>
		public TypeaheadValue(T value)
		{
			this.Value = value;
		}

		/// <summary>
		/// Gets or sets value for the typeahead. The value represents a single item
		/// selected in the typeahead client control.
		/// </summary>
		public T Value { get; set; }
	}

	/// <summary>
	/// Used to decorate input fields of type <see cref="TypeaheadValue{T}"/>.
	/// </summary>
	public class TypeaheadInputFieldAttribute : InputFieldAttribute
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="TypeaheadInputFieldAttribute"/> class.
		/// </summary>
		/// <param name="source">Type which acts as datasource for the items. It must implement
		/// <see cref="ITypeaheadRemoteSource"/> or <see cref="ITypeaheadInlineSource{T}"/>.</param>
		/// <param name="parameters">List of property names in "request" object. These "request" object
		/// properties will be serialized and sent to the typeahead source on each request.</param>
		public TypeaheadInputFieldAttribute(Type source, params string[] parameters)
		{
			this.Source = source;
			this.Parameters = parameters;
		}

		/// <summary>
		/// Gets or sets list of property names inside "request" object. These "request" object
		/// properties will be serialized and sent to the typeahead source on each request.
		/// </summary>
		public string[] Parameters { get; set; }

		/// <summary>
		/// Gets or sets source for the typeahead items. The type must implement
		/// <see cref="ITypeaheadRemoteSource"/> or <see cref="ITypeaheadInlineSource{T}"/>.
		/// </summary>
		public Type Source { get; }
	}
}