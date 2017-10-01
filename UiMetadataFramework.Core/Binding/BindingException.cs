namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// Thrown when form's input or output fields are configured incorrectly.
	/// </summary>
	public class BindingException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BindingException"/> class.
		/// </summary>
		/// <param name="message"></param>
		public BindingException(string message)
			: base(message)
		{
		}
	}
}