namespace UiMetadataFramework.Core.Inputs
{
	public class DecimalInput : InputMetadata
	{
		public DecimalInput(string name) : base(name, "decimal")
		{
		}

		public decimal? Steps { get; set; }
	}
}