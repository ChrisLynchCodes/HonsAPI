using AutoMapper;

namespace HonsBackendAPI.AutoMapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Models.Order, DTOs.OrderDto>();

            CreateMap<DTOs.OrderCreateDto, Models.Order>();
        }
    }
}
