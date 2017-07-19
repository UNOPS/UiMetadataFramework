namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// Used for decorating input fields for specifying default values.
	/// </summary>
	public class DefaultValueAttribute : Attribute
	{
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