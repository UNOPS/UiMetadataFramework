namespace UiMetadataFramework.Core.UI.Outputs
{
	using UiMetadataFramework.Core.Metadata;

	public class ObjectProperty : PropertyMetadata
	{
		public const string Typename = "object";

		public ObjectProperty(string name) : base(name, Typename)
		{
		}
	}
}