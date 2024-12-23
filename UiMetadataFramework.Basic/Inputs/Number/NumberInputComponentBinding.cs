namespace UiMetadataFramework.Basic.Inputs.Number
{
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class NumberInputComponentBinding : ComponentBinding
	{
		internal const string ControlName = "number";

		/// <inheritdoc />
		public NumberInputComponentBinding() : base(
			MetadataBinder.ComponentCategories.Input,
			serverTypes:
			[
				typeof(int),
				typeof(decimal),
				typeof(double),
				typeof(short),
				typeof(long),
				typeof(byte)
			],
			componentType: ControlName,
			metadataFactory: null)
		{
		}
	}
}