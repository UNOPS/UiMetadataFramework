namespace UiMetadataFramework.MediatR
{
	using System;

	/// <summary>
	/// Thrown when the form's definition has problem or the metadata is configured incorrectly.
	/// </summary>
	public class InvalidConfigurationException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidConfigurationException"/> class.
		/// </summary>
		/// <param name="message"></param>
		public InvalidConfigurationException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidConfigurationException"/> class.
		/// </summary>
		public InvalidConfigurationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}