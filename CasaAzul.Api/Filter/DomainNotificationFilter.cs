using CasaAzul.Domain.Notification.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace CasaAzul.Api.Filter
{
    public class DomainNotificationFilter : IAsyncResultFilter
    {
        private readonly IDomainNotification _domainNotification;

        public DomainNotificationFilter(IDomainNotification domainNotification)
        {
            _domainNotification = domainNotification;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (!context.ModelState.IsValid || _domainNotification.HasNotifications)
            {
                var validations = !context.ModelState.IsValid ?
                    JsonConvert.SerializeObject(context.ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)) :
                    JsonConvert.SerializeObject(_domainNotification.Notifications
                        .Select(x => x.Value));

                var problemDetails = new ProblemDetails
                {
                    Title = "Bad Request",
                    Status = StatusCodes.Status400BadRequest,
                    Instance = context.HttpContext.Request.Path.Value,
                    Detail = validations
                };

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = "application/problem+json";

                using (var writer = new StreamWriter(context.HttpContext.Response.Body))
                {
                    new JsonSerializer().Serialize(writer, problemDetails);
                    await writer.FlushAsync();
                }

                return;
            }

            await next();
        }
    }
}
