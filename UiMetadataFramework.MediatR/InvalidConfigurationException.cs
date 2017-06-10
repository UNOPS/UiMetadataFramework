namespace UiMetadataFramework.MediatR
{
	using System;

	public class InvalidConfigurationException : Exception
	{
		public InvalidConfigurationException(string message)
			: base(message)
		{
		}

		public InvalidConfigurationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}