namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Linq;
	using System.Reflection;

	/// <summary>
	/// Attribute used for decorating input fields.
	/// </summary>
	public class InputFieldAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets value indicating whether this field should be visible or not.
		/// </summary>
		public bool Hidden { get; set; }

		/// <summary>
		/// Gets or sets label for the field.
		/// </summary>
		public string? Label { get; set; }

		/// <summary>
		/// Gets or sets value which will dictate rendering position of this field
		/// in relationship to other input fields in <see cref="FormMetadata.InputFields"/>.
		/// </summary>
		public int OrderIndex { get; set; }

		/// <summary>
		/// Gets or sets value indicating whether value for this input field is required
		/// before submitting the form.
		/// </summary>
		public bool Required { get; set; }

		/// <summary>
		/// Gets metadata for the input field decorated with this attribute.
		/// </summary>
		/// <param name="property">Input field that has been decorated with this attribute.</param>
		/// <param name="binding">Binding for the input field.</param>
		/// <param name="binder">Metadata binder.</param>
		/// <returns>Instance of <see cref="InputFieldMetadata"/>.</returns>
		/// <remarks>This method will be used internally by <see cref="MetadataBinder"/>.</remarks>
		public virtual InputFieldMetadata GetMetadata(PropertyInfo property, InputFieldBinding binding, MetadataBinder binder)
		{
			var propertyType = property.PropertyType.IsConstructedGenericType && !property.PropertyType.IsNullabble()
				? property.PropertyType.GetGenericTypeDefinition()
				: property.PropertyType;

			var required = propertyType.GetTypeInfo().IsValueType
				// non-nullable value types are automatically required,
				// nullable types are automatically NOT required.
				? Nullable.GetUnderlyingType(propertyType) == null
				// reference types use attribute
				: this.Required;

			var eventHandlerAttributes = property.GetCustomAttributesImplementingInterface<IFieldEventHandlerAttribute>().ToList();
			var illegalAttributes = eventHandlerAttributes.Where(t => !t.ApplicableToInputField).ToList();
			if (illegalAttributes.Any())
			{
				throw new BindingException(
					$"Input '{property.DeclaringType!.FullName}.{property.Name}' cannot use " +
					$"'{illegalAttributes[0].GetType().FullName}', because the attribute is not applicable for input fields.");
			}

			return new InputFieldMetadata(binding.ClientType)
			{
				Id = property.Name,
				Hidden = binding.IsInputAlwaysHidden || this.Hidden,
				Label = this.Label ?? property.Name,
				OrderIndex = this.OrderIndex,
				Required = required,
				EventHandlers = eventHandlerAttributes.Select(t => t.ToMetadata(property, binder)).ToList(),
				CustomProperties = property.GetCustomProperties()
			};
		}
	}
}