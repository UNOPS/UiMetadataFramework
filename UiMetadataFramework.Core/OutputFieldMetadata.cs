namespace UiMetadataFramework.Core
{
	using System.Collections.Generic;
	using Newtonsoft.Json;

	/// <summary>
	/// Represents metadata for a single output field.
	/// </summary>
	public class OutputFieldMetadata : IFieldMetadata
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OutputFieldMetadata"/> class.
		/// </summary>
		/// <param name="type">Name of the client control which will be responsible for rendering
		/// this output field.</param>
		public OutputFieldMetadata(string type)
		{
			this.Type = type;
		}

		/// <summary>
		/// Gets or sets additional parameters for the client control.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public IDictionary<string, object> CustomProperties { get; set; }

		/// <summary>
		/// Gets or sets id of the field to which this metadata belongs.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets label for the output field.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string Label { get; set; }

		/// <summary>
		/// Gets name of the client control which will render this output field.
		/// </summary>
		public string Type { get; protected set; }

		/// <summary>
		/// Gets or sets value indicating whether this field should be visible or not.
		/// </summary>
		public bool Hidden { get; set; }

		/// <summary>
		/// Gets or sets value which will dictate rendering position of this field
		/// in relationship to output fields within the same <see cref="FormResponse"/>.
		/// </summary>
		public int OrderIndex { get; set; }

		/// <summary>
		/// Gets or sets event handlers for this field.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public IList<EventHandlerMetadata> EventHandlers { get; set; }
	}
}