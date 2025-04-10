using System.Threading.Tasks;
using System.Linq;
using WebAPI_SoftwareMind.Models.Entities;

namespace WebAPI_SoftwareMind.Services.BusinessLogic
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
        Task<bool> ProductExistsAsync(int id);
    }
}