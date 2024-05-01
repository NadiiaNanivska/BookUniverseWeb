using BookUniverse.Application.DTOs.BookDTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookUniverse.Application.MediatR.Books.Queries.GetAllBooksByCategory
{
    public record GetAllBooksByCategoryQuery(int category) : IRequest<Result<IEnumerable<BookDto>>>;
}
