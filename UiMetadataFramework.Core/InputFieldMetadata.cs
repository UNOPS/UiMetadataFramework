namespace UiMetadataFramework.Core
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Newtonsoft.Json;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// <see cref="IFieldMetadata"/> for an input component.
	/// </summary>
	public class InputFieldMetadata : IFieldMetadata
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InputFieldMetadata"/> class.
		/// </summary>
		/// <param name="type">Name of the component to be used.</param>
		public InputFieldMetadata(string type)
		{
			this.Type = type;
		}

		/// <summary>
		/// Creates a deep copy of the specified <see cref="InputFieldMetadata"/>.
		/// </summary>
		/// <param name="metadata">Instance to copy.</param>
		protected InputFieldMetadata(InputFieldMetadata metadata) : this(metadata.Type)
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
		public string Type { get; protected set; }

		/// <inheritdoc />
		public bool Hidden { get; set; }

		/// <inheritdoc />
		public int OrderIndex { get; set; }

		/// <inheritdoc />
		public object? ComponentConfiguration { get; set; }

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

		/// <summary>
		/// Gets <see cref="ComponentConfiguration"/> making sure it is not null.
		/// </summary>
		/// <exception cref="BindingException">Throw if <see cref="ComponentConfiguration"/> is null.</exception>
		public object GetComponentConfigurationOrException()
		{
			if (this.ComponentConfiguration == null)
			{
				throw new BindingException($"Field '{this.Id}' does not have any component configuration.");
			}

			return this.ComponentConfiguration;
		}
	}
}