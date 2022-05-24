
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using TaskAPI.Models;

namespace TaskAPI.Extensions
{
    public static class ExceptionHandlerExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(err =>
            {
                err.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error za testing purpose"
                        }.ToString());
                    }
                });
            });
        }
    }
}
