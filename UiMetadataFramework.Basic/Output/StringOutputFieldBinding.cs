namespace UiMetadataFramework.Basic.Output
{
	using UiMetadataFramework.Core.Binding;

	public class StringOutputFieldBinding : OutputFieldBinding
	{
		public const string ControlName = "text";

		public StringOutputFieldBinding() : base(typeof(string), ControlName)
		{
		}
	}
}