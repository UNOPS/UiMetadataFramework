namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	/// <summary>
	/// Represents a binding between a <see cref="Type"/> of an output field and the client-side
	/// control which will render that output field. The binding can involve multiple server-side
	/// types being bound to the same client-side control.
	/// </summary>
	public class OutputFieldBinding
	{
		public OutputFieldBinding(Type serverType, string clientType)
		{
			this.ServerTypes = new[] { serverType };
			this.ClientType = clientType;
		}

		public OutputFieldBinding(IEnumerable<Type> serverTypes, string clientType)
		{
			this.ServerTypes = serverTypes;
			this.ClientType = clientType;
		}

		/// <summary>
		/// Gets name of the client control which will render the output field.
		/// </summary>
		public string ClientType { get; }

		/// <summary>
		/// Gets the server-side types being bound.
		/// </summary>
		public IEnumerable<Type> ServerTypes { get; }

		public override bool Equals(object obj)
		{
			var binding = obj as OutputFieldBinding;

			if (binding == null)
			{
				return false;
			}

			return this.ClientType == binding.ClientType &&
				this.ServerTypes.All(t => binding.ServerTypes.Contains(t)) &&
				binding.ServerTypes.All(t => this.ServerTypes.Contains(t));
		}

		/// <summary>
		/// Gets custom properties of the output field.
		/// </summary>
		/// <param name="property">Property representing the output field for which to get metadata.</param>
		/// <param name="attribute"><see cref="OutputFieldAttribute"/> which was applied to the input field.</param>
		/// <param name="binder">Metadata binder being used.</param>
		/// <returns>Object representing custom properties for the output field or null if there are none.</returns>
		public virtual object GetCustomProperties(PropertyInfo property, OutputFieldAttribute attribute, MetadataBinder binder)
		{
			return attribute?.GetCustomProperties();
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((this.ClientType != null ? this.ClientType.GetHashCode() : 0) * 397) ^ (this.ServerTypes != null ? this.ServerTypes.GetHashCode() : 0);
			}
		}
	}
}