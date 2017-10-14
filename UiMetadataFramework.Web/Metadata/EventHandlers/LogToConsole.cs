namespace UiMetadataFramework.Web.Metadata.EventHandlers
{
	using System;
	using System.Reflection;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	public class LogToConsole : Attribute, IFieldEventHandlerAttribute, IFormEventHandlerAttribute
	{
		public LogToConsole(string runAt)
		{
			this.Id = "log-to-console";
			this.RunAt = runAt;
			this.ApplicableToInputField = true;
			this.ApplicableToOutputField = true;
		}

		public string Id { get; }
		public string RunAt { get; }
		public bool ApplicableToInputField { get; }
		public bool ApplicableToOutputField { get; }

		public EventHandlerMetadata ToMetadata(PropertyInfo property, MetadataBinder binder)
		{
			return new EventHandlerMetadata(this.Id, this.RunAt);
		}

		public EventHandlerMetadata ToMetadata(Type formType, MetadataBinder binder)
		{
			return new EventHandlerMetadata(this.Id, this.RunAt);
		}
	}
}