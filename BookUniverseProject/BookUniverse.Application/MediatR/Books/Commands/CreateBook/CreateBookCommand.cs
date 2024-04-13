using BookUniverse.Application.DTOs.BookDTOs;
using FluentResults;
using MediatR;

namespace BookUniverse.Application.MediatR.Books.Commands.CreateBook
{
    public record CreateBookCommand(AddBookDto newBook) : IRequest<Result<AddBookDto>>;
}
