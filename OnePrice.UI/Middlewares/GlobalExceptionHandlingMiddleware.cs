using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace OnePrice.UI.Middlewares
{
	public class GlobalExceptionHandlingMiddleware : IMiddleware
	{

		private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
		public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
		{
			_logger = logger;
		}


		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				ProblemDetails details = new()
				{
					Title = "Internal server error",
					Detail = "An internal server error has occured",
					Status = (int)HttpStatusCode.InternalServerError,
				};

				string errorHtml = $@"
					<html>
						<head>
							<title>{details.Title}</title>
						</head>
						<body>
							<h1>{details.Title} : {details.Status}</h1>
							<p>{details.Detail}</p>
							<button 
								style=""width: 100px; height: 50px;""
								onclick=""window.location.href='/'""
							>
								Back
							</button>
						</body>
					</html>
					";

				await context.Response.WriteAsync(errorHtml);
				context.Response.ContentType = "text/html";
			}
		}
	}
}
