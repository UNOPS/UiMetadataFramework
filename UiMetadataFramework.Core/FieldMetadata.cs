// ReSharper disable UnusedMemberInSuper.Global

namespace UiMetadataFramework.Core
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Newtonsoft.Json;

	/// <summary>
	/// Represents a wrapper around a component that can be used to indicate
	/// how a component should be rendered in a broader context of a form/etc.
	/// </summary>
	public class FieldMetadata
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FieldMetadata"/> class
		/// by copying existing metadata.
		/// </summary>
		/// <param name="metadata">Metadata to copy.</param>
		public FieldMetadata(FieldMetadata metadata) : this(metadata.Component)
		{
			if (metadata == null)
			{
				throw new ArgumentNullException(nameof(metadata));
			}

			this.Id = metadata.Id;
			this.Label = metadata.Label;
			this.OrderIndex = metadata.OrderIndex;
			this.Hidden = metadata.Hidden;
			this.EventHandlers = metadata.EventHandlers?.Select(t => t.Copy()).ToList();

			this.CustomProperties = metadata.CustomProperties != null
				? new Dictionary<string, object?>(metadata.CustomProperties)
				: null;

			this.Configuration = metadata.Configuration;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FieldMetadata"/> class.
		/// </summary>
		public FieldMetadata(Component component)
		{
			this.Component = component;
		}

		/// <summary>
		/// Gets or sets the component to be used.
		/// </summary>
		public Component Component { get; }

		/// <summary>
		/// Gets or sets additional parameters for this field.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public IDictionary<string, object?>? CustomProperties { get; set; }

		/// <summary>
		/// Gets or sets event handlers for this field.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public IList<EventHandlerMetadata>? EventHandlers { get; set; }

		/// <summary>
		/// Gets or sets value indicating whether this field should be visible or not.
		/// </summary>
		public bool Hidden { get; set; }

		/// <summary>
		/// Gets or sets id of the field to which this metadata belongs.
		/// </summary>
		public string? Id { get; set; }

		/// <summary>
		/// Gets or sets label for the field.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string? Label { get; set; }

		/// <summary>
		/// Gets or sets value which will dictate rendering position of this field
		/// in relationship to other fields.
		/// </summary>
		public int OrderIndex { get; set; }
		
		/// <summary>
		/// Configuration describing how the field should look/behave.
		/// May be null if the field does not require any configuration.
		/// </summary>
		public object? Configuration { get; set; }

		/// <inheritdoc />
		public override string ToString()
		{
			return this.ToDescriptiveString();
		}
	}
}