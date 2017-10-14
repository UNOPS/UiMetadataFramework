namespace UiMetadataFramework.Web.Metadata.ClientFunctions
{
	using UiMetadataFramework.MediatR;

	public class LogEventAsForm : FormEventHandlerAttribute
	{
		public LogEventAsForm(string runAt) : base("log-to-console", runAt)
		{
		}
	}
}