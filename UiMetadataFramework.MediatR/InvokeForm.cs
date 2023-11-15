namespace UiMetadataFramework.MediatR
{
	using System;
	using System.Linq;
	using System.Reflection;
	using System.Threading;
	using System.Threading.Tasks;
	using global::MediatR;
	using Newtonsoft.Json;

	/// <summary>
	/// Invokes form and returns its result.
	/// </summary>
	public class InvokeForm : IRequestHandler<InvokeForm.Request, InvokeForm.Response>
	{
		private readonly FormRegister formRegister;
		private readonly IMediator mediator;

		/// <summary>
		/// Initializes a new instance of the <see cref="InvokeForm"/> class.
		/// </summary>
		public InvokeForm(IMediator mediator, FormRegister formRegister)
		{
			this.mediator = mediator;
			this.formRegister = formRegister;
		}

		/// <inheritdoc />
		public async Task<Response> Handle(Request message, CancellationToken cancellationToken)
		{
			// Get form type and interface.
			var formType = this.formRegister.GetFormInfo(message.Form);

			if (formType == null)
			{
				throw new ArgumentException($"Form '{message.Form}' was not found.");
			}

			// Create request object.
			var request = message.InputFieldValues != null
				? JsonConvert.DeserializeObject(JsonConvert.SerializeObject(message.InputFieldValues), formType.RequestType)
				: Activator.CreateInstance(formType.RequestType);

			// Send request via MediatR. Calling IForm instance directly won't apply the decorators, 
			// that's why MediatR is used instead.
			var method = this.mediator
				.GetType()
				.GetTypeInfo()
				.GetMethods()
				.Where(t => t.Name == nameof(Mediator.Send))
				.Where(t => t.GetParameters().FirstOrDefault()?.ParameterType.Name.Contains("IRequest`") == true)
				.Single(t => t.ReturnType.Name.Contains("Task`"))
				.MakeGenericMethod(formType.ResponseType);

			var result = await method.InvokeAsync(this.mediator, request, default(CancellationToken));

			return new Response
			{
				RequestId = message.RequestId,
				Data = result
			};
		}

		/// <inheritdoc />
		public class Request : IRequest<Response>
		{
			/// <summary>
			/// Gets or sets id of the form to retrieve.
			/// </summary>
			public string Form { get; set; } = null!;

			/// <summary>
			/// Gets or sets form parameters.
			/// </summary>
			public object? InputFieldValues { get; set; }

			/// <summary>
			/// Gets or sets identifier for this request. When set, the value
			/// of <see cref="RequestId"/> will be set to the same value.
			/// </summary>
			/// <remarks>This property is useful if client combines multiple requests 
			/// and needs to know for which particular request the response is for.</remarks>
			public string? RequestId { get; set; }
		}

		/// <summary>
		/// Response for the received request.
		/// </summary>
		public class Response
		{
			/// <summary>
			/// The response object returned by the invoked form.
			/// </summary>
			public object? Data { get; set; }

			/// <summary>
			/// The request id that corresponds to the request id of the
			/// received request (<see cref="Request.RequestId"/>).
			/// </summary>
			public string? RequestId { get; set; }
		}
	}
}