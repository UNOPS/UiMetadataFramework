namespace UiMetadataFramework.Basic.Output.Number
{
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class NumberOutputComponentBinding : OutputComponentBinding
	{
		internal const string ControlName = "number";

		/// <inheritdoc />
		public NumberOutputComponentBinding() : base(
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