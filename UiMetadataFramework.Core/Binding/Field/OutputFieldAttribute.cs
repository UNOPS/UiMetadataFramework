namespace UiMetadataFramework.Core.Binding
{
	using System.Linq;
	using System.Reflection;

	/// <summary>
	/// Attribute used for decorating output fields.
	/// </summary>
	public class OutputFieldAttribute : FieldAttribute
	{
		/// <inheritdoc />
		public OutputFieldAttribute() : base(MetadataBinder.ComponentCategories.Output)
		{
		}

		/// <summary>
		/// Gets metadata for the output field decorated with this attribute.
		/// </summary>
		/// <param name="property">Output field that has been decorated with this attribute.</param>
		/// <param name="binding">Binding for the output field.</param>
		/// <param name="binder">Metadata binder.</param>
		/// <returns>Instance of <see cref="OutputFieldMetadata"/>.</returns>
		/// <remarks>This method will be used internally by <see cref="MetadataBinder"/>.</remarks>
		public override FieldMetadata GetMetadata(
			PropertyInfo property,
			ComponentBinding binding,
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

			var component = binder.Outputs.BuildComponent(property);

			return new FieldMetadata(component)
			{
				Id = property.Name,
				Hidden = this.Hidden,
				Label = this.Label ?? property.Name,
				OrderIndex = this.OrderIndex,
				CustomProperties = property.GetCustomProperties(binder),
				EventHandlers = eventHandlerAttributes.Select(t => t.ToMetadata(property, binder)).ToList()
			};
		}
	}
}