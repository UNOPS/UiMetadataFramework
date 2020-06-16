namespace UiMetadataFramework.Basic.Input.Dropdown
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Humanizer;
	using UiMetadataFramework.Basic.Input.Typeahead;
	using UiMetadataFramework.Core.Binding;

	public class DropdownInputFieldBinding : InputFieldBinding
	{
		public const string ControlName = "dropdown";

		public DropdownInputFieldBinding(DependencyInjectionContainer container) : base(typeof(DropdownValue<>), ControlName)
		{
			this.Container = container;
		}

		private DependencyInjectionContainer Container { get; }

		/// <inheritdoc cref="InputFieldBinding.GetCustomProperties"/>
		public override IDictionary<string, object> GetCustomProperties(InputFieldAttribute attribute, PropertyInfo property)
		{
			if (attribute is DropdownInputFieldAttribute dropdownInputFieldAttribute)
			{
				// Collect all values from inline source if exists
				var inlineSource = dropdownInputFieldAttribute.Source.GetInterfaces(typeof(IDropdownInlineSource)).SingleOrDefault();
				if (inlineSource != null)
				{
					var source = this.Container.GetInstance(dropdownInputFieldAttribute.Source);
					var items = dropdownInputFieldAttribute.Source.GetTypeInfo().GetMethod(nameof(IDropdownInlineSource.GetItems)).Invoke(source, null);

					return base.GetCustomProperties(attribute, property)
						.Set("Items", items);
				}

				// Collect all values from remote source if exists
				if (dropdownInputFieldAttribute.Source.GetInterfaces(typeof(IDropdownRemoteSource)).Any())
				{
					var parameters = property.GetCustomAttributes<RemoteSourceArgumentAttribute>()
						.Select(t => t.GetArgument())
						.ToList();

					return base.GetCustomProperties(attribute, property)
						.Set("Source", dropdownInputFieldAttribute.Source.GetFormId())
						.Set("Parameters", parameters);
				}
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

				return base.GetCustomProperties(attribute, property).Set("Items", items);
			}

			return base.GetCustomProperties(attribute, property).Set("Items", options);
		}

		private static Type GetEnumType(Type type)
		{
			if (type.GetTypeInfo().IsEnum)
			{
				return type;
			}

			return Nullable.GetUnderlyingType(type)?.GetTypeInfo().IsEnum == true
				? Nullable.GetUnderlyingType(type)
				: null;
		}
	}
}