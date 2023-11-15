namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// Used for decorating classes which will be used as input fields.
	/// A binding will be created based on this attribute, when
	/// <see cref="MetadataBinder.RegisterAssembly"/> is called.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, Inherited = false)]
	public class InputFieldTypeAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InputFieldTypeAttribute"/> class.
		/// </summary>
		/// <param name="clientType">Name of the client control which will render the input field.</param>
		/// <param name="mandatoryAttribute">Subtype of <see cref="InputFieldAttribute"/> that indicates the
		/// attribute that must be applied to the input field. If null then any <see cref="InputFieldAttribute"/>
		/// attribute can be applied or no attribute can be applied at all.</param>
		public InputFieldTypeAttribute(string clientType, Type? mandatoryAttribute = null)
		{
			this.ClientType = clientType;
			this.MandatoryAttribute = mandatoryAttribute;
		}

		/// <summary>
		/// Gets name of the client control which will render the input field.
		/// </summary>
		public string ClientType { get; }

		/// <summary>
		/// Indicates a specific subtype of <see cref="InputFieldAttribute"/> that must be applied
		/// on an input field. If null then any <see cref="InputFieldAttribute"/> can be applied or
		/// no attribute can be applied at all.
		/// </summary>
		/// <remarks>Attributes that derive from the specified type are also allowed.</remarks>
		public Type? MandatoryAttribute { get; }
	}
}