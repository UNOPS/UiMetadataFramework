namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Collections.Generic;

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
		public IEnumerable<Type> ServerTypes { get; set; }

		/// <summary>
		/// Gets custom properties of the output field.
		/// </summary>
		/// <param name="attribute"><see cref="OutputFieldAttribute"/> which was applied to the output field.</param>
		/// <returns>Object representing custom properties for the output field or null if there are none.</returns>
		public virtual object GetCustomProperties(OutputFieldAttribute attribute)
		{
			return null;
		}
	}
}