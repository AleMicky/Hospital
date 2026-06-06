using Hospital.Application.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hospital.Api.Filters;

public class ApiResponseFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(
        ResultExecutingContext context,
        ResultExecutionDelegate next)
    {
        if (context.Result is ObjectResult objectResult)
        {
            if (IsApiResponse(objectResult.Value))
            {
                await next();
                return;
            }

            var statusCode =
                objectResult.StatusCode
                ?? context.HttpContext.Response.StatusCode;

            var isSuccess = statusCode is >= 200 and < 300;

            object response;

            if (isSuccess)
            {
                response = ApiResponseFactory.Success(
                    objectResult.Value,
                    GetMessage(statusCode)
                );
            }
            else
            {
                response = ApiResponseFactory.Fail(
                    GetMessage(statusCode),
                    objectResult.Value
                );
            }

            context.Result = new ObjectResult(response)
            {
                StatusCode = statusCode
            };
        }

        await next();
    }

    private static bool IsApiResponse(object? value)
    {
        if (value is null)
            return false;

        var type = value.GetType();

        return type.IsGenericType &&
               type.GetGenericTypeDefinition() == typeof(ApiResponse<>);
    }

    private static string GetMessage(int statusCode)
    {
        return statusCode switch
        {
            200 => "Operación realizada correctamente.",
            201 => "Registro creado correctamente.",
            400 => "Solicitud incorrecta.",
            401 => "No autorizado.",
            403 => "Acceso denegado.",
            404 => "Recurso no encontrado.",
            500 => "Error interno del servidor.",
            _ => "Operación realizada."
        };
    }
}