namespace UiMetadataFramework.Core.UI.Outputs
{
	using UiMetadataFramework.Core.Metadata;

	public class HyperLinkProperty : PropertyMetadata
	{
		public const string Typename = "link";

		public HyperLinkProperty(string name) : base(name, Typename)
		{
		}
	}
}