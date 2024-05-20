using BookUniverse.Application.DTOs.BookDTOs;
using FluentResults;
using MediatR;

namespace BookUniverse.Application.MediatR.Books.Queries.GetAllBooksByUser
{
    public record GetAllBooksByUserQuery(string userId) : IRequest<Result<IEnumerable<BookDto>>>;
}
