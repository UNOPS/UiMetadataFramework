namespace UiMetadataFramework.Core.Binding;

using System.Linq;
using System.Reflection;

/// <inheritdoc />
public class DefaultFieldMetadataFactory : IFieldMetadataFactory
{
	/// <inheritdoc />
	public virtual FieldMetadata GetMetadata(
		FieldAttribute? attribute,
		PropertyInfo property,
		ComponentBinding binding,
		MetadataBinder binder)
	{
		var eventHandlerAttributes = property.GetCustomAttributesImplementingInterface<IFieldEventHandlerAttribute>().ToList();
		var illegalAttributes = eventHandlerAttributes.Where(t => !t.ApplicableToFieldCategory(binding.Category)).ToList();
		if (illegalAttributes.Any())
		{
			throw new BindingException(
				$"Field '{property.DeclaringType!.FullName}.{property.Name}' cannot use " +
				$"'{illegalAttributes[0].GetType().FullName}', because the attribute is not " +
				$"applicable for this field category.");
		}

		var component = binder.GetFieldCollection(binding.Category).BuildComponent(property);

		return new FieldMetadata(component)
		{
			Id = property.Name,
			Hidden = attribute?.Hidden ?? false,
			Label = attribute?.Label ?? property.Name,
			OrderIndex = attribute?.OrderIndex ?? 0,
			CustomProperties = property.GetCustomProperties(binder),
			EventHandlers = eventHandlerAttributes.Select(t => t.ToMetadata(property, binder)).ToList(),
		};
	}
}