
using WebAPI_SoftwareMind.Models.Entities.DTOs;

namespace WebAPI_SoftwareMind.Services
{
    public interface INegotiationService
    {
        Task<Negotiation> CreateNegotiationAsync(NegotiationDTO dto);
        Task<Negotiation> GetNegotiationByIdAsync(int id);
        Task<IQueryable<Negotiation>> GetAllNegotiationsAsync();
        Task UpdateNegotiationAsync(Negotiation negotiation);
        Task<Negotiation> DeleteNegotiationAsync(int negotiationId);

        Task<Negotiation> GetExistingNegotiationAsync(int productId);
    }
}