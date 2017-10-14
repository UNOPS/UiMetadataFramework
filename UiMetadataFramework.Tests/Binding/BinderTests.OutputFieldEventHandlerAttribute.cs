namespace UiMetadataFramework.Tests.Binding
{
	using System;
	using System.Reflection;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	public partial class BinderTests
	{
		public class OutputFieldEventHandlerAttribute : Attribute, IFieldEventHandlerAttribute
		{
			public const string Identifier = "output-field-function";

			public string Id { get; } = Identifier;
			public string RunAt { get; } = FormEvents.FormLoaded;
			public bool ApplicableToInputField { get; } = false;
			public bool ApplicableToOutputField { get; } = true;

			public EventHandlerMetadata ToMetadata(PropertyInfo property, MetadataBinder binder)
			{
				return new EventHandlerMetadata(this.Id, this.RunAt);
			}
		}
	}
}