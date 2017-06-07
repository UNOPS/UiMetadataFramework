namespace UiMetadataFramework.Core.Attributes
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Attribute to mark class as a DTO that is bound to a certain UI element.
	/// </summary>
	public class UiModelAttribute : Attribute
	{
		/// <summary>
		/// Indicates whether the model is an <see cref="IEnumerable{T}"/>, where T is the
		/// class which is decorated by this attribute.
		/// </summary>
		public bool IsEnumerableModel { get; set; }

		/// <summary>
		/// Name of the UI element to which the class is bound.
		/// </summary>
		public string Name { get; set; }
	}
}