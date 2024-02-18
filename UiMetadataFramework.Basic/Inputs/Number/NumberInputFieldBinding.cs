namespace UiMetadataFramework.Basic.Inputs.Number
{
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class NumberInputFieldBinding : InputFieldBinding
	{
		internal const string ControlName = "number";

		/// <inheritdoc />
		public NumberInputFieldBinding() : base(
			serverTypes: new[]
			{
				typeof(int),
				typeof(decimal),
				typeof(double),
				typeof(short),
				typeof(long),
				typeof(byte)
			},
			clientType: ControlName,
			metadataFactory: null)
		{
		}
	}
}