namespace UiMetadataFramework.MediatR
{
	using System;

	/// <summary>
	/// Used to decorate an <see cref="IForm{TRequest,TResponse}"/>, describing how to 
	/// generate metadata for it.
	/// </summary>
	public class FormAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets label for this form.
		/// </summary>
		public string Label { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the form should be auto-posted
		/// as soon as it has been loaded by the client. This can be useful for displaying
		/// reports and to generally show data without user having to post the form manually.
		/// </summary>
		public bool PostOnLoad { get; set; }

		///// <summary>
		///// Gets or sets <see cref="Type"/> of the request object required by the <see cref="IForm{TRequest,TResponse}"/>.
		///// </summary>
		//public Type Request { get; set; }

		///// <summary>
		///// Gets or sets <see cref="Type"/> of the response object returned by the <see cref="IForm{TRequest,TResponse}"/>.
		///// </summary>
		//public Type Response { get; set; }
	}
}