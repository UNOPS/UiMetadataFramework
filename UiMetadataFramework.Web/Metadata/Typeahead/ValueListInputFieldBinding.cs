namespace UiMetadataFramework.Web.Metadata.Typeahead
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// This input field doesn't actually need a client-side control, as it's only
	/// used programmatically by the typeahead and never actually needs to be rendered
	/// for the user to interact with directly.
	/// </summary>
	public class ValueListInputFieldBinding : InputFieldBinding
	{
		public ValueListInputFieldBinding() : base(typeof(ValueList<>), "value-list")
		{
		}
	}

	public class ValueList<T>
	{
		public IList<T> Items { get; set; }
	}
}