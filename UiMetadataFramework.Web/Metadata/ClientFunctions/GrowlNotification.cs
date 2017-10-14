namespace UiMetadataFramework.Web.Metadata.ClientFunctions
{
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	public class GrowlNotification : IClientFunctionMetadataProvider
	{
		public GrowlNotification(string message, string style) : this(null, message, style)
		{
			this.Message = message;
			this.Style = style;
		}

		public GrowlNotification(string heading, string message, string style)
		{
			this.Heading = heading;
			this.Message = message;
			this.Style = style;
		}

		public string Heading { get; set; }
		public string Message { get; set; }
		public string Style { get; set; }

		public ClientFunctionMetadata GetClientFunctionMetadata()
		{
			return new ClientFunctionMetadata("growl", this);
		}

		public static class Styles
		{
			public const string Success = "success";
			public const string Warning = "warning";
			public const string Danger = "danger";
		}
	}
}