namespace UiMetadataFramework.Tests.Binding
{
	using UiMetadataFramework.Core.Binding;

	public partial class BinderTests
	{
		[StringProperty("style", "blue")]
		[IntProperty("number", 1_001)]
		[MyForm(Id = "Magic", Label = "Do some magic", PostOnLoad = false, CloseOnPostIfModal = true)]
		[Documentation("help 1")]
		[Documentation("help 2")]
		public class DoMagic
		{
		}
	}
}