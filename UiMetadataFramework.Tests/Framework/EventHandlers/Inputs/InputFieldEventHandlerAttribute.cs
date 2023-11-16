namespace UiMetadataFramework.Tests.Framework.EventHandlers.Inputs;

using System;
using System.Reflection;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

public class InputFieldEventHandlerAttribute : Attribute, IFieldEventHandlerAttribute
{
	public const string Identifier = "input-field-function";

	public string Id => Identifier;
	public string RunAt => FormEvents.FormLoaded;
	public bool ApplicableToInputField => true;
	public bool ApplicableToOutputField => false;

	public EventHandlerMetadata ToMetadata(PropertyInfo property, MetadataBinder binder)
	{
		return new EventHandlerMetadata(this.Id, this.RunAt);
	}
}