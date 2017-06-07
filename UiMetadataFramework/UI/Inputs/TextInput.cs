namespace UiMetadataFramework.Core.UI.Inputs
{
	public class TextInput : Input
	{
		public TextInput(string name) : base(name, "text")
		{
		}

		public int? MaxLength { get; set; }
		public int? MinLength { get; set; }
	}
}