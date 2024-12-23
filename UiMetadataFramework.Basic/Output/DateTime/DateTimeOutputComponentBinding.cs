namespace UiMetadataFramework.Basic.Output.DateTime
{
	using System;
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class DateTimeOutputComponentBinding : ComponentBinding
	{
		internal const string ControlName = "datetime";

		/// <inheritdoc />
		public DateTimeOutputComponentBinding() : base(
			MetadataBinder.ComponentCategories.Output,
			serverType: typeof(DateTime),
			componentType: ControlName,
			metadataFactory: null)
		{
		}
	}
}