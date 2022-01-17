using AutoMapper;

namespace HonsBackendAPI.AutoMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Models.Product, DTOs.ProductDto>();

            CreateMap<DTOs.ProductCreateDto, Models.Product>();
        }

    }
}
