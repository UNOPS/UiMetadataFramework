namespace UiMetadataFramework.Tests.Binding
{
	public partial class BinderTests
	{
		public class InvalidResponse
		{
			[InputFieldEventHandler]
			public string? Name { get; set; }
		}
	}
}