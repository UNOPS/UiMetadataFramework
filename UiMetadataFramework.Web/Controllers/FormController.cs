namespace UiMetadataFramework.Web.Controllers
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using global::MediatR;
	using Microsoft.AspNetCore.Cors;
	using Microsoft.AspNetCore.Mvc;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.MediatR;

	[Route("api/form")]
	[EnableCors(Startup.CorsAllowAllPolicy)]
	public class FormController : Controller
	{
		private readonly FormRegister formRegister;
		private readonly IMediator mediator;
		private const string ContentType = "application/json";

		public FormController(IMediator mediator, FormRegister formRegister)
		{
			this.mediator = mediator;
			this.formRegister = formRegister;
		}

		[HttpPost("run")]
		public async Task<List<InvokeForm.Response>> Run([FromBody] IEnumerable<InvokeForm.Request> requests)
		{
			var results = new List<InvokeForm.Response>();
			foreach (var request in requests)
			{
				var response = await this.mediator.Send(request);
				results.Add(new InvokeForm.Response
				{
					RequestId = request.RequestId,
					Data = response.Data
				});
			}

			this.Response.ContentType = ContentType;
			return results;
		}

		[HttpGet("metadata/{id}")]
		public FormMetadata Metadata(string id)
		{
			this.Response.ContentType = ContentType;
			return this.formRegister.GetFormInfo(id)?.Metadata;
		}

		[HttpGet("metadata")]
		public IEnumerable<FormMetadata> Metadata()
		{
			this.Response.ContentType = ContentType;
			return this.formRegister.RegisteredForms.Select(t => t.Metadata);
		}
	}
}