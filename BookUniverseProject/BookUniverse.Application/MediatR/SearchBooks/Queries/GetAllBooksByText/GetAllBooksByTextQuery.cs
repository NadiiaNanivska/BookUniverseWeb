using FluentResults;
using MediatR;

namespace BookUniverse.Application.MediatR.SearchBooks.Queries.GetAllBooksByText
{
    public record GetAllBooksByTextQuery(string queryString) : IRequest<Result<IEnumerable<string>>>;
}
