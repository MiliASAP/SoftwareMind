using Mapster;
using Microsoft.EntityFrameworkCore;
using WebAPI_SoftwareMind.Models.Entities.DTOs;

namespace WebAPI_SoftwareMind.Services.BusinessLogic
{
    public class NegotiationService : INegotiationService
    {
        private readonly NegotiationDbContext _context;
        private readonly IProductService _productService;

        public NegotiationService(NegotiationDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public async Task<bool> NegotiationExistAsync(int id)
        {
            return await _context.Negotiations.AnyAsync(n => n.NegotiationId == id);
        }

        public async Task<Negotiation> GetExistingNegotiationAsync(int productId)
        {
            return await _context.Negotiations
                .Where(n => n.ProductId == productId)
                .FirstOrDefaultAsync();
        }

        public async Task<Negotiation> CreateNegotiationAsync(NegotiationInsertProductDTO dto)
        {
            var product = await _context.Products.FindAsync(dto.ProductId);
            if (product == null)
            {
                throw new ArgumentException("Product not found.");
            }
            var existingNegotiation = await GetExistingNegotiationAsync(dto.ProductId);

            if (existingNegotiation != null)
            {
                throw new InvalidOperationException("Negotiation for this product already exists.");
            }

            var negotiation = dto.Adapt<Negotiation>();
            negotiation.Product = product;
            negotiation.ProductId = product.ProductId;
            negotiation.Attempts = 1;

            _context.Negotiations.Add(negotiation);

            await _context.SaveChangesAsync();

            return existingNegotiation ?? await _context.Negotiations
                .FirstOrDefaultAsync(n => n.ProductId == dto.ProductId);
        }

        public async Task<Negotiation> NegotiateAsync(NegotiationNegotiateDTO dto)
        {
            var existingNegotiation = await _context.Negotiations
                .FirstOrDefaultAsync(n => n.NegotiationId == dto.NegotiationId);

            if (existingNegotiation == null)
            {
                throw new ArgumentException("Negotiation not found.");
            }

            if (existingNegotiation.Status != "Rejected")
            {
                throw new InvalidOperationException("Only rejected negotiations can be retried.");
            }

            if (existingNegotiation.Attempts >= 3)
            {
                await DeleteNegotiationAsync(dto.NegotiationId);
                throw new InvalidOperationException("Maximum number of negotiation attempts reached.");
            }

            existingNegotiation.ProposedPrice = dto.ProposedPrice;
            existingNegotiation.Attempts += 1;
            existingNegotiation.Status = "Pending";

            _context.Negotiations.Update(existingNegotiation);

            await _context.SaveChangesAsync();

            return existingNegotiation;
        }

        public async Task<Negotiation?> GetNegotiationByIdAsync(int id)
        {
            return await _context.Negotiations.Include(n => n.Product).FirstOrDefaultAsync(n => n.NegotiationId == id);
        }

        public async Task<List<Negotiation>> GetAllNegotiationsAsync()
        {
            return await _context.Negotiations.ToListAsync();
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

        public async Task<Negotiation> DecisionNegotiationAsync(NegotiationDecisionDTO dto)
        {
            var negotiation = await _context.Negotiations.FindAsync(dto.NegotiationId);

            if (negotiation == null || negotiation.Status != "Pending")
                return null;

            negotiation.Status = dto.Decision.ToLower() == "accept" ? "Accepted" : "Rejected";
            negotiation.ExpirationDate = DateTime.UtcNow.AddDays(7);

            _context.Negotiations.Update(negotiation);
            await _context.SaveChangesAsync();

            return negotiation;
        }
    }
}
