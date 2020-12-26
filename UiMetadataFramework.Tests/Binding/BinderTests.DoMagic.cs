namespace UiMetadataFramework.Tests.Binding
{
	using UiMetadataFramework.Core.Binding;

	public partial class BinderTests
	{
		[StringProperty("style", "blue")]
		[IntProperty("number", 1_001)]
        [BooleanProperty("bool-false", false)]
        [BooleanProperty("bool-true", true)]
		[MyForm(Id = "Magic", Label = "Do some magic", PostOnLoad = false, CloseOnPostIfModal = true)]
		[Documentation("help 1")]
		[Documentation("help 2")]
		public class DoMagic
		{
		}
	}
}