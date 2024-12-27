namespace UiMetadataFramework.Core.Binding
{
	/// <summary>
	/// Attribute used for decorating output fields.
	/// </summary>
	public class OutputFieldAttribute : FieldAttribute
	{
		/// <inheritdoc />
		public OutputFieldAttribute() : base(MetadataBinder.ComponentCategories.Output)
		{
		}
	}
}