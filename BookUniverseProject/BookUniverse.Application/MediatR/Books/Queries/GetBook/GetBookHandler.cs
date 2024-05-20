using AutoMapper;
using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Domain.Common;
using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Repositories.Base.UnitOfWork;
using FluentResults;
using MediatR;

namespace BookUniverse.Application.MediatR.Books.Queries.GetBook
{
    public class GetBookHandler
        : IRequestHandler<GetBookQuery, Result<BookDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetBookHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<BookDto>> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            Book book = await _unitOfWork.BookRepository.GetFirstOrDefaultAsync(b => b.Id == request.id);

            if (book is null)
            {
                return Result.Fail(new Error(ResponseMessagesConstants.NOTHING_FOUND));
            }

            return Result.Ok(_mapper.Map<BookDto>(book));
        }
    }
}
