namespace UiMetadataFramework.Core.UI.Actions
{
	public interface IFormLink
	{
		string Form { get; }
		string Link { get; }
		FormLinkTarget Target { get; }
	}
}