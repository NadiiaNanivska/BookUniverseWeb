using BookUniverse.Application.DTOs.CategoryDTOs;
using FluentResults;
using MediatR;

namespace BookUniverse.Application.MediatR.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(string categoryName) : IRequest<Result<CategoryDto>>;
}
