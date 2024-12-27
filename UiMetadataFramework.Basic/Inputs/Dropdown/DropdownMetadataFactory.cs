namespace UiMetadataFramework.Basic.Inputs.Dropdown
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Reflection;
	using Humanizer;
	using UiMetadataFramework.Basic.Inputs.Typeahead;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// <see cref="IMetadataFactory"/> for <see cref="DropdownValue{T}"/>.
	/// </summary>
	public class DropdownMetadataFactory : DefaultMetadataFactory
	{
		private const string SubtypeProperty = "Subtype";

		/// <inheritdoc />
		protected override void AugmentConfiguration(
			Type type,
			Type? derivedType,
			MetadataBinder binder,
			ComponentConfigurationAttribute[] configurationData,
			Dictionary<string, object?> result)
		{
			var sourceType = configurationData.OfType<DropdownAttribute>().SingleOrDefault()?.Source;

			var innerType = type.GenericTypeArguments[0];
			result[SubtypeProperty] = Nullable.GetUnderlyingType(innerType)?.Name ?? innerType.Name;

			if (sourceType == null)
			{
				// Get the T in DropdownValue<T>.
				var enumType = type.GenericTypeArguments[0].GetEnumType();

				if (enumType != null)
				{
					var items = Enum.GetValues(enumType)
						.Cast<object>()
						.Select(
							t => new DropdownItem(
								label: t.ToString().Humanize(LetterCasing.Sentence),
								value: t.ToString()))
						.ToList();

					result["Items"] = items;
					result["Source"] = enumType.FullName;

					return;
				}
			}
			else
			{
				var inlineSource = sourceType
					.GetInterfaces(typeof(IDropdownInlineSource))
					.SingleOrDefault();

				if (inlineSource != null)
				{
					var source = binder.Container.GetService(sourceType);

					var items = sourceType.GetTypeInfo()
						.GetMethod(nameof(IDropdownInlineSource.GetItems))!
						.Invoke(source, null);

					result["Items"] = items;
					result["Source"] = sourceType.FullName;

					return;
				}

				if (sourceType.GetInterfaces(typeof(ITypeaheadRemoteSource)).Any())
				{
					result["Source"] = sourceType.GetFormId();

					return;
				}
			}

			throw new BindingException("Field defines an invalid dropdown source.");
		}

		internal static Dictionary<string, object> ForInlineItems(List<DropdownItem> items, string? source = null)
		{
			return new Dictionary<string, object>
			{
				{ "Items", items },
				{ "Source", source ?? Guid.NewGuid().ToString() },
				{ "Subtype", "string" }
			};
		}

		internal static DropdownConfigurationValue GetConfiguration(Component component)
		{
			var dictionary = (Dictionary<string, object>)component.Configuration!;

			Debug.Assert(dictionary != null, nameof(dictionary) + " != null");

			var source = dictionary!["Source"] as string;

			if (dictionary.GetValueOrDefault("Items") is IEnumerable<DropdownItem> items)
			{
				return new DropdownConfigurationValue(source, items);
			}

			var paramsOrDefault = dictionary.GetValueOrDefault("Parameters");
			
			var parameters = (paramsOrDefault as IEnumerable<Dictionary<string, object>>)
				?.Select(RemoteSourceArgumentAttribute.FromDictionary)
				.ToArray();

			var subtype = dictionary[SubtypeProperty] as string;

			return new DropdownConfigurationValue(
				source,
				parameters,
				subtype);
		}

		/// <summary>
		/// Configuration for the dropdown.
		/// </summary>
		internal class DropdownConfigurationValue
		{
			public DropdownConfigurationValue(
				string? source,
				IEnumerable<RemoteSourceArgumentAttribute>? args,
				string? subtype)
			{
				this.Source = source;
				this.Parameters = args?.ToArray() ?? [];
				this.Subtype = subtype;
			}

			public DropdownConfigurationValue(string? source, IEnumerable<DropdownItem> items)
			{
				this.Source = source;
				this.Items = items.ToArray();
			}

			/// <summary>
			/// List of inline items. If empty, then the items can be retrieved from the source
			/// (which in this case is a remote source).
			/// </summary>
			public DropdownItem[]? Items { get; }

			public RemoteSourceArgumentAttribute[]? Parameters { get; }

			/// <summary>
			/// An identifier for the source from where the items are taken. If <see cref="Items"/> are empty,
			/// then this is a remote source.
			/// </summary>
			public string? Source { get; }

			/// <summary>
			/// Indicate the name of the type T inside the `Dropdown{T}`.
			/// </summary>
			public string? Subtype { get; }
		}
	}
}