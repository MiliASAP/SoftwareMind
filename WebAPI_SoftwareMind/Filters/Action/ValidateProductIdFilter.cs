using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using WebAPI_SoftwareMind.Services.BusinessLogic;

namespace WebAPI_SoftwareMind.Filters.Action
{
    public class ValidateProductIdFilter : IAsyncActionFilter
    {
        private readonly IProductService _productService;

        public ValidateProductIdFilter(IProductService productService)
        {
            _productService = productService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int? productId = null;

            if (context.ActionArguments.TryGetValue("id", out var idObj) && idObj is int id)
            {
                productId = id;
            }
            else
            {
                foreach (var arg in context.ActionArguments.Values)
                {
                    var prop = arg?.GetType().GetProperty("ProductId", BindingFlags.Public | BindingFlags.Instance);
                    if (prop != null && prop.GetValue(arg) is int pid)
                    {
                        productId = pid;
                        break;
                    }
                }
            }

            if (productId == null || productId <= 0)
            {
                context.ModelState.AddModelError("ProductId", "ProductId is invalid.");
                context.Result = new BadRequestObjectResult(new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                });
                return;
            }

            var exists = await _productService.ProductExistsAsync(productId.Value);
            if (!exists)
            {
                context.ModelState.AddModelError("ProductId", "Product doesn't exist.");
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
