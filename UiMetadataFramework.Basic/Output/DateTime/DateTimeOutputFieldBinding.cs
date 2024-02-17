namespace UiMetadataFramework.Basic.Output.DateTime
{
	using System;
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class DateTimeOutputFieldBinding : OutputFieldBinding
	{
		internal const string ControlName = "datetime";

		/// <inheritdoc />
		public DateTimeOutputFieldBinding() : base(
			serverType: typeof(DateTime),
			clientType: ControlName,
			mandatoryCustomProperty: null,
			metadataFactory: null)
		{
		}
	}
}