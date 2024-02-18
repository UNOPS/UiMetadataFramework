namespace UiMetadataFramework.Basic.Inputs.Dropdown
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Basic.Inputs.Typeahead;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Used to decorate input fields of type <see cref="DropdownValue{T}"/>.
	/// </summary>
	public class DropdownInputFieldAttribute : ComponentConfigurationAttribute
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
		public override object? CreateMetadata(
			Type type,
			MetadataBinder binder,
			params ComponentConfigurationItemAttribute[] additionalConfigurations)
		{
			var inlineSource = this.Source
				.GetInterfaces(typeof(IDropdownInlineSource))
				.SingleOrDefault();

			if (inlineSource != null)
			{
				var source = binder.Container.GetService(this.Source);

				var items = (IEnumerable<DropdownItem>)this.Source.GetTypeInfo()
					.GetMethod(nameof(IDropdownInlineSource.GetItems))!
					.Invoke(source, null);

				return new Configuration { Items = items.ToList() };
			}

			if (this.Source.GetInterfaces(typeof(IDropdownRemoteSource)).Any())
			{
				var parameters = additionalConfigurations.OfType<RemoteSourceArgumentAttribute>()
					.Select(t => t.GetArgument())
					.ToList();

				return new Configuration
				{
					Source = this.Source.GetFormId(),
					Parameters = parameters
				};
			}

			throw new BindingException($"Type '{this.Source}' is not a valid dropdown source.");
		}

		/// <summary>
		/// Represents configuration for the dropdown input field.
		/// </summary>
		public class Configuration
		{
			/// <summary>
			/// List of items to display in the dropdown. Only used when the
			/// source is an inline source.
			/// </summary>
			public List<DropdownItem>? Items { get; set; }

			/// <summary>
			/// List of parameters to pass when calling <see cref="Source"/>.
			/// </summary>
			public List<RemoteSourceArgument>? Parameters { get; set; }

			/// <summary>
			/// Name of the external source from which to retrieve the list of items. 
			/// </summary>
			public string? Source { get; set; }
		}
	}
}