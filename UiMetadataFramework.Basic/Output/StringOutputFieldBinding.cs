namespace UiMetadataFramework.Basic.Output
{
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class StringOutputFieldBinding : OutputFieldBinding
	{
		internal const string ControlName = "text";

		/// <inheritdoc />
		public StringOutputFieldBinding() : base(
			new[]
			{
				typeof(string),
				typeof(bool)
			},
			ControlName)
		{
		}
	}
}