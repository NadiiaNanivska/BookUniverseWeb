namespace BookUniverse.Application.Mapping.Books
{
    using AutoMapper;
    using BookUniverse.Application.DTOs.BookDTOs;
    using BookUniverse.Domain.Entities;

    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();

            CreateMap<AddBookDto, Book>().ReverseMap();
        }
    }
}
