using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using N_LayerBestPratice.Services.Results;

namespace N_LayerBestPratice.Services.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (httpContext.Response.HasStarted)
        {
            // Eğer response başlatıldıysa, yani response body yazılmaya başlandıysa, 
            // bu durumda exception'ı handle edemeyiz çünkü response body zaten yazılıyor.
            _logger.LogWarning("Response has started");
            return false;
        }
        _logger.LogError(exception, "Unhandled exception reached GlobalExceptionHandler. TraceId: {TraceId}", httpContext.TraceIdentifier);
        
        var problemDetails = new ProblemDetails
        {
            Title = "An error occurred while processing your request.",
            Detail = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
            Instance = httpContext.Request.Path
        };
        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);
        
        var errorDto = Result<ProblemDetails>.Success(ResultStatus.InternalServerError,problemDetails);
        
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = "application/problem+json";
        
        await httpContext.Response.WriteAsJsonAsync(errorDto, cancellationToken: cancellationToken);

        return true;
    }
}