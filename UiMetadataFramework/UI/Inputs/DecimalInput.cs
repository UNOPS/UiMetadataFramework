namespace UiMetadataFramework.Core.UI.Inputs
{
	public class DecimalInput : Input
	{
		public DecimalInput(string name) : base(name, "decimal")
		{
		}

		public decimal? Steps { get; set; }
	}
}