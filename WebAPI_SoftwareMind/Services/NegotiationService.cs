using Mapster;
using Microsoft.EntityFrameworkCore;
using WebAPI_SoftwareMind.Models.Entities;
using WebAPI_SoftwareMind.Models.Entities.DTOs;

namespace WebAPI_SoftwareMind.Services
{
    public class NegotiationService : INegotiationService
    {
        private readonly NegotiationDbContext _context;
        private readonly IProductService _productService;
        private readonly INegotiationService _negotiationService;

        public NegotiationService(NegotiationDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public async Task<Negotiation> GetExistingNegotiationAsync(int productId)
        {
            return await _context.Negotiations
                .Where(n => n.ProductId == productId)
                .FirstOrDefaultAsync();
        }

        public async Task<Negotiation> CreateNegotiationAsync(NegotiationDTO dto)
        {
            var product = await _context.Products.FindAsync(dto.ProductId);
            var existingNegotiation = await GetExistingNegotiationAsync(dto.ProductId);

            if (existingNegotiation != null)
            {
                existingNegotiation.ProposedPrice = dto.ProposedPrice;
                existingNegotiation.Attempts += 1;
                if (existingNegotiation.Attempts >= 3)
                {
                    return await DeleteNegotiationAsync(existingNegotiation.NegotiationId);
                }
                existingNegotiation.Status = "Pending"; 

                _context.Negotiations.Update(existingNegotiation);
            }
            else
            {
                var negotiation = dto.Adapt<Negotiation>();
                negotiation.Product = product;
                negotiation.ProductId = product.ProductId;
                negotiation.Attempts = 1;  

                _context.Negotiations.Add(negotiation);
            }

            await _context.SaveChangesAsync();

            return existingNegotiation ?? await _context.Negotiations
                .FirstOrDefaultAsync(n => n.ProductId == dto.ProductId);
        }
        


        public async Task<Negotiation?> GetNegotiationByIdAsync(int id)
        {
            return await _context.Negotiations.Include(n => n.Product).FirstOrDefaultAsync(n => n.NegotiationId == id);
        }

        public async Task<IQueryable<Negotiation>> GetAllNegotiationsAsync()
        {
            return _context.Negotiations.Include(n => n.Product).AsQueryable();
        }

        public async Task UpdateNegotiationAsync(Negotiation negotiation)
        {
            _context.Negotiations.Update(negotiation);
            await _context.SaveChangesAsync();
        }

        public async Task<Negotiation> DeleteNegotiationAsync(int negotiationId)
        {
            var negotiation = await _context.Negotiations
                .FirstOrDefaultAsync(n => n.NegotiationId == negotiationId);

            if (negotiation == null)
            {
                throw new InvalidOperationException("Negotiation not found.");
            }

            _context.Negotiations.Remove(negotiation);
            await _context.SaveChangesAsync();

            return negotiation; 
        }
    }
}
