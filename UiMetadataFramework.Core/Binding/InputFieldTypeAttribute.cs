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
		/// <param name="metadataFactory">Type that implements <see cref="IMetadataFactory"/> and which will
		/// be used to construct custom metadata. If null, then no custom metadata will be constructed for
		/// this component.</param>
		public InputFieldTypeAttribute(string clientType, Type? metadataFactory = null)
		{
			if (metadataFactory != null &&
				!typeof(IMetadataFactory).IsAssignableFrom(metadataFactory))
			{
				throw new BindingException(
					$"Invalid configuration of output component '{clientType}'. '{metadataFactory.FullName}' " +
					$"must implement '{typeof(IMetadataFactory).FullName}' in order to be used as a metadata factory.");
			}
			
			this.ClientType = clientType;
			this.MetadataFactory = metadataFactory;
		}

		/// <summary>
		/// Gets name of the client control which will render the input field.
		/// </summary>
		public string ClientType { get; }

		/// <summary>
		/// Represents <see cref="IMetadataFactory"/> that should be used to construct metadata.
		/// If null then no custom metadata will be constructed.
		/// </summary>
		public Type? MetadataFactory { get; }
	}
}