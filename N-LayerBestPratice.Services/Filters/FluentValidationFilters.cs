using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using N_LayerBestPratice.Services.Results;

namespace N_LayerBestPratice.Services.Filters;

public class FluentValidationFilters : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.SelectMany(v => v.Value.Errors)
                .Select(ve => new Error()
                {
                    Message = ve.ErrorMessage,
                    Code = ve.Exception?.Message ?? "ValidationError"
                }).ToList();
            var result = Result.Failure(ResultStatus.ValidationError, errors.ToArray());
            context.Result = new BadRequestObjectResult(result);
            return;
        }

        await next();
    }
}