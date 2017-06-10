namespace UiMetadataFramework.BasicFields.Input
{
	using UiMetadataFramework.Core.Binding;

	public class NumberInputFieldBinding : InputFieldBinding
	{
		public const string ControlName = "number";

		public NumberInputFieldBinding() : base(new[] { typeof(int), typeof(decimal), typeof(double), typeof(short), typeof(long), typeof(byte) },
			ControlName)
		{
		}
	}
}