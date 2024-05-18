using AutoMapper;
using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Application.MediatR.Books.Commands.CreateBook;
using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Repositories.Base.UnitOfWork;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace BookUniverse.XUnitTests.UnitTestsBookUniverse.BookTests
{
    public class CreateBookTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly CreateBookHandler _handler;

        public CreateBookTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new CreateBookHandler(_mockMapper.Object, _mockUnitOfWork.Object);
        }
        
        [Fact]
        public async Task ReturnSuccessResult_WhenBookIsValidAndSavedSuccessfully()
        {
            // Arrange
            var category = new Category { Id = 1, CategoryName = "Fiction" };
            var newBookDto = new AddBookDto
            {
                Title = "Sample Book",
                Author = "John Doe",
                Description = "DescriptDRDFGDCDFCFGCfcgv chksbgxkhcfbkshdxbk cskdbh hkcdsxbdhbsxnxsvdjn hjsdbnvjsdb jsbnjbion",
                NumberOfPages = 145,
                CategoryId = category.Id,
                Path = "somepath"
            };

            // Setup Mapper
            _mockMapper.Setup(mapper => mapper.Map<Book>(newBookDto, It.IsAny<Action<IMappingOperationOptions>>()))
                .Returns(new Book());

            // Setup Repository
            _mockUnitOfWork.Setup(uow => uow.BookRepository.Create(It.IsAny<Book>()))
                .Returns(new Book());

            _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync())
                .ReturnsAsync(1);

            var request = new CreateBookCommand(newBookDto);
            var handler = new CreateBookHandler(_mockMapper.Object, _mockUnitOfWork.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }





        [Fact]
        public async Task ReturnFailureResult_WhenBookIsInvalid()
        {
            // Arrange
            var addBookDto = new AddBookDto { /* Initialize properties */ };
            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("Title", "Title is required"));

            _mockMapper.Setup(m => m.Map<Book>(addBookDto))
                .Returns(new Book());

            var request = new CreateBookCommand(addBookDto);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Errors);
        }

        [Fact]
        public async Task ReturnFailureResult_WhenSaveChangesFails()
        {
            // Arrange
            var addBookDto = new AddBookDto {};

            _mockMapper.Setup(m => m.Map<Book>(addBookDto))
                .Returns(new Book());

            _mockUnitOfWork.Setup(u => u.BookRepository.Create(It.IsAny<Book>()))
                .Returns(new Book());

            _mockUnitOfWork.Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(0);

            var request = new CreateBookCommand(addBookDto);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Errors);
        }
    }
}
