namespace UiMetadataFramework.Core.Attributes
{
	using System;

	public class UiPropertyAttribute : Attribute
	{
		public bool Hidden { get; set; }

		public string Label { get; set; }

		/// <summary>
		/// Gets or sets value to indicate UI behavior when property's value is null.
		/// </summary>
		public NullDisplayBehavior OnNull { get; set; }

		public int OrderIndex { get; set; }
		public virtual object Parameters { get; set; }

		/// <summary>
		/// Gets or sets name of the associated UI control.
		/// When this property is set it will override <see cref="UiModelAttribute.Name"/>.
		/// </summary>
		public string Type { get; set; }
	}
}