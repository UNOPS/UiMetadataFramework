namespace UiMetadataFramework.Basic.Input.Dropdown
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Humanizer;
	using UiMetadataFramework.Basic.Input.Typeahead;
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

		private static Type? GetEnumType(Type type)
		{
			if (type.GetTypeInfo().IsEnum)
			{
				return type;
			}

			return Nullable.GetUnderlyingType(type)?.GetTypeInfo().IsEnum == true
				? Nullable.GetUnderlyingType(type)
				: null;
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

			// Collect all values from [Option] attributes.
			var options = property.GetCustomAttributes<OptionAttribute>()
				.Select(t => new DropdownItem(t.Label, t.Value))
				.ToList();

			// Figure out the T in the DropdownValue<T>.
			var type = property.PropertyType.GenericTypeArguments[0];

			// If it's an enum and there are no [Option] attributes.
			var enumType = GetEnumType(type);
			if (enumType != null && options.Count == 0)
			{
				var items = Enum.GetValues(enumType)
					.Cast<Enum>()
					.Select(t => new DropdownItem(t.Humanize(), t.ToString()))
					.ToList();

				return property.GetCustomProperties()
					.Set("Items", items);
			}

			return property.GetCustomProperties()
				.Set("Items", options);
		}
	}
}