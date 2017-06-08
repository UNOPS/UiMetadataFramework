namespace UiMetadataFramework.Core.Attributes
{
	using System;

	public class UiPropertyAttribute : Attribute
	{
		public string Label { get; set; }

		public int OrderIndex { get; set; }

		public object Parameters { get; set; }

		/// <summary>
		/// Gets or sets name of the associated UI control.
		/// When this property is set it will override <see cref="UiModelAttribute.Name"/>.
		/// </summary>
		public string Type { get; set; }
	}
}