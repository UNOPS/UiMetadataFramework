namespace UiMetadataFramework.Basic.Inputs.Number
{
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class NumberInputComponentBinding : InputComponentBinding
	{
		internal const string ControlName = "number";

		/// <inheritdoc />
		public NumberInputComponentBinding() : base(
			serverTypes: new[]
			{
				typeof(int),
				typeof(decimal),
				typeof(double),
				typeof(short),
				typeof(long),
				typeof(byte)
			},
			componentType: ControlName,
			metadataFactory: null)
		{
		}
	}
}