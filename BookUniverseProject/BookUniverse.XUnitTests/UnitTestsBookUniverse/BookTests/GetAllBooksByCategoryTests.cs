using System.Linq.Expressions;
using AutoMapper;
using BookUniverse.Application.DTOs.BookDTOs;
using BookUniverse.Application.MediatR.Books.Queries.GetAllBooksByCategory;
using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Repositories.Base.UnitOfWork;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Xunit;

namespace BookUniverse.XUnitTests.UnitTestsBookUniverse.BookTests
{
    
    public class GetAllBooksByCategoryTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly GetAllBooksByCategoryHandler _handler;

        public GetAllBooksByCategoryTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new GetAllBooksByCategoryHandler(_mockMapper.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task ReturnSuccessResult_WhenBooksFoundForCategory()
        {
            // Arrange
            var categoryId = 1;
            var books = GetTestBooks(categoryId);
            var filteredBooks = books.Where(b => b.CategoryId == categoryId);

            _mockUnitOfWork.Setup(uow => uow.BookRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Book, bool>>>(), 
                    It.IsAny<Func<IQueryable<Book>, IIncludableQueryable<Book, object>>>()))
                .ReturnsAsync(filteredBooks);



            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<BookDto>>(filteredBooks))
                       .Returns(filteredBooks.Select(b => new BookDto()));

            var request = new GetAllBooksByCategoryQuery(categoryId);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(filteredBooks.Count(), result.Value.Count());
        }

        [Fact]
        public async Task ReturnFailureResult_WhenNoBooksFoundForCategory()
        {
            // Arrange
            var categoryId = 1;
            IEnumerable<Book> filteredBooks = null;

            _mockUnitOfWork.Setup(uow => uow.BookRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Book, bool>>>(), 
                    It.IsAny<Func<IQueryable<Book>, IIncludableQueryable<Book, object>>>()))
                .ReturnsAsync(filteredBooks);

            var request = new GetAllBooksByCategoryQuery(categoryId);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Nothing found in DB", result.Errors.First().Message);
        }

        private IEnumerable<Book> GetTestBooks(int categoryId)
        {
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", CategoryId = categoryId },
                new Book { Id = 2, Title = "Book 2", CategoryId = categoryId },
                new Book { Id = 3, Title = "Book 3", CategoryId = categoryId + 1 }
            };
            return books;
        }
    }
}
