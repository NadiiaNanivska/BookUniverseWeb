using AutoMapper;
using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Application.DTOValidators.BookValidators;
using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Repositories.Base.UnitOfWork;
using FluentResults;
using FluentValidation.Results;
using MediatR;

namespace BookUniverse.Application.MediatR.Books.Commands.CreateBook
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, Result<AddBookDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AddBookDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            // TODO: google service && test
            AddBookDtoValidator validator = new AddBookDtoValidator();
            ValidationResult validationResult = validator.Validate(request.newBook);

            if (!validationResult.IsValid)
            {
                return Result.Fail("Not valid book");
            }

            Book book = _mapper.Map<Book>(request.newBook);
            
            var entity = _unitOfWork.BookRepository.Create(book);
            var resultIsSuccess = await _unitOfWork.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(_mapper.Map<AddBookDto>(entity));
            }
            else
            {
                string errorMsg = "Error occurred while creating a book";
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
