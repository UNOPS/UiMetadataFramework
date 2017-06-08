namespace UiMetadataFramework.Core.Outputs
{
	public class HyperLinkProperty : PropertyMetadata
	{
		public const string Typename = "link";

		public HyperLinkProperty(string name) : base(name, Typename)
		{
		}
	}
}