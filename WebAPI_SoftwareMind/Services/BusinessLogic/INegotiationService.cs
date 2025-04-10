using WebAPI_SoftwareMind.Models.Entities.DTOs;

namespace WebAPI_SoftwareMind.Services.BusinessLogic
{
    public interface INegotiationService
    {
        Task<Negotiation> CreateNegotiationAsync(NegotiationInsertProductDTO dto);
        Task<Negotiation> GetNegotiationByIdAsync(int id);
        Task<List<Negotiation>> GetAllNegotiationsAsync();
        Task<Negotiation> DeleteNegotiationAsync(int negotiationId);
        Task<bool> NegotiationExistAsync(int id);
        Task<Negotiation> GetExistingNegotiationAsync(int productId);
        Task<Negotiation> DecisionNegotiationAsync(NegotiationDecisionDTO dto);
        Task<Negotiation> NegotiateAsync(NegotiationNegotiateDTO dto);
    }
}