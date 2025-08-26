using Microsoft.AspNetCore.Mvc.Filters;
using N_LayerBestPratice.Repository.Abstract;
using N_LayerBestPratice.Repository.Concrete;
using N_LayerBestPratice.Repository.DbContext;
using N_LayerBestPratice.Services.Results;

namespace N_LayerBestPratice.Services.Filters;

public class NotFoundFilter<T,TId> : Attribute, IAsyncActionFilter where T : BaseEntity<TId> where TId : struct
{
    private readonly IGenericRepository<T,TId> _repository;

    public NotFoundFilter(IGenericRepository<T, TId> repository)
    {
        _repository = repository;
    }


    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var idValue = context.ActionArguments.TryGetValue("id", out var idAsObject) ? idAsObject : null;

        if (idAsObject is not  TId id)
        {
            await next();
            return;
        }

        if (_repository.AnyByIdAsync(id).Result) // idyi bulursa devam et
        {
            await next();
            return;
        }
        // anyByIdAsync false dönerse burdaki işlemleri devam ediyoruz
        var entityName = typeof(T).Name;
        var actionName = context.ActionDescriptor.RouteValues["action"].ToString();
        
        var result = Result.Failure(ResultStatus.NotFound, new Error()
        {
            Code = $"{entityName} NotFound",
            Message = $"{entityName} with id {id} not found for {actionName} operation"
        });

        context.Result = new Microsoft.AspNetCore.Mvc.ObjectResult(result);
        return;
     
    }
}