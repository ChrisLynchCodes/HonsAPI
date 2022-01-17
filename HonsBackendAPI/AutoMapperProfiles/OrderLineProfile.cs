using AutoMapper;

namespace HonsBackendAPI.AutoMapperProfiles
{
    public class OrderLineProfile : Profile
    {
        public OrderLineProfile()
        {
            CreateMap<Models.OrderLine, DTOs.OrderLineDto>();
            CreateMap<DTOs.OrderLineCreateDto, Models.OrderLine>();

           
        }
    }
}
