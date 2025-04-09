using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_SoftwareMind.Filters.Action;
using WebAPI_SoftwareMind.Models.Entities;
using WebAPI_SoftwareMind.Services;

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

    // GET: api/product
    [HttpGet]
    [AllowAnonymous]
    public async Task<IQueryable<Product>> GetAll()
    {
        return await _productService.GetAllProductsAsync();
    }

    // GET: api/product/5
    [HttpGet("{id}")]
    [AllowAnonymous]
    [ServiceFilter(typeof(Product_ValidateProductIdFilter))]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        return await _productService.GetProductByIdAsync(id);
    }

    // POST: api/product
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Product>> Create([FromBody] Product product)
    {
        var createdProduct = await _productService.CreateProductAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = createdProduct.ProductId }, createdProduct);
    }
}