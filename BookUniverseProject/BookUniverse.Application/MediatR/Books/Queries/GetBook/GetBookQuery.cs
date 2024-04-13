using BookUniverse.Application.DTOs.BookDTOs;
using FluentResults;
using MediatR;

namespace BookUniverse.Application.MediatR.Books.Queries.GetBook
{
    public record GetBookQuery(int id) : IRequest<Result<BookDto>>;
}
