namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;

	/// <summary>
	/// Attribute used for decorating input fields.
	/// </summary>
	public class InputFieldAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets value indicating whether this field should be visible or not.
		/// </summary>
		public bool Hidden { get; set; }

		/// <summary>
		/// Gets or sets label for the field.
		/// </summary>
		public string Label { get; set; }

		/// <summary>
		/// Gets or sets value which will dictate rendering position of this field
		/// in relationship to other input fields in <see cref="FormMetadata.InputFields"/>.
		/// </summary>
		public int OrderIndex { get; set; }

		/// <summary>
		/// Gets or sets value indicating whether value for this input field is required
		/// before submitting the form.
		/// </summary>
		public bool Required { get; set; }
	}
}