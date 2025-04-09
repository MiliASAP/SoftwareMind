using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI_SoftwareMind.Services;

namespace WebAPI_SoftwareMind.Filters.Action
{
    public class Product_ValidateProductIdFilter : IAsyncActionFilter
    {
        private readonly IProductService _productService;

        public Product_ValidateProductIdFilter(IProductService productService)
        {
            _productService = productService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.TryGetValue("id", out var idObj) && idObj is int id)
            {
                if (id <= 0)
                {
                    context.ModelState.AddModelError("ProductId", "ProductId is invalid.");
                    context.Result = new BadRequestObjectResult(new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    });
                    return;
                }

                var exists = await _productService.ProductExistsAsync(id);
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