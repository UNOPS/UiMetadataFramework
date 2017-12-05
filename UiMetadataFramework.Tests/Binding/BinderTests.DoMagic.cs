namespace UiMetadataFramework.Tests.Binding
{
	using UiMetadataFramework.Core.Binding;

	public partial class BinderTests
	{
		[StringProperty("style", "blue")]
		[IntProperty("number", 1_001)]
		[MyForm(Id = "Magic", Label = "Do some magic", PostOnLoad = false, CloseOnPostIfModal = true)]
		public class DoMagic
		{
		}
	}
}