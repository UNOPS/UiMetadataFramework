namespace UiMetadataFramework.Core.Inputs
{
	public class TextInput : InputMetadata
	{
		public TextInput(string name) : base(name, "text")
		{
		}

		public int? MaxLength { get; set; }
		public int? MinLength { get; set; }
	}
}