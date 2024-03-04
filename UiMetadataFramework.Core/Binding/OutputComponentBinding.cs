namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// <see cref="IFieldBinding"/> for an output field.
	/// </summary>
	public class OutputComponentBinding : FieldBinding<OutputComponentAttribute>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OutputComponentBinding"/> class.
		/// </summary>
		/// <param name="serverType">Type which should be rendered on the client.</param>
		/// <param name="clientType">Name of the client control which will render the specified type.</param>
		/// <param name="metadataFactory">Type that implements <see cref="IMetadataFactory"/> and which will
		/// be used to construct custom metadata. If null, then no custom metadata will be constructed for
		/// this component.</param>
		/// <param name="allowedConfigurations">Allowed configurations for this component.</param>
		public OutputComponentBinding(
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
		/// Initializes a new instance of the <see cref="OutputComponentBinding"/> class.
		/// </summary>
		/// <param name="serverTypes">Types which should be rendered on the client.</param>
		/// <param name="clientType">Name of the client control which will render the specified types.</param>
		/// <param name="metadataFactory"><see cref="IMetadataFactory"/> to use for constructing component's
		/// metadata. If null then <see cref="DefaultMetadataFactory"/> will be used.</param>
		/// <param name="allowedConfigurations">Allowed configurations for this component.</param>
		public OutputComponentBinding(
			IEnumerable<Type> serverTypes,
			string clientType,
			Type? metadataFactory,
			params HasConfigurationAttribute[] allowedConfigurations)
			: base(
				serverTypes,
				clientType,
				metadataFactory,
				allowedConfigurations)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="OutputComponentBinding"/> class.
		/// </summary>
		/// <param name="serverType">Type which should be rendered on the client.</param>
		/// <param name="attribute"><see cref="OutputComponentAttribute"/> instance.</param>
		/// <param name="allowedConfigurations">Allowed configurations for this component.</param>
		public OutputComponentBinding(
			Type serverType,
			OutputComponentAttribute attribute,
			params HasConfigurationAttribute[] allowedConfigurations)
			: base(
				new[] { serverType },
				attribute.Name,
				attribute.MetadataFactory,
				allowedConfigurations)
		{
		}
	}
}