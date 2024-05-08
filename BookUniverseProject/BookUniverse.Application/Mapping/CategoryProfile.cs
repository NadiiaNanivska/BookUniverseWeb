using AutoMapper;
using BookUniverse.Application.DTOs.CategoryDTOs;
using BookUniverse.Domain.Entities;

namespace BookUniverse.Application.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
