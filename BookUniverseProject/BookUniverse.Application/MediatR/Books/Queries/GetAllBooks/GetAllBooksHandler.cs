using AutoMapper;
using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Repositories.Base.UnitOfWork;
using FluentResults;
using MediatR;

namespace BookUniverse.Application.MediatR.Books.Queries.GetAllBooks
{
    public class GetAllBooksHandler
        : IRequestHandler<GetAllBooksQuery, Result<IEnumerable<BookDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllBooksHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<BookDto>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Book> books = await _unitOfWork.BookRepository.GetAllAsync();

            if (books is null)
            {
                return Result.Fail(new Error("Nothing found in DB"));
            }

            return Result.Ok(_mapper.Map<IEnumerable<BookDto>>(books));
        }
    }
}
