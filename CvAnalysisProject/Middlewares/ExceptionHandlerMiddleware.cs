using CvAnalysisSystem.Common.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System.Text.Json;
using CvAnalysisSystem.Common.GlobalResponses;

namespace CvAnalysisSystemProject.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception errors)
        {
            switch (errors)
            {
                case BadRequestException:
                    var message = new List<string>() { errors.Message };
                    await WriteError(context, HttpStatusCode.BadRequest, message);
                    break;

                case TooManyRequestException:
                    message = [errors.Message];
                    await WriteError(context, HttpStatusCode.TooManyRequests, message);
                    break;
                case NotFoundException:
                    message = [errors.Message];
                    await WriteError(context, HttpStatusCode.NotFound, message);
                    break;
                case UnauthorizedAccessException:
                    message = [errors.Message];
                    await WriteError(context, HttpStatusCode.Unauthorized, message);
                    break;
                default:
                    message = [errors.Message];
                    await WriteError(context, HttpStatusCode.InternalServerError, message);
                    break;
            }
        }
    }
    public static async Task WriteError(HttpContext context, HttpStatusCode statusCode, List<string> messages)
    {
        context.Response.Clear();
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var json = JsonSerializer.Serialize(new Result(messages));
        await context.Response.WriteAsync(json);
    }
}