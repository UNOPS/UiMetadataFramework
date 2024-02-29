namespace UiMetadataFramework.Basic.Inputs.Dropdown
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Basic.Inputs.Typeahead;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// <see cref="IMetadataFactory"/> for <see cref="DropdownValue{T}"/>.
	/// </summary>
	public class DropdownMetadataFactory : DefaultMetadataFactory
	{
		/// <inheritdoc />
		protected override void AugmentConfiguration(
			Type type,
			MetadataBinder binder,
			ComponentConfigurationAttribute[] configurationData,
			Dictionary<string, object?> result)
		{
			var sourceType = configurationData.OfType<DropdownAttribute>().First().Source;

			var inlineSource = sourceType
				.GetInterfaces(typeof(IDropdownInlineSource))
				.SingleOrDefault();

			if (inlineSource != null)
			{
				var source = binder.Container.GetService(sourceType);

				var items = (IEnumerable<DropdownItem>)sourceType.GetTypeInfo()
					.GetMethod(nameof(IDropdownInlineSource.GetItems))!
					.Invoke(source, null);

				result["Items"] = items.ToList();

				return;
			}

			if (sourceType.GetInterfaces(typeof(IDropdownRemoteSource)).Any())
			{
				var parameters = configurationData.OfType<RemoteSourceArgumentAttribute>()
					.Select(t => t.GetArgument())
					.ToList();

				result["Source"] = sourceType.GetFormId();
				result["Parameters"] = parameters;

				return;
			}

			throw new BindingException($"Type '{sourceType}' is not a valid dropdown source.");
		}
	}
}