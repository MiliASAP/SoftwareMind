using System.Threading.Tasks;
using System.Linq;
using WebAPI_SoftwareMind.Models.Entities;

namespace WebAPI_SoftwareMind.Services
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int id);
        Task<IQueryable<Product?>> GetAllProductsAsync();
        Task<bool> ProductExistsAsync(int id);
    }
}