namespace UiMetadataFramework.Core.Binding
{
	using System;

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
		/// <returns>An object with additional metadata describing how to run the processor.</returns>
		public virtual object GetCustomProperties()
		{
			return null;
		}

		public InputFieldProcessorMetadata ToMetadata()
		{
			return new InputFieldProcessorMetadata(this.Id)
			{
				RunAt = this.RunAt,
				CustomProperties = this.GetCustomProperties()
			};
		}
	}
}