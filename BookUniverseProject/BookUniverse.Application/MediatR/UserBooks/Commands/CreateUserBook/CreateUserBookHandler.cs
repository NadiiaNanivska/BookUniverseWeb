using AutoMapper;
using BookUniverse.Application.DTOs.CategoryDTOs;
using BookUniverse.Application.MediatR.Categories.Commands.CreateCategory;
using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Repositories.Base.UnitOfWork;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookUniverse.Application.MediatR.UserBooks.Commands.CreateUserBook
{
    public class CreateUserBookHandler : IRequestHandler<CreateUserBookCommand, Result<Unit>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserBookHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(CreateUserBookCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.userBook.userId) || request.userBook.bookId < 0)
            {
                string errorMsg = "Not valid user or book";
                return Result.Fail(new Error(errorMsg));
            }

            var existingUserBook = await _unitOfWork.UserBookRepository
            .GetByUserIdAndBookIdAsync(request.userBook.userId, request.userBook.bookId);

            if (existingUserBook != null)
            {
                string errorMsg = "User book already exists";
                return Result.Fail(new Error(errorMsg));
            }

            UserBook newUserBook = new UserBook() { BookId = request.userBook.bookId, UserId = request.userBook.userId };
            _unitOfWork.UserBookRepository.Create(newUserBook);

            var resultIsSuccess = await _unitOfWork.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok();
            }
            else
            {
                string errorMsg = "Error occurred while creating a user book";
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
