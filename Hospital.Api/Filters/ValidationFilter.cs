using FluentValidation;
using Hospital.Application.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hospital.Api.Filters;

public class ValidationFilter(IServiceProvider serviceProvider) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument is null)
                continue;

            var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
            var validator = serviceProvider.GetService(validatorType) as IValidator;

            if (validator is null)
                continue;

            var validationContext = new ValidationContext<object>(argument);
            var result = await validator.ValidateAsync(validationContext);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .Select(x => new ApiError
                    {
                        Campo = x.PropertyName,
                        Mensaje = x.ErrorMessage
                    })
                    .ToList();

                context.Result = new BadRequestObjectResult(
                    ApiResponseFactory.Fail(
                        "Errores de validación.",
                        errors
                    )
                );

                return;
            }
        }

        await next();
    }
}