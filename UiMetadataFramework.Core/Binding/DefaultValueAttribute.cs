namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// Used for decorating input fields for specifying default values.
	/// </summary>
	public class DefaultValueAttribute : Attribute
	{
		/// <summary>
		/// Configures default value for the input field to be a constant.
		/// </summary>
		/// <param name="value">Default value.</param>
		public DefaultValueAttribute(string value)
		{
			this.Type = "const";
			this.Id = value;
		}

		/// <summary>
		/// Configures default value for the input field to be taken from a data source.
		/// </summary>
		/// <param name="type">Name of the data source.</param>
		/// <param name="id">Id of the item within the data source.</param>
		public DefaultValueAttribute(string type, string id)
		{
			this.Type = type;
			this.Id = id;
		}

		/// <summary>
		/// Gets or sets id of the item within the source.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets type of the source.
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// Creates a new instance of <see cref="InputFieldSource"/>, with the mapping
		/// specified by this attribute.
		/// </summary>
		/// <returns></returns>
		public InputFieldSource AsInputFieldSource()
		{
			return new InputFieldSource(this.Type, this.Id);
		}
	}
}