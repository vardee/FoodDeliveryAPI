using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using backendTask.DBContext.Models;
using Microsoft.AspNetCore.Http;

namespace YourNamespace
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext db)
        {
            try
            {
                await _next(db);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(db, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext db, Exception exception)
        {
            var response = db.Response;
            response.ContentType = "application/json";

            var statusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Internal Server Error";

            if (exception is Exceptions excception)
            {
                statusCode = (int)excception.StatusCode;
                message = excception.Message;
            }

            response.StatusCode = statusCode;

            var error = new Error
            {
                StatusCode = statusCode,
                Message = message,
                StackTrace = exception.StackTrace
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(error, options);

            await response.WriteAsync(json);
        }
    }
}



