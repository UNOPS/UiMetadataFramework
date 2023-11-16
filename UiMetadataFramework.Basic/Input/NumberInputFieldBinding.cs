namespace UiMetadataFramework.Basic.Input
{
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class NumberInputFieldBinding : InputFieldBinding
	{
		internal const string ControlName = "number";

		/// <inheritdoc />
		public NumberInputFieldBinding() : base(
			new[]
			{
				typeof(int),
				typeof(decimal),
				typeof(double),
				typeof(short),
				typeof(long),
				typeof(byte)
			},
			ControlName)
		{
		}
	}
}