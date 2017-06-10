namespace UiMetadataFramework.BasicFields.Input
{
	using System;
	using UiMetadataFramework.Core.Binding;

	public class DateTimeInputFieldBinding : InputFieldBinding
	{
		public const string ControlName = "datetime";

		public DateTimeInputFieldBinding() : base(typeof(DateTime), ControlName)
		{
		}
	}
}