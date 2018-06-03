using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CatchMe.Api.Framework
{
    public class ErrorHandlerMiddleware
    {
		private readonly RequestDelegate _next;

		public ErrorHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
		}
		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch(Exception exception)
			{
				await HandleErrorAsync(context, exception);
			}
		}

		private static Task HandleErrorAsync(HttpContext context, Exception exception)
		{
			//var exceptionType = exception.GetType();
			//var statusCode = HttpStatusCode.InternalServerError;
			//switch(exceptionType)
			//{
			//	case typeof(UnauthorizedAccessException):
			//			statusCode = HttpStatusCode.Unauthorized;
			//			break;

			//	case typeof(ArgumentException):
			//			statusCode = HttpStatusCode.BadRequest;
			//			break;




			//}

			//var response = new { message = exception.Message };
			//var payload = JsonConvert.SerializeObject(response);
			//context.Response.ContentType = "application/json";
			//context.Response.StatusCode = (int)statusCode;

			//return context.Response.WriteAsync(payload);

			return context.Response.WriteAsync("TEST middleware implementation");
		}

    }
}
