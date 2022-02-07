using System;
using System.Net.Mime;
using System.Threading.Tasks;
using iPractice.Abstraction.Exceptions;
using iPractice.ApiBase.Response;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Formatting = System.Xml.Formatting;
using ValidationException = iPractice.Abstraction.Exceptions.ValidationException;
using ValidationProblemDetails = iPractice.Abstraction.Exceptions.ValidationProblemDetails;
using ProblemDetails = iPractice.Abstraction.Exceptions.ProblemDetails;


namespace iPractice.ApiBase.Extension
{
    internal static class HttpContextExtensions
    {
        internal static Task HandleExceptionAsync(this HttpContext context,
            Exception exception)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;

            BaseProblemDetails details = exception switch
            {
                BusinessException ex => new ProblemDetails(ex)
                {
                    Status = StatusCodes.Status422UnprocessableEntity,
                    Title = "UnprocessableEntity",
                    ErrorCode = StatusCodes.Status422UnprocessableEntity
                },
                ValidationException ex => new ValidationProblemDetails(ex)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation Exception",
                    ErrorCode = StatusCodes.Status400BadRequest
                },
                _ => new ProblemDetails(exception)
                {
                    StackTrace = exception.StackTrace ?? string.Empty,
                    Message = exception.Message,
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Unknown Exception",
                }
            };
            return context.ProblemDetailResponseAsync(details);
        }

        private static Task ProblemDetailResponseAsync<T>(this HttpContext context, T details) where T : BaseProblemDetails
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = details.Status;
            
            var result = Response<T>.Fail(details, details.Status);

            var responseText = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy(),
                },
                Formatting = (Newtonsoft.Json.Formatting) Formatting.Indented,
            });
            return context.Response.WriteAsync(responseText);
        }
    }
}