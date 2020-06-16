namespace UiMetadataFramework.Basic.Input.Typeahead
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Base <see cref="InputFieldBinding"/> for any kind of "typeahead-like" control which needs to be bound to
	/// a <see cref="ITypeaheadRemoteSource"/> or <see cref="ITypeaheadInlineSource{T}"/>.
	/// </summary>
	public abstract class TypeaheadInputFieldBindingBase : InputFieldBinding
	{
		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		/// <param name="serverType">Type which should be rendered on the client.</param>
		/// <param name="clientType">Name of the client control which will render the specified type.</param>
		/// <param name="container">Dependency injection container to use for initialisation of
		/// <see cref="ITypeaheadInlineSource{T}"/>.</param>
		protected TypeaheadInputFieldBindingBase(Type serverType, string clientType, DependencyInjectionContainer container)
			: base(serverType, clientType)
		{
			this.Container = container;
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
				var parameters = property.GetCustomAttributes<RemoteSourceArgumentAttribute>()
					.Select(t => t.GetArgument())
					.ToList();

				return base.GetCustomProperties(attribute, property)
					.Set("Source", typeaheadInputFieldAttribute.Source.GetFormId())
					.Set("Parameters", parameters);
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
}