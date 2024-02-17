namespace UiMetadataFramework.Core
{
	using System.Collections.Generic;
	using System.Linq;
	using Newtonsoft.Json;
	using UiMetadataFramework.Core.Binding;

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
		/// Creates a copy of the specified instance.
		/// </summary>
		/// <param name="metadata">Instance to copy.</param>
		protected OutputFieldMetadata(OutputFieldMetadata metadata) : this(metadata.Type)
		{
			this.Id = metadata.Id;
			this.Label = metadata.Label;
			this.OrderIndex = metadata.OrderIndex;
			this.Hidden = metadata.Hidden;
			this.EventHandlers = metadata.EventHandlers?.Select(t => t.Copy()).ToList();

			this.CustomProperties = metadata.CustomProperties != null
				? new Dictionary<string, object?>(metadata.CustomProperties)
				: null;
		}

		/// <summary>
		/// Gets or sets metadata for the component to be displayed by this field. 
		/// </summary>
		public object? ComponentConfiguration { get; set; }

		/// <summary>
		/// Gets or sets additional parameters for the client control.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public IDictionary<string, object?>? CustomProperties { get; set; }

		/// <summary>
		/// Gets or sets id of the field to which this metadata belongs.
		/// </summary>
		public string? Id { get; set; }

		/// <summary>
		/// Gets or sets label for the output field.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string? Label { get; set; }

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
		public IList<EventHandlerMetadata>? EventHandlers { get; set; }

		/// <summary>
		/// Gets <see cref="ComponentConfiguration"/> making sure it is of type <typeparamref name="T"/>.
		/// If the <see cref="ComponentConfiguration"/> is null or is not of type <typeparamref name="T"/>,
		/// then an exception is thrown. 
		/// </summary>
		public T GetComponentConfigurationOrException<T>() where T : class
		{
			if (this.ComponentConfiguration is not T result)
			{
				throw new BindingException($"Component metadata for '{this.Id}' is not of type '{typeof(T).FullName}'.");
			}

			return result;
		}
	}
}