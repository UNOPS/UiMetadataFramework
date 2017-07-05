namespace UiMetadataFramework.Basic.Output
{
	using UiMetadataFramework.Core.Binding;

	public class NumberOutputFieldBinding : OutputFieldBinding
	{
		public const string ControlName = "number";

		public NumberOutputFieldBinding() : base(new[] { typeof(int), typeof(decimal), typeof(double), typeof(short), typeof(long), typeof(byte) },
			ControlName)
		{
		}
	}
}