namespace UiMetadataFramework.Core
{
	/// <summary>
	/// Represents a source from which a value can be retrieved. This class can be useful
	/// for binding input field values to a specific data source.
	/// </summary>
	public class InputFieldSource
	{
		public InputFieldSource()
		{
		}

		public InputFieldSource(string type, string id)
		{
			this.Id = id;
			this.Type = type;
		}

		/// <summary>
		/// Gets or sets id of the item within the source.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets type of the source.
		/// </summary>
		public string Type { get; set; }
	}
}