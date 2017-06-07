namespace UiMetadataFramework.Core.UI.Outputs
{
	using UiMetadataFramework.Core.Metadata;

	public class TabHyperLinkProperty : PropertyMetadata
	{
		public const string Typename = "tab-link";

		public TabHyperLinkProperty(string name) : base(name, Typename)
		{
		}
	}
}