namespace UiMetadataFramework.Basic.InputProcessors
{
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Used for decorating input fields whose values should come from an output field.
	/// </summary>
	public class BindToOutputAttribute : InputFieldProcessorAttribute
	{
		/// <summary>
		/// Configures default value for the input field to be a constant.
		/// </summary>
		/// <param name="outputFieldId">Id of the output field to bind to.</param>
		public BindToOutputAttribute(string outputFieldId)
		{
			this.RunAt = InputFieldProcessorRunTime.Response;
			this.Id = "bind-to-output";
			this.OutputFieldId = outputFieldId;
		}

		/// <summary>
		/// Gets or sets if of the output field to bind to.
		/// </summary>
		public string OutputFieldId { get; set; }

		public override object GetCustomProperties()
		{
			return new
			{
				OutputFieldId = this.OutputFieldId
			};
		}
	}
}