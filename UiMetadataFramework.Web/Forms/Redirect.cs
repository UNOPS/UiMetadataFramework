namespace UiMetadataFramework.Web.Forms
{
	using global::MediatR;
	using UiMetadataFramework.BasicFields.Response;
	using UiMetadataFramework.Core.Binding;
	using UiMetadataFramework.MediatR;

	[Form(Label = "Redirect handler", PostOnLoad = false)]
	public class Redirect : IForm<Redirect.Request, Redirect.Response>
	{
		public Response Handle(Request message)
		{
			return new Response
			{
				Form = message.Form
			};
		}

		public class Request : IRequest<Response>
		{
			[InputField(Label = "Id of the form to redirect to", Required = true)]
			public string Form { get; set; }
		}

		public class Response : RedirectResponse
		{
		}
	}
}