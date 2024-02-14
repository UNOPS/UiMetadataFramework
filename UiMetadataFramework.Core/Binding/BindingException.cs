namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// Thrown when form's metadata is configured incorrectly.
	/// </summary>
	public class BindingException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BindingException"/> class.
		/// </summary>
		public BindingException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BindingException"/> class.
		/// </summary>
		public BindingException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}