namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// <see cref="IFieldBinding"/> for an output field.
	/// </summary>
	public class OutputComponentBinding : IFieldBinding
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OutputComponentBinding"/> class.
		/// </summary>
		/// <param name="serverType">Type which should be rendered on the client.</param>
		/// <param name="clientType">Name of the client control which will render the specified type.</param>
		/// <param name="metadataFactory">Type that implements <see cref="IMetadataFactory"/> and which will
		/// be used to construct custom metadata. If null, then no custom metadata will be constructed for
		/// this component.</param>
		public OutputComponentBinding(
			Type serverType,
			string clientType,
			Type? metadataFactory)
			: this(
				new[] { serverType },
				clientType,
				metadataFactory)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="OutputComponentBinding"/> class.
		/// </summary>
		/// <param name="serverTypes">Types which should be rendered on the client.</param>
		/// <param name="clientType">Name of the client control which will render the specified types.</param>
		/// <param name="metadataFactory"><see cref="IMetadataFactory"/> to use for constructing component's
		/// metadata. If null then <see cref="DefaultMetadataFactory"/> will be used.</param>
		public OutputComponentBinding(
			IEnumerable<Type> serverTypes,
			string clientType,
			Type? metadataFactory)
		{
			this.ServerTypes = serverTypes;
			this.ClientType = clientType;
			this.MetadataFactory = metadataFactory;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="OutputComponentBinding"/> class.
		/// </summary>
		/// <param name="serverType">Type which should be rendered on the client.</param>
		/// <param name="attribute"><see cref="OutputComponentAttribute"/> instance.</param>
		public OutputComponentBinding(Type serverType, OutputComponentAttribute attribute)
			: this(
				serverType,
				attribute.Name,
				attribute.MetadataFactory)
		{
		}

		/// <summary>
		/// Gets the server-side types being bound.
		/// </summary>
		public IEnumerable<Type> ServerTypes { get; }

		/// <inheritdoc />
		public string ClientType { get; }

		/// <inheritdoc />
		public Type? MetadataFactory { get; }

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			if (obj is not OutputComponentBinding binding)
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