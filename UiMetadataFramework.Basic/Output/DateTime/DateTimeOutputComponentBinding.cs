namespace UiMetadataFramework.Basic.Output.DateTime
{
	using System;
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class DateTimeOutputComponentBinding : OutputComponentBinding
	{
		internal const string ControlName = "datetime";

		/// <inheritdoc />
		public DateTimeOutputComponentBinding() : base(
			serverType: typeof(DateTime),
			clientType: ControlName,
			metadataFactory: null)
		{
		}
	}
}