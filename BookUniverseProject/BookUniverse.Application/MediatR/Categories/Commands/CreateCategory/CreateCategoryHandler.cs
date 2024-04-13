using AutoMapper;
using BookUniverse.Application.DTOs.CategoryDTOs;
using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Repositories.Base.UnitOfWork;
using FluentResults;
using MediatR;

namespace BookUniverse.Application.MediatR.Categories.Commands.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Result<CategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.categoryName) || string.IsNullOrEmpty(request.categoryName))
            {
                string errorMsg = "Not valid category name";
                return Result.Fail(new Error(errorMsg));
            }

            Category newCategory = new Category() { CategoryName = request.categoryName };
            var entity = _unitOfWork.CategoryRepository.Create(newCategory);

            var resultIsSuccess = await _unitOfWork.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(_mapper.Map<CategoryDto>(entity));
            }
            else
            {
                string errorMsg = "Error occurred while creating a category";
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
