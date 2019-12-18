namespace Atm.Api.Middleware
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Application.Errors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            object errors = null;

            switch (exception)
            {
                case RestException restException:
                    this.logger.LogError(exception, "REST ERROR");
                    errors = restException.Errors;
                    context.Response.StatusCode = (int)restException.Code;
                    break;

                case { } ex:
                    this.logger.LogError(ex, "SERVER ERROR");
                    errors = string.IsNullOrEmpty(ex.Message) ? "Error" : ex.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            if (errors != null)
            {
                var result = JsonConvert.SerializeObject(new
                {
                    errors
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
}
