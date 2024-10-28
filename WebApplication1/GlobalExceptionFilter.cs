using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Microsoft.Data.SqlClient; 
using System.Text.Json;
using log4net;

public class GlobalExceptionFilter : IExceptionFilter
{
    private static readonly ILog _logger = LogManager.GetLogger(typeof(GlobalExceptionFilter));
    public void OnException(ExceptionContext context)
    {
       

        ObjectResult response = null;

       
        var exception = context.Exception;

        
        switch (exception)
        {
            case ArgumentNullException argNullException:
                response = new ObjectResult(new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "A required argument was null.",
                    Detail = argNullException.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                break;

            case ArgumentException argException:
                response = new ObjectResult(new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "There was an issue with the provided argument.",
                    Detail = argException.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                break;

            case InvalidOperationException invOpException:
                response = new ObjectResult(new
                {
                    StatusCode = (int)HttpStatusCode.Conflict,
                    Message = "The operation is invalid.",
                    Detail = invOpException.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
                break;

            case UnauthorizedAccessException unAuthException:
                response = new ObjectResult(new
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Message = "Unauthorized access.",
                    Detail = unAuthException.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };
                break;

            case FileNotFoundException fileNotFoundException:
                response = new ObjectResult(new
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "File not found.",
                    Detail = fileNotFoundException.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.NotFound
                };
                break;
            case SqlException sqlException when sqlException.Number == 2627:
                // Handling duplicate data error (unique constraint violation)
                response = new ObjectResult(new
                {
                    StatusCode = (int)HttpStatusCode.Conflict,
                    Message = "Duplicate data error. The data you are trying to insert already exists.",
                    Detail = sqlException.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
                break;

            case SqlException sqlException:
                response = new ObjectResult(new
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "A database error occurred.",
                    Detail = sqlException.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                break;

            

            case HttpRequestException httpRequestException:
                response = new ObjectResult(new
                {
                    StatusCode = (int)HttpStatusCode.BadGateway,
                    Message = "An HTTP request error occurred.",
                    Detail = httpRequestException.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.BadGateway
                };
                break;

            case TimeoutException timeoutException:
                response = new ObjectResult(new
                {
                    StatusCode = (int)HttpStatusCode.RequestTimeout,
                    Message = "The request timed out.",
                    Detail = timeoutException.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.RequestTimeout
                };
                break;

            case FormatException formatException:
                response = new ObjectResult(new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "There was a format issue.",
                    Detail = formatException.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                break;

            case JsonException jsonException:
                response = new ObjectResult(new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "There was a JSON processing error.",
                    Detail = jsonException.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                break;

            case KeyNotFoundException keyNotFoundException:
                response = new ObjectResult(new
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Key not found.",
                    Detail = keyNotFoundException.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.NotFound
                };
                break;

            case InvalidDataException invalidDataException:
                response = new ObjectResult(new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Invalid data error.",
                    Detail = invalidDataException.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                break;

            case NotImplementedException notImplementedException:
                response = new ObjectResult(new
                {
                    StatusCode = (int)HttpStatusCode.NotImplemented,
                    Message = "Feature not implemented.",
                    Detail = notImplementedException.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.NotImplemented
                };
                break;

            case Exception generalException:
                response = new ObjectResult(new
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "An unexpected error occurred. Please try again later.",
                    Detail = generalException.Message
                })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                break;
        }

        
        if (response != null)
        {
            context.Result = response;
        }

        _logger.Error("An error occurred", exception);
    }
}
