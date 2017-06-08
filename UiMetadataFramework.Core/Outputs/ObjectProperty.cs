namespace UiMetadataFramework.Core.Outputs
{
	public class ObjectProperty : PropertyMetadata
	{
		public const string Typename = "object";

		public ObjectProperty(string name) : base(name, Typename)
		{
		}
	}
}