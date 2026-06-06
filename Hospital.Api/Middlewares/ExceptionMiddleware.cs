using System.Net;
using System.Text.Json;
using Hospital.Application.Common;
using Hospital.Application.Exceptions;

namespace Hospital.Api.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (BadRequestException ex)
        {
            await WriteErrorAsync(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (UnauthorizedException ex)
        {
            await WriteErrorAsync(context, HttpStatusCode.Unauthorized, ex.Message);
        }
        catch (NotFoundException ex)
        {
            await WriteErrorAsync(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (ConflictException ex)
        {
            await WriteErrorAsync(context, HttpStatusCode.Conflict, ex.Message);
        }
        catch (Exception ex)
        {
            await WriteErrorAsync(context, HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    private static async Task WriteErrorAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = ApiResponseFactory.Fail(message);

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}