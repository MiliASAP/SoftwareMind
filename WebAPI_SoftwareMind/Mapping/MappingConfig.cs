using Mapster;
using WebAPI_SoftwareMind.Models.Entities;
using WebAPI_SoftwareMind.Models.Entities.DTOs;

namespace WebAPI_SoftwareMind.Mapping
{
    public static class MappingConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<NegotiationInsertProductDTO, Negotiation>
                .NewConfig()
                .Map(dest => dest.ProductId, src => src.ProductId) 
                .Ignore(dest => dest.Product) 
                .IgnoreNullValues(true); 
        }
    }
}