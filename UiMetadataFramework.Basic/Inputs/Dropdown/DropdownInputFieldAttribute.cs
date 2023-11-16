namespace UiMetadataFramework.Basic.Inputs.Dropdown
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Basic.Inputs.Typeahead;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Used to decorate input fields of type <see cref="DropdownValue{T}"/>.
	/// </summary>
	public class DropdownInputFieldAttribute : InputFieldAttribute
	{
		/// <inheritdoc />
		public DropdownInputFieldAttribute(Type source)
		{
			this.Source = source;
		}

		/// <summary>
		/// Gets or sets source for the dropdown items. The type must implement
		/// <see cref="IDropdownInlineSource"/> or <see cref="IDropdownRemoteSource"/>.
		/// </summary>
		public Type Source { get; set; }

		/// <inheritdoc />
		public override InputFieldMetadata GetMetadata(PropertyInfo property, InputFieldBinding binding, MetadataBinder binder)
		{
			var result = base.GetMetadata(property, binding, binder);

			var customProperties = this.GetCustomProperties(property, binder);
			result.CustomProperties = result.CustomProperties.Merge(customProperties);

			return result;
		}

		private IDictionary<string, object?> GetCustomProperties(
			PropertyInfo property,
			MetadataBinder binder)
		{
			var inlineSource = this.Source
				.GetInterfaces(typeof(IDropdownInlineSource))
				.SingleOrDefault();

			if (inlineSource != null)
			{
				var source = binder.Container.GetService(this.Source);

				var items = this.Source.GetTypeInfo()
					.GetMethod(nameof(IDropdownInlineSource.GetItems))!
					.Invoke(source, null);

				return property.GetCustomProperties()
					.Set("Items", items);
			}

			if (this.Source.GetInterfaces(typeof(IDropdownRemoteSource)).Any())
			{
				var parameters = property.GetCustomAttributes<RemoteSourceArgumentAttribute>()
					.Select(t => t.GetArgument())
					.ToList();

				return property.GetCustomProperties()
					.Set("Source", this.Source.GetFormId())
					.Set("Parameters", parameters);
			}

			throw new BindingException($"Type '{this.Source}' is not a valid dropdown source.");
		}
	}
}