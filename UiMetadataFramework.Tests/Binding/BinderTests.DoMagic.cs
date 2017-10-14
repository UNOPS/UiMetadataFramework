namespace UiMetadataFramework.Tests.Binding
{
	public partial class BinderTests
	{
		[MyForm(Id = "Magic", Label = "Do some magic", PostOnLoad = false, CloseOnPostIfModal = true)]
		public class DoMagic
		{
		}
	}
}