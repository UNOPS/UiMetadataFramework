namespace UiMetadataFramework.Core.UI.Actions
{
	public enum FormLinkTarget
	{
		/// <summary>
		/// target is modal. Means modal output and input have to be the same 
		/// to be able to render output and input property which will help
		/// in editing values.
		/// </summary>
		Modal,

		NestedForm,

		/// <summary>
		/// No target, meaning action should be executed immediately without
		/// any additional input. If any required inputs don't have values then
		/// an associated form will be presented to the user.
		/// </summary>
		Action,
		Link
	}
}