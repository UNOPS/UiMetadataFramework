namespace UiMetadataFramework.Tests.Binding
{
	public partial class BinderTests
	{
		public class InvalidRequest
		{
			[OutputFieldEventHandler]
			public string? Name { get; set; }
		}
	}
}