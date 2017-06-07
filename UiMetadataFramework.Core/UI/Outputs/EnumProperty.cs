namespace UiMetadataFramework.Core.UI.Outputs
{
	using UiMetadataFramework.Core.Metadata;

	public class EnumProperty : PropertyMetadata
	{
		public const string Typename = "enum";

		public EnumProperty(string name) : base(name, Typename)
		{
		}
	}
}