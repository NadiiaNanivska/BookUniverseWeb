using AutoMapper;
using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Application.DTOs.CategoryDTOs;
using BookUniverse.Application.MediatR.Books.Queries.GetAllBooks;
using BookUniverse.Domain.Common;
using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Repositories.Base.UnitOfWork;
using FluentResults;
using MediatR;

namespace BookUniverse.Application.MediatR.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, Result<IEnumerable<CategoryDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCategoriesHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Category> catoegories = await _unitOfWork.CategoryRepository.GetAllAsync();

            if (catoegories is null)
            {
                return Result.Fail(new Error(ResponseMessagesConstants.NOTHING_FOUND));
            }

            return Result.Ok(_mapper.Map<IEnumerable<CategoryDto>>(catoegories));
        }
    }
}
