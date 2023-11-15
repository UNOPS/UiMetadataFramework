namespace UiMetadataFramework.Core
{
	using System.Collections.Generic;

	/// <summary>
	/// Represents metadata for single field within a form.
	/// </summary>
	public interface IFieldMetadata
	{
		/// <summary>
		/// Gets or sets additional parameters for the client control.
		/// </summary>
		IDictionary<string, object?>? CustomProperties { get; set; }

		/// <summary>
		/// Gets or sets event handlers for this field.
		/// </summary>
		IList<EventHandlerMetadata>? EventHandlers { get; set; }

		/// <summary>
		/// Gets or sets value indicating whether this field should be visible or not.
		/// </summary>
		bool Hidden { get; }

		/// <summary>
		/// Gets or sets id of the field to which this metadata belongs.
		/// </summary>
		string? Id { get; }

		/// <summary>
		/// Gets or sets label for the output field.
		/// </summary>
		string? Label { get; }

		/// <summary>
		/// Gets or sets value which will dictate rendering position of this field
		/// in relationship to output fields within the same <see cref="FormResponse"/>.
		/// </summary>
		int OrderIndex { get; }

		/// <summary>
		/// Gets name of the client control which will render this output field.
		/// </summary>
		string Type { get; }
	}
}