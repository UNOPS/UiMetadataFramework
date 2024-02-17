namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Represents a binding between a <see cref="Type"/> of an output field and the client-side
	/// control which will render that output field. The binding can involve multiple server-side
	/// types being bound to the same client-side control.
	/// </summary>
	public class OutputFieldBinding
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OutputFieldBinding"/> class.
		/// </summary>
		/// <param name="serverType">Type which should be rendered on the client.</param>
		/// <param name="clientType">Name of the client control which will render the specified type.</param>
		/// <param name="mandatoryCustomProperty">Indicates the <see cref="ICustomPropertyAttribute"/> that must
		/// accompany this component.</param>
		/// <param name="metadataFactory">Type that implements <see cref="IMetadataFactory"/> and which will
		/// be used to construct custom metadata. If null, then no custom metadata will be constructed for
		/// this component.</param>
		public OutputFieldBinding(
			Type serverType,
			string clientType,
			Type? mandatoryCustomProperty,
			Type? metadataFactory)
			: this(
				new[] { serverType },
				clientType,
				mandatoryCustomProperty,
				metadataFactory)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="OutputFieldBinding"/> class.
		/// </summary>
		/// <param name="serverTypes">Types which should be rendered on the client.</param>
		/// <param name="clientType">Name of the client control which will render the specified types.</param>
		/// <param name="mandatoryCustomProperty">Indicates the <see cref="ICustomPropertyAttribute"/> that must
		/// accompany this component.</param>
		/// <param name="metadataFactory"></param>
		public OutputFieldBinding(
			IEnumerable<Type> serverTypes,
			string clientType,
			Type? mandatoryCustomProperty,
			Type? metadataFactory)
		{
			this.ServerTypes = serverTypes;
			this.ClientType = clientType;
			this.MandatoryCustomProperty = mandatoryCustomProperty;
			this.MetadataFactory = metadataFactory;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="OutputFieldBinding"/> class.
		/// </summary>
		/// <param name="serverType">Type which should be rendered on the client.</param>
		/// <param name="attribute"><see cref="OutputFieldTypeAttribute"/> instance.</param>
		public OutputFieldBinding(Type serverType, OutputFieldTypeAttribute attribute)
			: this(
				serverType,
				attribute.ClientType,
				attribute.MandatoryCustomProperty,
				attribute.MetadataFactory)
		{
		}

		/// <summary>
		/// Gets name of the client control which will render the output field.
		/// </summary>
		public string ClientType { get; }

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

		/// <summary>
		/// Gets the server-side types being bound.
		/// </summary>
		public IEnumerable<Type> ServerTypes { get; }

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			if (obj is not OutputFieldBinding binding)
			{
				return false;
			}

			return this.ClientType == binding.ClientType &&
				this.ServerTypes.All(t => binding.ServerTypes.Contains(t)) &&
				binding.ServerTypes.All(t => this.ServerTypes.Contains(t));
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			unchecked
			{
				return (this.ClientType.GetHashCode() * 397) ^ this.ServerTypes.GetHashCode();
			}
		}
	}
}