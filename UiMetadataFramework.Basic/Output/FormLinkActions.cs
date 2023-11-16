namespace UiMetadataFramework.Basic.Output
{
	/// <summary>
	/// Describes available actions for <see cref="FormLink.Action"/>.
	/// </summary>
	public static class FormLinkActions
	{
		/// <summary>
		/// Indicates that form should be run immediately after user has clicked on the <see cref="FormLink"/>.
		/// User should not be able to modify <see cref="FormLink.InputFieldValues"/>.
		/// </summary>
		public const string Run = "run";

		/// <summary>
		/// Indicates that the form should not be run immediately after user has clicked on the <see cref="FormLink"/>.
		/// Instead the form should be opened in a modal allowing user to review it before running.
		/// </summary>
		public const string OpenModal = "open-modal";

		/// <summary>
		/// Indicates that <see cref="FormLink"/> should behave as a regular link simply redirecting client to the specified
		/// form. 
		/// </summary>
		public const string Redirect = "redirect";
	}
}