using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OppJar.Core.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace OppJar.Core.Extensions
{
    public static class LogExtention
    {
        private const string CONTENT_TYPE = "application/json";

        public static IApplicationBuilder AddLog(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            var loggerFactory = (ILoggerFactory)app.ApplicationServices.GetService(typeof(ILoggerFactory));

            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                    context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, X-CSRF-Token, X-Requested-With, Accept, Accept-Version, Content-Length, Content-MD5, Date, X-Api-Version, X-File-Name");
                    context.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,PUT,PATCH,DELETE,OPTIONS");

                    var exception = context.Features.Get<IExceptionHandlerFeature>();

                    if (exception.Error is BadRequestException)
                    {
                        await BadRequest(context, exception);

                        return;
                    }

                    if (exception.Error is NotFoundException)
                    {
                        context.Response.StatusCode = 404;

                        await context.Response.WriteAsync(exception.Error.Message).ConfigureAwait(false);

                        return;
                    }

                    if (exception.Error is ForbiddenException)
                    {
                        context.Response.StatusCode = 403;

                        string errorMsg = string.IsNullOrEmpty(exception.Error.Message) ? "Forbidden" : exception.Error.Message;

                        await context.Response.WriteAsync(errorMsg).ConfigureAwait(false);

                        return;
                    }

                    var logger = loggerFactory.CreateLogger("ExceptionLogger");

                    logger.LogError($"Exception: {exception.Error.Message}, Stack trace: {exception.Error.StackTrace}");

                    await ServerInternal(context, exception, env);

                    return;

                });
            });

            return app;
        }

        private static async Task BadRequest(HttpContext context, IExceptionHandlerFeature exception)
        {
            context.Response.StatusCode = 400;

            context.Response.ContentType = CONTENT_TYPE;

            var badRequestException = exception.Error as BadRequestException;

            if (badRequestException.Errors == null)
            {
                var error = JsonConvert.SerializeObject(new { errors = exception.Error.Message });

                await context.Response.WriteAsync(error).ConfigureAwait(false);
            }
            else
            {
                var values = badRequestException.Errors.Select(d => string.Format("\"{0}\": \"{1}\"", d.Key.FirstCharToLower(), d.Value));

                await context.Response.WriteAsync("{" + string.Join(",", values) + "}").ConfigureAwait(false);
            }
        }

        private static async Task ServerInternal(HttpContext context,
            IExceptionHandlerFeature exception,
            IWebHostEnvironment env)
        {
            string error = string.Empty;

            if (env.IsDevelopment())
            {
                error = JsonConvert.SerializeObject(new
                {
                    errors = exception.Error.Message,
                    stackTrace = exception.Error.StackTrace,
                });
            }
            else
            {
                error = JsonConvert.SerializeObject(new
                {
                    errors = "Something when wrong please contact with administrator thanks",
                });
            }

            context.Response.StatusCode = 500;

            context.Response.ContentType = CONTENT_TYPE;

            await context.Response.WriteAsync(error).ConfigureAwait(false);

        }


        private static string FirstCharToLower(this string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            return input.First().ToString().ToLower() + input.Substring(1);
        }
    }
}
