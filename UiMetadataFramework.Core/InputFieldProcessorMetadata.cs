namespace UiMetadataFramework.Core
{
	/// <summary>
	/// Represents a function which can be run at a specific time during form's lifecycle to
	/// manipulate input field.
	/// </summary>
	public class InputFieldProcessorMetadata
	{
		public InputFieldProcessorMetadata()
		{
		}

		public InputFieldProcessorMetadata(string id)
		{
			this.Id = id;
		}

		/// <summary>
		/// Gets or sets custom properties describing how to run the processor.
		/// </summary>
		public object CustomProperties { get; set; }

		/// <summary>
		/// Gets or sets id of the processor.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets event at which the processor will run.
		/// </summary>
		public InputFieldProcessorRunTime RunAt { get; set; }
	}
}