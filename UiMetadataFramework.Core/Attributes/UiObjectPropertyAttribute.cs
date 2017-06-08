namespace UiMetadataFramework.Core.Attributes
{
	using UiMetadataFramework.Core.Outputs;

	/// <summary>
	/// Marks property as a generic object to be rendered with default rendering rules.
	/// </summary>
	public class UiObjectPropertyAttribute : UiPropertyAttribute
	{
		public UiObjectPropertyAttribute()
		{
			this.Type = ObjectProperty.Typename;
		}
	}
}