namespace UiMetadataFramework.Basic.Inputs.DateTime
{
	using System;
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class DateTimeInputComponentBinding : InputComponentBinding
	{
		internal const string ControlName = "datetime";

		/// <inheritdoc />
		public DateTimeInputComponentBinding() : base(
			serverType: typeof(DateTime),
			componentType: ControlName,
			metadataFactory: null)
		{
		}
	}
}