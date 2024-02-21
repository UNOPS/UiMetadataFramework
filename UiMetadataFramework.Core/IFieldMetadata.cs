// ReSharper disable UnusedMemberInSuper.Global

namespace UiMetadataFramework.Core
{
	using System.Collections.Generic;

	/// <summary>
	/// Represents a wrapper around a component that can be used to indicate
	/// how a component should be rendered in a broader context of a form/etc.
	/// </summary>
	public interface IFieldMetadata
	{
		/// <summary>
		/// Gets or sets the component to be used.
		/// </summary>
		public Component Component { get; }

		/// <summary>
		/// Gets or sets additional parameters for this field.
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
		/// Gets or sets label for the field.
		/// </summary>
		string? Label { get; }

		/// <summary>
		/// Gets or sets value which will dictate rendering position of this field
		/// in relationship to other fields.
		/// </summary>
		int OrderIndex { get; }
	}
}