// ReSharper disable MemberCanBePrivate.Global

namespace UiMetadataFramework.Basic.Input.Typeahead
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

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
		public TypeaheadInputFieldAttribute(Type source)
		{
			this.Source = source;
		}

		/// <summary>
		/// Gets or sets source for the typeahead items. The type must implement
		/// <see cref="ITypeaheadRemoteSource"/> or <see cref="ITypeaheadInlineSource{T}"/>.
		/// </summary>
		public Type Source { get; }

		/// <inheritdoc />
		public override InputFieldMetadata GetMetadata(
			PropertyInfo property,
			InputFieldBinding binding,
			MetadataBinder binder)
		{
			var result = base.GetMetadata(property, binding, binder);

			var customProperties = this.GetCustomProperties(property, binder);

			result.CustomProperties = result.CustomProperties.Merge(customProperties);

			return result;
		}

		/// <summary>
		/// Gets custom properties for the input field.
		/// </summary>
		/// <param name="property">Represents an input field.</param>
		/// <param name="binder"><see cref="MetadataBinder"/> instance.</param>
		/// <returns>Custom properties.</returns>
		protected IDictionary<string, object?> GetCustomProperties(PropertyInfo property, MetadataBinder binder)
		{
			if (this.Source.GetInterfaces(typeof(ITypeaheadRemoteSource)).Any())
			{
				var parameters = property.GetCustomAttributes<RemoteSourceArgumentAttribute>()
					.Select(t => t.GetArgument())
					.ToList();

				return property.GetCustomProperties()
					.Set("Source", this.Source.GetFormId())
					.Set("Parameters", parameters);
			}

			var inlineSource = this.Source
				.GetInterfaces(typeof(ITypeaheadInlineSource<>))
				.SingleOrDefault();

			if (inlineSource != null)
			{
				var source = binder.Container.GetService(this.Source);

				var items = this.Source.GetTypeInfo()
					.GetMethod(nameof(ITypeaheadInlineSource<int>.GetItems))
					!.Invoke(source, null);

				return property.GetCustomProperties()
					.Set("Source", items);
			}

			throw new BindingException(
				$"Field '{property.DeclaringType!.FullName}.{property.Name}' is " +
				$"bound to an invalid typeahead source.");
		}
	}
}