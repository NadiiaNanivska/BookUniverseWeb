using AutoMapper;
using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Application.MediatR.Books.Queries.GetAllBooks;
using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Repositories.Base.UnitOfWork;
using Moq;
using Xunit;



namespace BookUniverse.XUnitTests.UnitTestsBookUniverse.BookTests
{
    public class GetAllBooksTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly GetAllBooksHandler _handler;

        public GetAllBooksTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new GetAllBooksHandler(_mockMapper.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task ReturnOkResult_WhenBooksAreFound()
        {
            // Arrange
            var books = new List<Book> { new Book { Id = 1, Title = "Test Book" } };
            var bookDtos = new List<BookDto> { new BookDto { Id = 1, Title = "Test Book" } };

            _mockUnitOfWork.Setup(u => u.BookRepository.GetAllAsync(null, null))
                .ReturnsAsync(books);

            _mockMapper.Setup(m => m.Map<IEnumerable<BookDto>>(books))
                .Returns(bookDtos);

            var request = new GetAllBooksQuery();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(bookDtos, result.Value);
        }

        [Fact]
        public async Task ReturnError_WhenNoBooksFound()
        {
            _mockUnitOfWork.Setup(u => u.BookRepository.GetAllAsync(null, null))
                .ReturnsAsync((IEnumerable<Book>)null);

            var request = new GetAllBooksQuery();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Single(result.Errors);
            Assert.Equal("Nothing found in DB", result.Errors[0].Message);
        }

        [Fact]
        public async Task ReturnEmptyCollection_WhenNoBooksInDatabase()
        {
            // Arrange
            var books = new List<Book>();

            _mockUnitOfWork.Setup(u => u.BookRepository.GetAllAsync(null, null))
                .ReturnsAsync(books);

            _mockMapper.Setup(m => m.Map<IEnumerable<BookDto>>(books))
                .Returns(new List<BookDto>());

            var request = new GetAllBooksQuery();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value);
        }
    }
}

