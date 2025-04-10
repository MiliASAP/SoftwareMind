using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_SoftwareMind.Filters.Action;
using WebAPI_SoftwareMind.Models.Entities;
using WebAPI_SoftwareMind.Services.BusinessLogic;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly NegotiationDbContext _context;
    private readonly IProductService _productService;

    public ProductController(NegotiationDbContext context, IProductService productService)
    {
        _context = context;
        _productService = productService;
    }

    /// <summary>
    /// Retrieves a list of all products.
    /// </summary>
    /// <returns>A list of products.</returns>
    /// <response code="200">Returns the list of products</response>
    /// <response code="500">If there is an internal server error</response>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        return await _productService.GetAllProductsAsync();
    }


    /// <summary>
    /// Gets a product by its unique id.
    /// </summary>
    /// <param name="id">The unique id of the product.</param>
    /// <returns>Returns the product with the specified ID.</returns>
    /// <response code="200">Returns the product</response>
    /// <response code="404">If the product is not found</response>
    [HttpGet("{id}")]
    [AllowAnonymous]
    [ServiceFilter(typeof(ValidateProductIdFilter))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        return await _productService.GetProductByIdAsync(id);
    }
    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="product">The product to create.</param>
    /// <response code="201">Returns the created product</response>
    /// <response code="400">If the input is invalid</response>
    /// <response code="401">If the user is not authorized</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Product>> Create([FromBody] Product product)
    {
        var createdProduct = await _productService.CreateProductAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = createdProduct.ProductId }, createdProduct);
    }
}