namespace UiMetadataFramework.Basic.Output
{
	using System;
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class DateTimeOutputFieldBinding : OutputFieldBinding
	{
		internal const string ControlName = "datetime";

		/// <inheritdoc />
		public DateTimeOutputFieldBinding() : base(typeof(DateTime), ControlName)
		{
		}
	}
}