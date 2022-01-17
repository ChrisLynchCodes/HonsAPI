using AutoMapper;

namespace HonsBackendAPI.AutoMapperProfiles
{
    public class CustomerProfile : Profile
    {

        public CustomerProfile()
        {

            CreateMap<Models.Customer, DTOs.CustomerDto>();
            CreateMap<DTOs.CustomerCreateDto, Models.Customer>();


            //CreateMap<Models.Customer, DTOs.CustomerDto>()
            //    .ForMember(dest => dest.Fullname,
            //    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}
