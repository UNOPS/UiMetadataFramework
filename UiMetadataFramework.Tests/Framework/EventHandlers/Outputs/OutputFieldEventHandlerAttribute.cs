namespace UiMetadataFramework.Tests.Framework.EventHandlers.Outputs;

using System;
using System.Reflection;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

public class OutputFieldEventHandlerAttribute : Attribute, IFieldEventHandlerAttribute
{
	public const string Identifier = "output-field-function";

	public string Id => Identifier;
	public string RunAt => FormEvents.FormLoaded;
	public bool ApplicableToInputField => false;
	public bool ApplicableToOutputField => true;

	public EventHandlerMetadata ToMetadata(PropertyInfo property, MetadataBinder binder)
	{
		return new EventHandlerMetadata(this.Id, this.RunAt);
	}
}