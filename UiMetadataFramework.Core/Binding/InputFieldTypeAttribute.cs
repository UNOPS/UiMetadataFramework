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
        public InputFieldTypeAttribute(string clientType)
        {
            this.ClientType = clientType;
        }

        /// <summary>
        /// Gets name of the client control which will render the input field.
        /// </summary>
        public string ClientType { get; }
    }
}