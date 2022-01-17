using AutoMapper;

namespace HonsBackendAPI.AutoMapperProfiles
{
    public class AdminProfile : Profile
    {

        public AdminProfile()
        {
            CreateMap<Models.Admin, DTOs.AdminDto>();
            CreateMap<DTOs.AdminCreateDto, Models.Admin>();
        }
    }
}
