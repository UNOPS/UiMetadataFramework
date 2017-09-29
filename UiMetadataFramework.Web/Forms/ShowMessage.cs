namespace UiMetadataFramework.Web.Forms
{
	using global::MediatR;
	using UiMetadataFramework.Basic.Output;
	using UiMetadataFramework.Basic.Response;
	using UiMetadataFramework.Core.Binding;
	using UiMetadataFramework.MediatR;

	[Form(Id = "Message", Label = "Show message handler", PostOnLoad = false)]
	public class ShowMessage : IForm<ShowMessage.Request, ShowMessage.Response>
	{
		public Response Handle(Request message)
		{
			return new Response
			{
				Message = message.Text
			};
		}

		public static FormLink FormLink(string label)
		{
			return new FormLink
			{
				Label = label,
				Form = typeof(ShowMessage).GetFormId()
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