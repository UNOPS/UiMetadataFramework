namespace UiMetadataFramework.Basic.Output.InlineForm
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Represents a form which will be rendered as part of response.
	/// </summary>
	[OutputComponent("inline-form")]
	public class InlineForm
	{
		/// <summary>
		/// Gets or sets name of the form to render.
		/// </summary>
		public string Form { get; set; } = null!;

		/// <summary>
		/// Gets or sets values for the input fields of the form (i.e. - <see cref="FormMetadata.InputFields"/>).
		/// </summary>
		public IDictionary<string, object?>? InputFieldValues { get; set; }
	}
}