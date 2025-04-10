using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using WebAPI_SoftwareMind.Services.BusinessLogic;

namespace WebAPI_SoftwareMind.Filters.Action
{
    public class ValidateNegotiationIdFilter : IAsyncActionFilter
    {
        private readonly INegotiationService _negotiationService;

        public ValidateNegotiationIdFilter(INegotiationService negotiationService)
        {
            _negotiationService = negotiationService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int? negotiationId = null;

            if (context.ActionArguments.TryGetValue("id", out var idObj) && idObj is int id)
            {
                negotiationId = id;
            }
            else
            {
                foreach (var arg in context.ActionArguments.Values)
                {
                    var prop = arg?.GetType().GetProperty("NegotiationId", BindingFlags.Public | BindingFlags.Instance);
                    if (prop != null && prop.GetValue(arg) is int pid)
                    {
                        negotiationId = pid;
                        break;
                    }
                }
            }

            if (negotiationId == null || negotiationId <= 0)
            {
                context.ModelState.AddModelError("NegotiationId", "NegotiationId is invalid.");
                context.Result = new BadRequestObjectResult(new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                });
                return;
            }

            var exists = await _negotiationService.NegotiationExistAsync(negotiationId.Value);
            if (!exists)
            {
                context.ModelState.AddModelError("NegotiationId", "Negotiation doesn't exist.");
                context.Result = new NotFoundObjectResult(new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status404NotFound
                });
                return;
            }

            await next();
        }
    }
}
