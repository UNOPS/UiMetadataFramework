namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Reflection;

	/// <summary>
	/// Decorates input field, indicating to use a processor on the field.
	/// </summary>
	public class InputFieldProcessorAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets id of the processor.
		/// </summary>
		public string Id { get; protected set; }

		/// <summary>
		/// Gets or sets event at which the processor will run.
		/// </summary>
		public InputFieldProcessorRunTime RunAt { get; protected set; }

		/// <summary>
		/// Gets custom properties for the processor.
		/// </summary>
		/// <param name="property">Property representing the output field for which to get metadata.</param>
		/// <param name="binder">Metadata binder being used.</param>
		/// <returns>An object with additional metadata describing how to run the processor.</returns>
		public virtual object GetCustomProperties(PropertyInfo property, MetadataBinder binder)
		{
			return null;
		}

		/// <summary>
		/// Converts attribute to the metadata.
		/// </summary>
		/// <param name="property">Property representing the output field for which to get metadata.</param>
		/// <param name="binder">Metadata binder being used.</param>
		/// <returns>Metadata for the input field processor.</returns>
		public InputFieldProcessorMetadata ToMetadata(PropertyInfo property, MetadataBinder binder)
		{
			return new InputFieldProcessorMetadata(this.Id)
			{
				RunAt = this.RunAt,
				CustomProperties = this.GetCustomProperties(property, binder)
			};
		}
	}
}