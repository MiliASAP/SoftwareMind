using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebAPI_SoftwareMind.Services;

namespace WebAPI_SoftwareMind.Filters.Action
{
    public class Negotiation_ValidateNagotiationCreateFilter : IAsyncActionFilter
    {
        private readonly IProductService _productService;

        public Negotiation_ValidateNagotiationCreateFilter(IProductService productService)
        {
            _productService = productService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                });
                return;
            }

            if (context.ActionArguments.TryGetValue("negotiation", out var negotiationObj) &&
                negotiationObj is Negotiation negotiation)
            {
                if (negotiation.ProductId <= 0)
                {
                    context.ModelState.AddModelError("ProductId", "ProductId is invalid.");
                    context.Result = new BadRequestObjectResult(new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    });
                    return;
                }

                var exists = await _productService.ProductExistsAsync(negotiation.ProductId);
                if (!exists)
                {
                    context.ModelState.AddModelError("ProductId", "Product doesn't exist.");
                    context.Result = new NotFoundObjectResult(new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound
                    });
                    return;
                }
            }

            await next(); 
        }
    }
}
