namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// Used for decorating classes which will be used as output fields.
	/// A binding will be created based on this attribute, when <see cref="MetadataBinder.RegisterAssembly"/>
	/// is called.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, Inherited = false)]
	public class OutputFieldTypeAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OutputFieldTypeAttribute"/> class.
		/// </summary>
		/// <param name="clientType">Name of the client control which will render the output field.</param>
		/// <param name="mandatoryCustomProperty">Indicates the <see cref="ICustomPropertyAttribute"/> that must
		/// accompany this component.</param>
		/// <param name="metadataFactory">Type that implements <see cref="IMetadataFactory"/> and which will
		/// be used to construct custom metadata. If null, then no custom metadata will be constructed for
		/// this component.</param>
		public OutputFieldTypeAttribute(
			string clientType,
			Type? mandatoryCustomProperty = null,
			Type? metadataFactory = null)
		{
			if (mandatoryCustomProperty != null &&
				!typeof(ICustomPropertyAttribute).IsAssignableFrom(mandatoryCustomProperty))
			{
				throw new BindingException(
					$"Invalid configuration of output component '{clientType}'. '{mandatoryCustomProperty.FullName}' " +
					$"must implement '{typeof(ICustomPropertyAttribute).FullName}' in order to be used as a custom property.");
			}

			if (metadataFactory != null &&
				!typeof(IMetadataFactory).IsAssignableFrom(metadataFactory))
			{
				throw new BindingException(
					$"Invalid configuration of output component '{clientType}'. '{metadataFactory.FullName}' " +
					$"must implement '{typeof(IMetadataFactory).FullName}' in order to be used as a metadata factory.");
			}

			this.ClientType = clientType;
			this.MandatoryCustomProperty = mandatoryCustomProperty;
			this.MetadataFactory = metadataFactory;
		}

		/// <summary>
		/// Gets name of the client control which will render the output field.
		/// </summary>
		public string ClientType { get; set; }

		/// <summary>
		/// Indicates the <see cref="ICustomPropertyAttribute"/> that must accompany this component.
		/// If null then this component does not require any custom properties.
		/// </summary>
		/// <remarks>Attributes that derive from the specified type are also allowed.</remarks>
		public Type? MandatoryCustomProperty { get; }

		/// <summary>
		/// Represents <see cref="IMetadataFactory"/> that should be used to construct metadata.
		/// If null then no custom metadata will be constructed.
		/// </summary>
		public Type? MetadataFactory { get; }
	}
}