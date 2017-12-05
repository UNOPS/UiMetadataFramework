namespace UiMetadataFramework.Basic.Input
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Humanizer;
	using UiMetadataFramework.Core.Binding;

	public class DropdownInputFieldBinding : InputFieldBinding
	{
		public const string ControlName = "dropdown";

		public DropdownInputFieldBinding() : base(typeof(DropdownValue<>), ControlName)
		{
		}

		/// <inheritdoc cref="InputFieldBinding.GetCustomProperties"/>
		public override IDictionary<string, object> GetCustomProperties(InputFieldAttribute attribute, PropertyInfo property)
		{
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

	/// <summary>
	/// Represents an item that should be avaialble for user to pick
	/// as one of the options from an input field such as dropdown.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	public class OptionAttribute : Attribute
	{
		public OptionAttribute(string label, string value)
		{
			this.Label = label;
			this.Value = value;
		}

		public OptionAttribute(object value)
		{
			var v = (Enum)value;
			this.Label = v.Humanize();
			this.Value = v.ToString();
		}

		public OptionAttribute()
		{
		}

		public string Label { get; set; }
		public string Value { get; set; }
	}

	public class DropdownItem
	{
		public DropdownItem(string label, string value)
		{
			this.Value = value;
			this.Label = label;
		}

		public DropdownItem()
		{
		}

		public string Label { get; set; }

		public string Value { get; set; }
	}

	public class DropdownValue<T>
	{
		public DropdownValue()
		{
		}

		public DropdownValue(T value)
		{
			this.Value = value;
		}

		public T Value { get; set; }
	}
}