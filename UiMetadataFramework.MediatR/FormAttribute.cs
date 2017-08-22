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
		/// Gets or sets unique identifier for the form.
		/// </summary>
		public string Id { get; set; }

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

		/// <summary>
		/// Gets or sets value indicating whether the initial post (<see cref="PostOnLoad"/>) 
		/// should validate all input fields before posting.
		/// </summary>
		public bool PostOnLoadValidation { get; set; } = true;

		/// <summary>
		/// Gets custom properties of the form.
		/// </summary>
		/// <param name="type">Type to which this attribute is applied.</param>
		/// <returns>Object representing custom properties for the form or null if there are none.</returns>
		public virtual object GetCustomProperties(Type type)
		{
			return null;
		}
	}
}