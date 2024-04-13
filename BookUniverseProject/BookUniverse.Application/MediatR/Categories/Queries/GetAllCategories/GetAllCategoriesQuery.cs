using BookUniverse.Application.DTOs.CategoryDTOs;
using FluentResults;
using MediatR;

namespace BookUniverse.Application.MediatR.Categories.Queries.GetAllCategories
{
    public record GetAllCategoriesQuery() : IRequest<Result<IEnumerable<CategoryDto>>>;
}
