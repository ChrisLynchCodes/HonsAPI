using AutoMapper;

namespace HonsBackendAPI.AutoMapperProfiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Models.Review, DTOs.ReviewDto>();
            CreateMap<DTOs.ReviewCreateDto, Models.Review>();
        }
        
    }
}
