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
		public OutputFieldTypeAttribute(string clientType)
		{
			this.ClientType = clientType;
		}

		/// <summary>
		/// Gets name of the client control which will render the output field.
		/// </summary>
		public string ClientType { get; set; }
	}
}