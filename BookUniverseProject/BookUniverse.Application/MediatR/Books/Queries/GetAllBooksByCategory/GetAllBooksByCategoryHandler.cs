using AutoMapper;
using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Application.MediatR.Books.Queries.GetAllBooks;
using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Repositories.Base.UnitOfWork;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookUniverse.Application.MediatR.Books.Queries.GetAllBooksByCategory
{
    public class GetAllBooksByCategoryHandler : IRequestHandler<GetAllBooksByCategoryQuery, Result<IEnumerable<BookDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllBooksByCategoryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<BookDto>>> Handle(GetAllBooksByCategoryQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Book> filteredBooks = await _unitOfWork.BookRepository.GetAllAsync(book => book.CategoryId == request.categoryId);

            if (filteredBooks is null)
            {
                return Result.Fail(new Error("Nothing found in DB"));
            }

            return Result.Ok(_mapper.Map<IEnumerable<BookDto>>(filteredBooks));
        }
    }
}
