namespace UiMetadataFramework.Core.Binding
{
	using System;
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
		/// Gets metadata for the output field decorated with this attribute.
		/// </summary>
		/// <param name="property">Output field that has been decorated with this attribute.</param>
		/// <param name="binding">Binding for the output field.</param>
		/// <param name="binder">Metadata binder.</param>
		/// <returns>Instance of <see cref="OutputFieldMetadata"/>.</returns>
		/// <remarks>This method will be used internally by <see cref="MetadataBinder"/>.</remarks>
		public virtual OutputFieldMetadata GetMetadata(
			PropertyInfo property,
			OutputFieldBinding binding,
			MetadataBinder binder)
		{
			var eventHandlerAttributes = property.GetCustomAttributesImplementingInterface<IFieldEventHandlerAttribute>().ToList();
			var illegalAttributes = eventHandlerAttributes.Where(t => !t.ApplicableToOutputField).ToList();
			if (illegalAttributes.Any())
			{
				throw new BindingException(
					$"Output '{property.DeclaringType!.FullName}.{property.Name}' cannot use " +
					$"'{illegalAttributes[0].GetType().FullName}', because the attribute is not " +
					$"applicable for output fields.");
			}

			return new OutputFieldMetadata(binding.ClientType)
			{
				Id = property.Name,
				Hidden = this.Hidden,
				Label = this.Label ?? property.Name,
				OrderIndex = this.OrderIndex,
				ComponentConfiguration = GetComponentConfiguration(property, binder),
				CustomProperties = property.GetCustomProperties(binder),
				EventHandlers = eventHandlerAttributes.Select(t => t.ToMetadata(property, binder)).ToList()
			};
		}

		private static object? GetComponentConfiguration(
			PropertyInfo property,
			MetadataBinder binder)
		{
			var factories = property
				.GetCustomAttributes<ComponentConfigurationAttribute>(inherit: true)
				.ToList();

			if (factories.Count > 1)
			{
				throw new BindingException(
					$"Property '{property.DeclaringType!.FullName}.{property.Name}' has " +
					$"{factories.Count} '{nameof(ComponentConfigurationAttribute)}' applied to it. " +
					$"There should never be more than 1.");
			}

			var field = binder.BindOutputField(
				property.PropertyType,
				factories.FirstOrDefault());

			return field.ComponentConfiguration;
		}
	}
}