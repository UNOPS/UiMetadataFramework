namespace UiMetadataFramework.Basic.Server
{
	using System;
	using System.Collections.Concurrent;
	using System.Linq;
	using System.Reflection;
	using System.Threading;
	using System.Threading.Tasks;
	using MediatR;
	using Newtonsoft.Json;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Invokes forms and returns its result.
	/// </summary>
	public class FormRunner
	{
		private static readonly ConcurrentDictionary<Type, MethodInfo> MediatorSendMethod = new();
		private readonly FormRegister formRegister;
		private readonly IMediator mediator;
		private readonly MethodInfo method;

		/// <summary>
		/// Initializes a new instance of the <see cref="FormRunner"/> class.
		/// </summary>
		public FormRunner(IMediator mediator, FormRegister formRegister)
		{
			this.mediator = mediator;
			this.formRegister = formRegister;

			this.method = MediatorSendMethod.GetOrAdd(
				this.mediator.GetType(),
				c =>
				{
					return c
						.GetTypeInfo()
						.GetMethods()
						.ToArray()
						.Where(t => t.Name == nameof(Mediator.Send))
						.Where(t => t.GetParameters().FirstOrDefault()?.ParameterType == typeof(object))
						.Single(t => t.ReturnType.Name.Contains("Task`"));
				});
		}

		/// <summary>
		/// Runs a form and returns its response.
		/// </summary>
		/// <param name="request">An object whose serialized version will represent a form's request.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>Form's response.</returns>
		/// <exception cref="ArgumentException">Thrown if form cannot be found.</exception>
		public Task<object> Run<TForm>(object? request, CancellationToken cancellationToken)
		{
			var formId = typeof(TForm).GetFormId();

			return this.RunForm(formId, request, cancellationToken);
		}

		/// <summary>
		/// Runs a form and returns its response.
		/// </summary>
		/// <param name="form">Form to run.</param>
		/// <param name="request">An object whose serialized version will represent a form's request.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>Form's response.</returns>
		/// <exception cref="ArgumentException">Thrown if form cannot be found.</exception>
		public Task<object> RunForm(
			string form,
			object? request,
			CancellationToken cancellationToken)
		{
			// Get form type and interface.
			var formType = this.formRegister.GetFormInfo(form);

			if (formType == null)
			{
				throw new ArgumentException($"Form '{form}' was not found.");
			}

			// Create request object.
			var formRequest = request != null
				? JsonConvert.DeserializeObject(JsonConvert.SerializeObject(request), formType.RequestType)
				: Activator.CreateInstance(formType.RequestType);

			// Send request via MediatR. Calling IForm instance directly won't apply the decorators, 
			// that's why MediatR is used instead.
			return this.method.InvokeAsync(
				this.mediator,
				formRequest,
				cancellationToken);
		}
	}
}