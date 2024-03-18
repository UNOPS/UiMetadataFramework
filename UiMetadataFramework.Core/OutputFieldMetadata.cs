namespace UiMetadataFramework.Core
{
	using System.Collections.Generic;
	using System.Linq;
	using Newtonsoft.Json;

	/// <summary>
	/// <see cref="IFieldMetadata"/> for an output component.
	/// </summary>
	public class OutputFieldMetadata : IFieldMetadata
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OutputFieldMetadata"/> class.
		/// </summary>
		public OutputFieldMetadata(Component component)
		{
			this.Component = component;
		}

		/// <summary>
		/// Creates a copy of the specified instance.
		/// </summary>
		/// <param name="metadata">Instance to copy.</param>
		protected OutputFieldMetadata(OutputFieldMetadata metadata) : this(metadata.Component)
		{
			this.Id = metadata.Id;
			this.Label = metadata.Label;
			this.OrderIndex = metadata.OrderIndex;
			this.Hidden = metadata.Hidden;
			this.EventHandlers = metadata.EventHandlers?.Select(t => t.Copy()).ToList();
			this.Component = metadata.Component;

			this.CustomProperties = metadata.CustomProperties != null
				? new Dictionary<string, object?>(metadata.CustomProperties)
				: null;
		}

		/// <inheritdoc />
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public IDictionary<string, object?>? CustomProperties { get; set; }

		/// <inheritdoc />
		public string? Id { get; set; }

		/// <inheritdoc />
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string? Label { get; set; }

		/// <inheritdoc />
		public bool Hidden { get; set; }

		/// <inheritdoc />
		public int OrderIndex { get; set; }

		/// <inheritdoc />
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public IList<EventHandlerMetadata>? EventHandlers { get; set; }

		/// <inheritdoc />
		public Component Component { get; }

		/// <inheritdoc />
		public override string ToString()
		{
			return this.ToDescriptiveString();
		}
	}
}