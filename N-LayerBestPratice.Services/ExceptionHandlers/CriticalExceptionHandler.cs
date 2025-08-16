using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace N_LayerBestPratice.Services.ExceptionHandlers;

public class CriticalExceptionHandler : IExceptionHandler
{
    // private readonly ILogger<CriticalExceptionHandler> _logger = logger;

    private readonly ILogger<CriticalExceptionHandler> _logger;

    public CriticalExceptionHandler(ILogger<CriticalExceptionHandler> logger)
    {
        _logger = logger;
    }

    // False doneceğiz en son global exception handler'a geçecek ve orda true doenceğiz bir Result dönerek
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        if (exception is CriticalException)
        {
            _logger.LogCritical(exception.Message);
        }
        
        // Eğer exception CriticalException ise, loglayıp false döneceğiz
        return new ValueTask<bool>(false);
    }
}