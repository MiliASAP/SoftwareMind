using Microsoft.EntityFrameworkCore;
using WebAPI_SoftwareMind.Models.Entities;

namespace WebAPI_SoftwareMind.Services
{
    public class ProductService : IProductService
    {
        private readonly NegotiationDbContext _context;

        public ProductService(NegotiationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            return product;
        }


        public async Task<IQueryable<Product>> GetAllProductsAsync()
        {
            return _context.Products.AsQueryable();
        }

        public async Task<bool> ProductExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.ProductId == id);
        }
    }
}