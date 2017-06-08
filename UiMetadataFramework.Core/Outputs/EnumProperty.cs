namespace UiMetadataFramework.Core.Outputs
{
	public class EnumProperty : PropertyMetadata
	{
		public const string Typename = "enum";

		public EnumProperty(string name) : base(name, Typename)
		{
		}
	}
}