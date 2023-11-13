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
        /// Gets or sets the attribute that must be applied to all uses of that output type.
        /// </summary>
		public Type Attribute { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputFieldTypeAttribute"/> class with the specified client type and attribute.
        /// </summary>
        /// <param name="clientType">The name of the client control which will render the output field.</param>
        /// <param name="attribute">The attribute that must be applied to all uses of that output type.</param>
        public OutputFieldTypeAttribute(string clientType, Type attribute)
        {
            this.ClientType = clientType;
            this.Attribute = attribute;
        }

        /// <summary>
        /// Gets name of the client control which will render the output field.
        /// </summary>
        public string ClientType { get; set; }
	}
}