namespace UiMetadataFramework.Web.Middleware
{
	using System;
	using System.Net;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Http;
	using Newtonsoft.Json;

	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate next;

		public ErrorHandlingMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await this.next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var result = JsonConvert.SerializeObject(new
			{
				error = exception.GetBaseException().Message
			});

			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			return context.Response.WriteAsync(result);
		}
	}
}