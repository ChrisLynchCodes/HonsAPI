using AutoMapper;

namespace HonsBackendAPI.AutoMapperProfiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Models.Address, DTOs.AddressDto>();
            CreateMap<DTOs.AddressCreateDto, Models.Address>();
        }
    }
}
