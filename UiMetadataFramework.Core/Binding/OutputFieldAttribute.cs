namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// Attribute used for decorating output fields.
	/// </summary>
	public class OutputFieldAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets value indicating whether this field should be visible or not.
		/// </summary>
		public bool Hidden { get; set; }

		/// <summary>
		/// Gets or sets label for the output field.
		/// </summary>
		public string Label { get; set; }

		/// <summary>
		/// Gets or sets value which will dictate rendering position of this field
		/// in relationship to other output fields in <see cref="FormMetadata.OutputFields"/>.
		/// </summary>
		public int OrderIndex { get; set; }

		/// <summary>
		/// Gets custom properties of the output field.
		/// </summary>
		/// <returns>Object representing custom properties for the output field or null if there are none.</returns>
		public virtual object GetCustomProperties()
		{
			return null;
		}
	}
}