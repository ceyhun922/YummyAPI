using AutoMapper;
using YummyAPI.DTOs.CategoryDTO;
using YummyAPI.Entities;

namespace YummyAPI.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category,ResultCategoryDTOs>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTOs>().ReverseMap();
        }
    }
}