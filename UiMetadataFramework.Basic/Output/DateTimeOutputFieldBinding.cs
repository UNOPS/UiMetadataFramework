namespace UiMetadataFramework.Basic.Output
{
	using System;
	using UiMetadataFramework.Core.Binding;

	public class DateTimeOutputFieldBinding : OutputFieldBinding
	{
		public const string ControlName = "datetime";

		public DateTimeOutputFieldBinding() : base(typeof(DateTime), ControlName)
		{
		}
	}
}