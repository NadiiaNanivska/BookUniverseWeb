using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Application.DTOs.CategoryDTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookUniverse.Application.MediatR.UserBooks.Commands.CreateUserBook
{
    public record CreateUserBookCommand(UserBookDto userBook) : IRequest<Result<Unit>>;

}
