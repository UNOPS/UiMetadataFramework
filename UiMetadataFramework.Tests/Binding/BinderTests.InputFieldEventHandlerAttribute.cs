namespace UiMetadataFramework.Tests.Binding
{
	using System;
	using System.Reflection;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	public partial class BinderTests
	{
		public class InputFieldEventHandlerAttribute : Attribute, IFieldEventHandlerAttribute
		{
			public const string Identifier = "input-field-function";

			public string Id { get; } = Identifier;
			public string RunAt { get; } = FormEvents.FormLoaded;
			public bool ApplicableToInputField { get; } = true;
			public bool ApplicableToOutputField { get; } = false;

			public EventHandlerMetadata ToMetadata(PropertyInfo property, MetadataBinder binder)
			{
				return new EventHandlerMetadata(this.Id, this.RunAt);
			}
		}
	}
}