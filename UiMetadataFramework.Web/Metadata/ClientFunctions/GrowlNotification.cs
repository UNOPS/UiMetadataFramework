namespace UiMetadataFramework.Web.Metadata.ClientFunctions
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	public class GrowlNotification
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
			var customProperties = new Dictionary<string, object>()
				.Set(nameof(this.Heading), this.Heading)
				.Set(nameof(this.Message), this.Heading)
				.Set(nameof(this.Style), this.Style);

			return new ClientFunctionMetadata("growl", customProperties);
		}

		public static class Styles
		{
			public const string Success = "success";
			public const string Warning = "warning";
			public const string Danger = "danger";
		}
	}
}