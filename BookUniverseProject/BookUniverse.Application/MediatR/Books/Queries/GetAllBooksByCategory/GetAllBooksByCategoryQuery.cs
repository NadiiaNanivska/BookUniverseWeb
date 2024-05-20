using BookUniverse.Application.DTOs.BookDTOs;
using FluentResults;
using MediatR;

namespace BookUniverse.Application.MediatR.Books.Queries.GetAllBooksByCategory
{
    public record GetAllBooksByCategoryQuery(int categoryId) : IRequest<Result<IEnumerable<BookDto>>>;
}
