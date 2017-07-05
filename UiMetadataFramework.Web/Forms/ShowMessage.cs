namespace UiMetadataFramework.Web.Forms
{
	using global::MediatR;
	using UiMetadataFramework.Basic.Response;
	using UiMetadataFramework.Core.Binding;
	using UiMetadataFramework.MediatR;

	[Form(Label = "Show message handler", PostOnLoad = false)]
	public class ShowMessage : IForm<ShowMessage.Request, ShowMessage.Response>
	{
		public Response Handle(Request message)
		{
			return new Response
			{
				Message = message.Text
			};
		}

		public class Request : IRequest<Response>
		{
			[InputField(Required = true)]
			public string Text { get; set; }
		}

		public class Response : MessageResponse
		{
		}
	}
}