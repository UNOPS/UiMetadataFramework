namespace UiMetadataFramework.Web.Metadata.ClientFunctions
{
	using UiMetadataFramework.Core.Binding;

	public class LogEventAsField : FieldEventHandlerAttribute
	{
		public LogEventAsField(string runAt) : base("log-to-console", runAt, true, true)
		{
		}
	}
}