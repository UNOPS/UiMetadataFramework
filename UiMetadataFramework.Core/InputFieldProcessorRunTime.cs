namespace UiMetadataFramework.Core
{
	/// <summary>
	/// Represents the point in time when an input field processor should run.
	/// </summary>
	public enum InputFieldProcessorRunTime
	{
		/// <summary>
		/// Upon initialization of the input.
		/// </summary>
		Init,

		/// <summary>
		/// After the form has been submitted and response received.
		/// </summary>
		Response
	}
}