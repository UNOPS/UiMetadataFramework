namespace UiMetadataFramework.Core
{
	using System.Collections.Generic;

	/// <summary>
	/// Represents a client-side function.
	/// </summary>
	public class ClientFunctionMetadata
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ClientFunctionMetadata"/> class.
		/// </summary>
		/// <param name="id"></param>
		public ClientFunctionMetadata(string id) : this(id, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ClientFunctionMetadata"/> class.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="customProperties"></param>
		public ClientFunctionMetadata(string id, IDictionary<string, object> customProperties)
		{
			this.Id = id;
			this.CustomProperties = customProperties;
		}

		/// <summary>
		/// Gets or sets custom properties describing how to run the function.
		/// </summary>
		public IDictionary<string, object> CustomProperties { get; set; }

		/// <summary>Gets or sets id of the function.</summary>
		public string Id { get; }
	}
}