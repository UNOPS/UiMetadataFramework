namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// <see cref="IFieldBinding"/> for an input field.
	/// </summary>
	public class InputComponentBinding : FieldBinding<InputComponentAttribute>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InputComponentBinding"/> class.
		/// </summary>
		/// <param name="serverType">Type which should be rendered on the client.</param>
		/// <param name="clientType">Name of the client control which will render the specified type.</param>
		/// <param name="metadataFactory">Type that implements <see cref="IMetadataFactory"/> and which will
		/// be used to construct custom metadata. If null, then no custom metadata will be constructed for
		/// this component.</param>
		/// <param name="allowedConfigurations">Allowed configurations for this component.</param>
		public InputComponentBinding(
			Type serverType,
			string clientType,
			Type? metadataFactory,
			params HasConfigurationAttribute[] allowedConfigurations)
			: base(
				new[] { serverType },
				clientType,
				metadataFactory,
				allowedConfigurations)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InputComponentBinding"/> class.
		/// </summary>
		/// <param name="serverType">Type which should be rendered on the client.</param>
		/// <param name="attribute"><see cref="InputComponentAttribute"/> instance.</param>
		/// <param name="allowedConfigurations">Allowed configurations for this component.</param>
		public InputComponentBinding(
			Type serverType,
			InputComponentAttribute attribute,
			params HasConfigurationAttribute[] allowedConfigurations)
			: base(
				new[] { serverType },
				attribute.Name,
				attribute.MetadataFactory,
				allowedConfigurations)
		{
			this.IsInputAlwaysHidden = attribute.AlwaysHidden;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InputComponentBinding"/> class.
		/// </summary>
		/// <param name="serverTypes">Types which should be rendered on the client.</param>
		/// <param name="clientType">Name of the client control which will render the specified types.</param>
		/// <param name="metadataFactory">Type that implements <see cref="IMetadataFactory"/> and which will
		/// be used to construct custom metadata. If null, then no custom metadata will be constructed for
		/// this component.</param>
		/// <param name="allowedConfigurations">Allowed configurations for this component.</param>
		public InputComponentBinding(
			IEnumerable<Type> serverTypes,
			string clientType,
			Type? metadataFactory,
			params HasConfigurationAttribute[] allowedConfigurations) : base(
			serverTypes,
			clientType,
			metadataFactory,
			allowedConfigurations)
		{
		}

		/// <summary>
		/// Gets or sets value indicating whether input should never be explicitly rendered on the client.
		/// If this value is set to true, then <see cref="InputFieldMetadata.Hidden"/> will always
		/// be true.
		/// </summary>
		public bool IsInputAlwaysHidden { get; set; }
	}
}