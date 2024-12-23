namespace UiMetadataFramework.Basic.Inputs.DateTime
{
	using System;
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class DateTimeInputComponentBinding : ComponentBinding
	{
		internal const string ControlName = "datetime";

		/// <inheritdoc />
		public DateTimeInputComponentBinding() : base(
			MetadataBinder.ComponentCategories.Input,
			serverType: typeof(DateTime),
			componentType: ControlName,
			metadataFactory: null)
		{
		}
	}
}