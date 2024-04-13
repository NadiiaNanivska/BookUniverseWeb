using BookUniverse.Application.DTOs.BookDTOs;
using FluentResults;
using MediatR;

namespace BookUniverse.Application.MediatR.Books.Queries.GetAllBooks
{
    public record GetAllBooksQuery() : IRequest<Result<IEnumerable<BookDto>>>;
}
