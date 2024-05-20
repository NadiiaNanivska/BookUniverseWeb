using AutoMapper;
using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Application.MediatR.UserBooks.Commands.CreateUserBook;
using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Repositories.Base.UnitOfWork;
using Moq;
using Xunit;

namespace BookUniverse.XUnitTests.UnitTestsBookUniverse.UserTests
{
    public class CreateUserBookTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly CreateUserBookHandler _handler;

        public CreateUserBookTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new CreateUserBookHandler(_mockMapper.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task ReturnSuccessResult_WhenUserBookIsValidAndSavedSuccessfully()
        {
            // Arrange
            var userBookDto = new UserBookDto { userId =  "user1", bookId = 1};
            var newUserBook = new UserBook { UserId = "user1", BookId = 1 };
            _mockMapper.Setup(m => m.Map<UserBook>(userBookDto)).Returns(newUserBook);
            _mockUnitOfWork.Setup(u => u.UserBookRepository.GetByUserIdAndBookIdAsync(userBookDto.userId, userBookDto.bookId))
                           .ReturnsAsync((UserBook)null);
            _mockUnitOfWork.Setup(u => u.UserBookRepository.Create(It.IsAny<UserBook>())).Verifiable();
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var request = new CreateUserBookCommand(userBookDto);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _mockUnitOfWork.Verify(u => u.UserBookRepository.Create(It.IsAny<UserBook>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task ReturnFailureResult_WhenUserBookIsInvalid()
        {
            // Arrange
            var userBookDto = new UserBookDto {userId =  "", bookId = -1};
            var request = new CreateUserBookCommand(userBookDto);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Not valid user or book", result.Errors.First().Message);
        }

        [Fact]
        public async Task ReturnFailureResult_WhenUserBookAlreadyExists()
        {
            // Arrange
            var userBookDto = new UserBookDto { userId =  "user1", bookId = 1 };
            var existingUserBook = new UserBook { UserId = "user1", BookId = 1 };
            _mockUnitOfWork.Setup(u => u.UserBookRepository.GetByUserIdAndBookIdAsync(userBookDto.userId, userBookDto.bookId))
                           .ReturnsAsync(existingUserBook);

            var request = new CreateUserBookCommand(userBookDto);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("User book already exists", result.Errors.First().Message);
        }

        [Fact]
        public async Task ReturnFailureResult_WhenSaveChangesFails()
        {
            // Arrange
            var userBookDto = new UserBookDto { userId =  "user1", bookId = 1 };
            _mockUnitOfWork.Setup(u => u.UserBookRepository.GetByUserIdAndBookIdAsync(userBookDto.userId, userBookDto.bookId))
                           .ReturnsAsync((UserBook)null);
            _mockUnitOfWork.Setup(u => u.UserBookRepository.Create(It.IsAny<UserBook>())).Verifiable();
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(0);

            var request = new CreateUserBookCommand(userBookDto);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Error occurred while creating a user book", result.Errors.First().Message);
        }
    }
}