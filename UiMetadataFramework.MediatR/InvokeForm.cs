namespace UiMetadataFramework.MediatR
{
	using System;
	using System.Linq;
	using System.Reflection;
	using System.Threading;
	using System.Threading.Tasks;
	using global::MediatR;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	/// <summary>
	/// Invokes form and returns its result.
	/// </summary>
	public class InvokeForm : IAsyncRequestHandler<InvokeForm.Request, InvokeForm.Response>
	{
		private readonly FormRegister formRegister;
		private readonly IMediator mediator;

		public InvokeForm(IMediator mediator, FormRegister formRegister)
		{
			this.mediator = mediator;
			this.formRegister = formRegister;
		}

		public async Task<Response> Handle(Request message)
		{
			// Get form type and interface.
			var formType = this.formRegister.GetFormInfo(message.Form);

			// Create request object.
			var request = message.InputFieldValues != null
				? JsonConvert.DeserializeObject(message.InputFieldValues.ToString(), formType.RequestType)
				: Activator.CreateInstance(formType.RequestType);

			// Send request via MediatR. Calling IForm instance directly won't apply the decorators, 
			// that's why MediatR is used instead.
			var method = this.mediator
				.GetType()
				.GetTypeInfo()
				.GetMethods()
				.Single(t => t.Name == nameof(Mediator.Send) && t.ReturnType.Name.Contains("Task`"))
				.MakeGenericMethod(formType.ResponseType);

			var result = await method.InvokeAsync(this.mediator, request, default(CancellationToken));

			return new Response
			{
				RequestId = message.RequestId,
				Data = result
			};
		}

		public class Request : IRequest<Response>
		{
			/// <summary>
			/// Gets or sets id of the form to retrieve.
			/// </summary>
			public string Form { get; set; }

			/// <summary>
			/// Gets or sets identifier for this request. When set, the value
			/// of <see cref="RequestId"/> will be set to the same value.
			/// </summary>
			/// <remarks>This property is useful if client combines multiple requests 
			/// and needs to know for which particular request the response is for.</remarks>
			public string RequestId { get; set; }

			/// <summary>
			/// Gets or sets form parameters.
			/// </summary>
			public JObject InputFieldValues { get; set; }
		}

		public class Response
		{
			public object Data { get; set; }
			public string RequestId { get; set; }
		}
	}
}