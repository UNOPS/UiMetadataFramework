namespace UiMetadataFramework.Core.Outputs
{
	public class ActionListProperty : PropertyMetadata
	{
		public const string Typename = "action-list";

		public ActionListProperty(string name)
			: base(name, Typename)
		{
		}
	}
}