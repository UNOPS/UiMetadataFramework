namespace UiMetadataFramework.Core
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Newtonsoft.Json;

	/// <summary>
	/// <see cref="IFieldMetadata"/> for an input component.
	/// </summary>
	public class InputFieldMetadata : IFieldMetadata
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InputFieldMetadata"/> class.
		/// </summary>
		public InputFieldMetadata(Component component)
		{
			this.Component = component;
		}

		/// <summary>
		/// Creates a deep copy of the specified <see cref="InputFieldMetadata"/>.
		/// </summary>
		/// <param name="metadata">Instance to copy.</param>
		protected InputFieldMetadata(InputFieldMetadata metadata) : this(metadata.Component)
		{
			if (metadata == null)
			{
				throw new ArgumentNullException(nameof(metadata));
			}

			this.Id = metadata.Id;
			this.Hidden = metadata.Hidden;
			this.Label = metadata.Label;
			this.Required = metadata.Required;
			this.OrderIndex = metadata.OrderIndex;
			this.EventHandlers = metadata.EventHandlers?.Select(t => t.Copy()).ToList();

			this.CustomProperties = metadata.CustomProperties != null
				? new Dictionary<string, object?>(metadata.CustomProperties)
				: null;
		}

		/// <summary>
		/// Indicates if a value for this input field is required.
		/// </summary>
		public bool Required { get; set; }

		/// <inheritdoc />
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public IDictionary<string, object?>? CustomProperties { get; set; }

		/// <inheritdoc />
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public IList<EventHandlerMetadata>? EventHandlers { get; set; }

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
		public Component Component { get; }
	}
}