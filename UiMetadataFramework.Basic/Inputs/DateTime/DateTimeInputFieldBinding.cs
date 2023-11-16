namespace UiMetadataFramework.Basic.Inputs.DateTime
{
	using System;
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class DateTimeInputFieldBinding : InputFieldBinding
	{
		internal const string ControlName = "datetime";

		/// <inheritdoc />
		public DateTimeInputFieldBinding() : base(typeof(DateTime), ControlName)
		{
		}
	}
}