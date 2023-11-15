namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	/// <summary>
	/// Attribute used for decorating output fields.
	/// </summary>
	public class OutputFieldAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets value indicating whether this field should be visible or not.
		/// </summary>
		public bool Hidden { get; set; }

		/// <summary>
		/// Gets or sets label for the output field.
		/// </summary>
		public string? Label { get; set; }

		/// <summary>
		/// Gets or sets value which will dictate rendering position of this field
		/// in relationship to other output fields in <see cref="FormMetadata.OutputFields"/>.
		/// </summary>
		public int OrderIndex { get; set; }

		/// <summary>
		/// Gets custom properties of the output field.
		/// </summary>
		/// <param name="property">Property representing the output field for which to get metadata.</param>
		/// <param name="binder">Metadata binder being used.</param>
		/// <returns>Object representing custom properties for the output field or null if there are none.</returns>
		public virtual IDictionary<string, object?>? GetCustomProperties(PropertyInfo property, MetadataBinder binder)
		{
			return null;
		}

		/// <summary>
		/// Gets metadata for the output field decorated with this attribute.
		/// </summary>
		/// <param name="property">Output field that has been decorated with this attribute.</param>
		/// <param name="binding">Binding for the output field.</param>
		/// <param name="binder">Metadata binder.</param>
		/// <returns>Instance of <see cref="OutputFieldMetadata"/>.</returns>
		/// <remarks>This method will be used internally by <see cref="MetadataBinder"/>.</remarks>
		public virtual OutputFieldMetadata GetMetadata(PropertyInfo property, OutputFieldBinding? binding, MetadataBinder binder)
		{
			var isEnumerable = property.IsEnumerable();

			if (!isEnumerable && binding == null)
			{
				throw new KeyNotFoundException(
					$"Cannot retrieve metadata for '{property.DeclaringType}.{property.Name}', " +
					$"because type '{property.PropertyType.FullName}' is not bound to any output field control.");
			}

			var clientControlName = isEnumerable
				? binder.OutputFieldBindings.ContainsKey(property.PropertyType.GenericTypeArguments[0])
					? MetadataBinder.ValueListOutputControlName
					: MetadataBinder.ObjectListOutputControlName
				: binding!.ClientType;

			IDictionary<string, object?>? customProperties;

			if (clientControlName == MetadataBinder.ObjectListOutputControlName)
			{
				var additionalCustomProperties = property.GetCustomProperties();
				customProperties = new Dictionary<string, object?>
				{
					{ "Columns", binder.BindOutputFields(property.PropertyType.GenericTypeArguments[0]).ToList() },
					{ "Customizations", this.GetCustomProperties(property, binder) }
				}.Merge(additionalCustomProperties);
			}
			else
			{
				if (binding != null)
				{
					// All non-enumerable properties (i.e. - custom properties) will have binding.
					customProperties = binding.GetCustomProperties(property, this, binder);
				}
				else
				{
					// Only for "ValueListOutputControlName" (i.e. - string[], int[], FormLink[], etc) will the code come here.
					// Basically only the outputs of IEnumerable{T} where T is a known type with a binding.

					var itemBinding = binder.OutputFieldBindings[property.PropertyType.GenericTypeArguments[0]];

					customProperties = new Dictionary<string, object?>
					{
						{ "Type", itemBinding.ClientType }
					}.Merge(itemBinding.GetCustomProperties(property, this, binder));
				}
			}

			var eventHandlerAttributes = property.GetCustomAttributesImplementingInterface<IFieldEventHandlerAttribute>().ToList();
			var illegalAttributes = eventHandlerAttributes.Where(t => !t.ApplicableToOutputField).ToList();
			if (illegalAttributes.Any())
			{
				throw new BindingException(
					$"Output '{property.DeclaringType!.FullName}.{property.Name}' cannot use " +
					$"'{illegalAttributes[0].GetType().FullName}', because the attribute is not " +
					$"applicable for output fields.");
			}

			return new OutputFieldMetadata(clientControlName)
			{
				Id = property.Name,
				Hidden = this.Hidden,
				Label = this.Label ?? property.Name,
				OrderIndex = this.OrderIndex,
				CustomProperties = customProperties,
				EventHandlers = eventHandlerAttributes.Select(t => t.ToMetadata(property, binder)).ToList()
			};
		}
	}
}