namespace BookUniverse.Application.Mapping.Books
{
    using AutoMapper;
    using BookUniverse.Application.DTOs.BookDTOs;
    using BookUniverse.Domain.Entities;

    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<AddBookDto, Book>()
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
                .AfterMap((src, dest, opt) =>
                {
                    if (opt.Items.TryGetValue("CategoryId", out var categoryId))
                    {
                        dest.CategoryId = (int)categoryId;
                    }
                })
                .ReverseMap();
        }
    }
}
