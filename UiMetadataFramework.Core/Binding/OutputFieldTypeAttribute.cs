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
		/// <param name="mandatoryAttribute">Subtype of <see cref="OutputFieldAttribute"/> that indicates the
		/// attribute that must be applied to the output field. If null then any <see cref="OutputFieldAttribute"/>
		/// attribute can be applied or no attribute can be applied at all.</param>
		public OutputFieldTypeAttribute(string clientType, Type? mandatoryAttribute = null)
		{
			this.ClientType = clientType;
			this.MandatoryAttribute = mandatoryAttribute;
		}

		/// <summary>
		/// Gets name of the client control which will render the output field.
		/// </summary>
		public string ClientType { get; set; }

		/// <summary>
		/// Indicates a specific subtype of <see cref="OutputFieldAttribute"/> that must be applied
		/// on an output field. If null then any <see cref="OutputFieldAttribute"/> can be applied or
		/// no attribute can be applied at all.
		/// </summary>
		/// <remarks>Attributes that derive from the specified type are also allowed.</remarks>
		public Type? MandatoryAttribute { get; }
	}
}