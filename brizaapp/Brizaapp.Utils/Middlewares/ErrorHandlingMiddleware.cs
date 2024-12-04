using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Brizaapp.Utils.Middlewares
{
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
        await next(context);
      }
      catch (TaskCanceledException ex1)
      {
        logger.LogDebug(ex1, nameof(TaskCanceledException));
      }
      catch (Exception ex)
      {
        await HandleExceptionAsync(context, ex);
      }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      object errorBody = new
      {
        errors = new string[] { exception.Message },
        traceId = context.TraceIdentifier
      };

      var code = HttpStatusCode.InternalServerError; 

      if (exception is UserDataException)
      {
        code = HttpStatusCode.BadRequest;
        var ex = (UserDataException)exception;

        if (ex.ResponseObject != null && ex.ResponseObject is Array && ((Array)ex.ResponseObject).Length > 0)
        {
          errorBody = new { errors = ex.ResponseObject, traceId = context.TraceIdentifier };
        }

        logger.LogWarning(exception, "BadRequest - TraceId: {traceId}", context.TraceIdentifier);
      }


      //TODO: agregar exception
      //else if (exception is UserDataException)
      //{
      //  code = HttpStatusCode.BadRequest;
      //  var ex = (UserDataException)exception;

      //  if (ex.ResponseObject != null)
      //  {
      //    errorBody = new { errors = ex.ResponseObject, traceId = context.TraceIdentifier };
      //  }

      //  logger.LogWarning(exception, "BadRequest - TraceId: {traceId}", context.TraceIdentifier);
      //}
      //else if (exception is ForbiddenException)
      //{
      //  code = HttpStatusCode.Forbidden;
      //  logger.LogWarning(exception, "Forbidden - TraceId: {traceId}", context.TraceIdentifier);
      //}
      //else if (exception is TimeoutException)
      //{
      //  code = HttpStatusCode.GatewayTimeout;
      //  logger.LogError(exception, "GatewayTimeout - TraceId: {traceId}", context.TraceIdentifier);
      //}
      //else if (exception is BadGatewayException)
      //{
      //  code = HttpStatusCode.BadGateway;
      //  logger.LogError(exception, "BadGateway - TraceId: {traceId}", context.TraceIdentifier);
      //}
      //else
      //{
      //  NLogger.Error(exception, "InternalServerError - TraceId: {traceId}", context.TraceIdentifier);
      //}

      var result = JsonConvert.SerializeObject(errorBody);
      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)code;
      await context.Response.WriteAsync(result);
    }
  }
}
