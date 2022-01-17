using AutoMapper;

namespace HonsBackendAPI.AutoMapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Models.Category, DTOs.CategoryDto>();
            CreateMap<DTOs.CategoryCreateDto, Models.Category>();

        }
        

    }
}
