using AutoMapper;
using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Domain.Common;
using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Repositories.Base.UnitOfWork;
using FluentResults;
using MediatR;

namespace BookUniverse.Application.MediatR.Books.Queries.GetAllBooksByUser
{
    public class GetAllBooksByUserHandler : IRequestHandler<GetAllBooksByUserQuery, Result<IEnumerable<BookDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllBooksByUserHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<BookDto>>> Handle(GetAllBooksByUserQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Book> filteredBooks = _unitOfWork.UserBookRepository.GetAllByUser(u => u.User.Id == request.userId).ToList();

            if (filteredBooks is null)
            {
                return Result.Fail(new Error(ResponseMessagesConstants.NOTHING_FOUND));
            }
            return Result.Ok(_mapper.Map<IEnumerable<BookDto>>(filteredBooks));
        }
    }
}
