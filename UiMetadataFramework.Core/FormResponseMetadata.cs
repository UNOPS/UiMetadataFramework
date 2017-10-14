namespace UiMetadataFramework.Core
{
	using System.Collections.Generic;
	using Newtonsoft.Json;

	/// <summary>
	/// Metadata describing how to handle the response.
	/// </summary>
	public class FormResponseMetadata
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FormResponseMetadata"/> class.
		/// </summary>
		public FormResponseMetadata()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FormResponseMetadata"/> class.
		/// </summary>
		public FormResponseMetadata(string handler)
		{
			this.Handler = handler;
		}

		/// <summary>
		/// Gets or sets functions that the client should run immediately after it receives this response.
		/// These functions will be run before the response is handled by the <see cref="Handler"/>.
		/// </summary>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public IList<ClientFunctionMetadata> FunctionsToRun { get; set; }

		/// <summary>
		/// Gets or sets name of the client-side handler which will be responsible for
		/// processing the <see cref="FormResponse{T}"/>.
		/// </summary>
		/// <remarks>Client can implement an arbitrary number of handlers,
		/// however usually it will at least need to have an "object" handler,
		/// which will simply render the response. Other handlers might include "redirect" handler,
		/// which will redirect to another form or URL.</remarks>
		public string Handler { get; internal set; }
	}
}