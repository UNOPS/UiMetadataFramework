namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// <see cref="IFieldBinding"/> for an input field.
	/// </summary>
	public class InputFieldBinding : IFieldBinding
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InputFieldBinding"/> class.
		/// </summary>
		/// <param name="serverType">Type which should be rendered on the client.</param>
		/// <param name="clientType">Name of the client control which will render the specified type.</param>
		/// <param name="metadataFactory">Type that implements <see cref="IMetadataFactory"/> and which will
		/// be used to construct custom metadata. If null, then no custom metadata will be constructed for
		/// this component.</param>
		public InputFieldBinding(
			Type serverType,
			string clientType,
			Type? metadataFactory)
			: this(new[] { serverType }, clientType, metadataFactory)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InputFieldBinding"/> class.
		/// </summary>
		/// <param name="serverType">Type which should be rendered on the client.</param>
		/// <param name="attribute"><see cref="InputFieldTypeAttribute"/> instance.</param>
		public InputFieldBinding(Type serverType, InputFieldTypeAttribute attribute)
			: this(new[] { serverType }, attribute.ClientType, attribute.MetadataFactory)
		{
			this.MetadataFactory = attribute.MetadataFactory;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InputFieldBinding"/> class.
		/// </summary>
		/// <param name="serverTypes">Types which should be rendered on the client.</param>
		/// <param name="clientType">Name of the client control which will render the specified types.</param>
		/// <param name="metadataFactory">Type that implements <see cref="IMetadataFactory"/> and which will
		/// be used to construct custom metadata. If null, then no custom metadata will be constructed for
		/// this component.</param>
		public InputFieldBinding(
			IEnumerable<Type> serverTypes,
			string clientType,
			Type? metadataFactory)
		{
			this.ServerTypes = serverTypes;
			this.ClientType = clientType;
			this.MetadataFactory = metadataFactory;
		}

		/// <summary>
		/// Gets or sets value indicating whether input should never be explicitly rendered on the client.
		/// If this value is set to true, then <see cref="InputFieldMetadata.Hidden"/> will always
		/// be true.
		/// </summary>
		public bool IsInputAlwaysHidden { get; set; }

		/// <summary>
		/// Gets the server-side types being bound.
		/// </summary>
		public IEnumerable<Type> ServerTypes { get; }

		/// <inheritdoc />
		public string ClientType { get; }

		/// <summary>
		/// Represents <see cref="IMetadataFactory"/> that should be used to construct metadata.
		/// If null then no custom metadata will be constructed.
		/// </summary>
		/// <remarks>If the type implements <see cref="ComponentConfigurationAttribute"/> then it will indicate
		/// that this component has configuration that must be provided whenever constructing its metadata.</remarks>
		public Type? MetadataFactory { get; }

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			if (!(obj is InputFieldBinding binding))
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
				var hashCode = this.ClientType.GetHashCode();
				hashCode = (hashCode * 397) ^ this.ServerTypes.GetHashCode();
				return hashCode;
			}
		}
	}
}