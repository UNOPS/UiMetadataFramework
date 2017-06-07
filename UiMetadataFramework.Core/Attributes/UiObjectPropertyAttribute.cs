namespace UiMetadataFramework.Core.Attributes
{
	using UiMetadataFramework.Core.UI.Outputs;

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