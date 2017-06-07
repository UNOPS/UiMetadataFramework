namespace UiMetadataFramework.Core.UI.Actions
{
	using UiMetadataFramework.Core.Metadata;

	public class ActionListProperty : PropertyMetadata
	{
		public const string Typename = "action-list";

		public ActionListProperty(string name, params FormParameter[] parameters)
			: base(name, Typename, parameters)
		{
		}
	}
}