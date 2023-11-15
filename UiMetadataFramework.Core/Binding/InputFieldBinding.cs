namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Represents a binding between a <see cref="Type"/> of field and the client-side
	/// control which will render that field. The binding can involve multiple server-side
	/// types being bound to the same client-side control.
	/// </summary>
	public class InputFieldBinding
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InputFieldBinding"/> class.
		/// </summary>
		/// <param name="serverType">Type which should be rendered on the client.</param>
		/// <param name="clientType">Name of the client control which will render the specified type.</param>
		public InputFieldBinding(Type serverType, string clientType)
			: this(new[] { serverType }, clientType)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InputFieldBinding"/> class.
		/// </summary>
		/// <param name="serverType">Type which should be rendered on the client.</param>
		/// <param name="attribute"><see cref="InputFieldTypeAttribute"/> instance.</param>
		public InputFieldBinding(Type serverType, InputFieldTypeAttribute attribute)
			: this(new[] { serverType }, attribute.ClientType)
		{
			this.MandatoryAttribute = attribute.MandatoryAttribute;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InputFieldBinding"/> class.
		/// </summary>
		/// <param name="serverTypes">Types which should be rendered on the client.</param>
		/// <param name="clientType">Name of the client control which will render the specified types.</param>
		public InputFieldBinding(IEnumerable<Type> serverTypes, string clientType)
		{
			this.ServerTypes = serverTypes;
			this.ClientType = clientType;
		}

		/// <summary>
		/// Gets name of the client control which will render the input field.
		/// </summary>
		public string ClientType { get; }

		/// <summary>
		/// Gets or sets value indicating whether input should never be explicitly rendered on the client.
		/// If this value is set to true, then <see cref="InputFieldMetadata.Hidden"/> will always
		/// be true.
		/// </summary>
		public bool IsInputAlwaysHidden { get; set; }

		/// <summary>
		/// Indicates a specific subtype of <see cref="InputFieldAttribute"/> that must be applied
		/// on an input field. If null then any <see cref="InputFieldAttribute"/> can be applied or
		/// no attribute can be applied at all.
		/// </summary>
		/// <remarks>Attributes that derive from the specified type are also allowed.</remarks>
		public Type? MandatoryAttribute { get; }

		/// <summary>
		/// Gets the server-side types being bound.
		/// </summary>
		public IEnumerable<Type> ServerTypes { get; }

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