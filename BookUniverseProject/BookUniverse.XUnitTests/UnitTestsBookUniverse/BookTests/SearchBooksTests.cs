using AutoMapper;
using BookUniverse.Application.MediatR.SearchBooks.Queries.GetAllBooksByQuery;
using BookUniverse.Application.MediatR.SearchBooks.Queries.GetAllBooksByText;
using BookUniverse.Infrastructure.Services.SearchBook;
using Google.Apis.Books.v1.Data;
using Moq;
using Xunit;

namespace BookUniverse.XUnitTests.UnitTestsBookUniverse.BookTests
{
    public class GetAllBooksByTextTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ISearchBook> _mockSearchBook;
        private readonly GetAllBooksByTextHandler _handler;

        public GetAllBooksByTextTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockSearchBook = new Mock<ISearchBook>();
            _handler = new GetAllBooksByTextHandler(_mockMapper.Object, _mockSearchBook.Object);
        }

        [Fact]
        public async Task ReturnSuccessResult_WhenBooksAreFound()
        {
            // Arrange
            var queryString = "sample query";
            var volumes = new Volumes
            {
                Items = new List<Volume>
                {
                    new Volume
                    {
                        VolumeInfo = new Volume.VolumeInfoData
                        {
                            Title = "Sample Book 1",
                            Authors = new List<string> { "Author 1" }
                        }
                    },
                    new Volume
                    {
                        VolumeInfo = new Volume.VolumeInfoData
                        {
                            Title = "Sample Book 2",
                            Authors = new List<string> { "Author 2" }
                        }
                    }
                }
            };
            _mockSearchBook.Setup(s => s.SearchAsync(queryString)).ReturnsAsync(volumes);

            var request = new GetAllBooksByTextQuery(queryString);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count());
            Assert.Contains(result.Value, b => b == "Sample Book 1 by Author 1");
            Assert.Contains(result.Value, b => b == "Sample Book 2 by Author 2");
        }

        [Fact]
        public async Task ReturnFailureResult_WhenNoBooksAreFound()
        {
            // Arrange
            var queryString = "sample query";
            var volumes = new Volumes { Items = null };
            _mockSearchBook.Setup(s => s.SearchAsync(queryString)).ReturnsAsync(volumes);

            var request = new GetAllBooksByTextQuery(queryString);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Nothing found in DB", result.Errors.First().Message);
        }

        [Fact]
        public async Task ReturnSuccessResult_WithEmptyList_WhenBooksItemsAreEmpty()
        {
            // Arrange
            var queryString = "sample query";
            var volumes = new Volumes { Items = new List<Volume>() };
            _mockSearchBook.Setup(s => s.SearchAsync(queryString)).ReturnsAsync(volumes);

            var request = new GetAllBooksByTextQuery(queryString);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value);
        }
    }
}