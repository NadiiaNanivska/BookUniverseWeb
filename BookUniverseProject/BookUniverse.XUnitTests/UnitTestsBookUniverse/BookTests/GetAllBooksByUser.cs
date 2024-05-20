using System.Linq.Expressions;
using AutoMapper;
using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Application.MediatR.Books.Queries.GetAllBooksByUser;
using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Repositories.Base.UnitOfWork;
using Moq;
using Xunit;

namespace BookUniverse.XUnitTests.UnitTestsBookUniverse.BookTests
{
    public class GetAllBooksByUserTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly GetAllBooksByUserHandler _handler;

        public GetAllBooksByUserTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new GetAllBooksByUserHandler(_mockMapper.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task ReturnSuccessResult_WhenBooksAreFound()
        {
            // Arrange
            var userId = "sampleUserId";
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Sample Book 1" },
                new Book { Id = 2, Title = "Sample Book 2" }
            };
            var bookDtos = new List<BookDto>
            {
                new BookDto { Id = 1, Title = "Sample Book 1" },
                new BookDto { Id = 2, Title = "Sample Book 2" }
            };

            _mockUnitOfWork.Setup(uow => uow.UserBookRepository.GetAllByUser(It.IsAny<Expression<Func<UserBook, bool>>>()))
                .Returns(books);

            _mockMapper.Setup(m => m.Map<IEnumerable<BookDto>>(books)).Returns(bookDtos);

            var request = new GetAllBooksByUserQuery(userId);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count());
        }

        [Fact]
        public async Task ReturnSuccessResult_WithEmptyList_WhenNoBooksMatchUser()
        {
            // Arrange
            var userId = "sampleUserId";
            
            _mockUnitOfWork.Setup(uow => uow.UserBookRepository.GetAllByUser(It.IsAny<Expression<Func<UserBook, bool>>>()))
                .Returns(new List<Book>());
            _mockMapper.Setup(m => m.Map<IEnumerable<BookDto>>(It.IsAny<IEnumerable<Book>>()))
                       .Returns(new List<BookDto>());

            var request = new GetAllBooksByUserQuery(userId);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value);
        }
    }
}