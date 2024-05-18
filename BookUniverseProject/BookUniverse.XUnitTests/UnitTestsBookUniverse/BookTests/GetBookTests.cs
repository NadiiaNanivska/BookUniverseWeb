using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Application.MediatR.Books.Queries.GetBook;
using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Repositories.Base.UnitOfWork;
using Moq;
using Xunit;

namespace BookUniverse.XUnitTests.UnitTestsBookUniverse.BookTests
{
    public class GetBookTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly GetBookHandler _handler;

        public GetBookTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new GetBookHandler(_mockMapper.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task ReturnBookDto_WhenBookIsFound()
        {
            // Arrange
            var bookId = 1;
            var book = new Book { Id = bookId, Title = "Test Book" };
            var bookDto = new BookDto { Id = bookId, Title = "Test Book" };

            _mockUnitOfWork.Setup(u => u.BookRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>(), null))
                .ReturnsAsync(book);

            _mockMapper.Setup(m => m.Map<BookDto>(book))
                .Returns(bookDto);

            var request = new GetBookQuery(bookId);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(bookDto, result.Value);
        }

        [Fact]
        public async Task ReturnFailure_WhenBookIsNotFound()
        {
            // Arrange
            var bookId = 1;

            _mockUnitOfWork.Setup(u => u.BookRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Book, bool>>>(), null))
                .ReturnsAsync((Book)null);

            var request = new GetBookQuery(bookId);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Single(result.Errors);
            Assert.Equal("Nothing found in DB", result.Errors[0].Message);
        }
    }
}
