using AutoMapper;
using BookUniverse.Application.DTOs.CategoryDTOs;
using BookUniverse.Application.MediatR.Categories.Queries.GetAllCategories;
using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Repositories.Base.UnitOfWork;
using Moq;
using Xunit;

namespace BookUniverse.XUnitTests.UnitTestsBookUniverse.CategoryTests
{
    public class GetAllCategoriesTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly GetAllCategoriesHandler _handler;

        public GetAllCategoriesTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new GetAllCategoriesHandler(_mockMapper.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task ReturnSuccessResult_WhenCategoriesFound()
        {
            // Arrange
            var categories = GetTestCategories();

            _mockUnitOfWork.Setup(uow => uow.CategoryRepository.GetAllAsync(null, null))
                           .ReturnsAsync(categories);

            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<CategoryDto>>(categories))
                       .Returns(categories.Select(c => new CategoryDto()));

            var request = new GetAllCategoriesQuery();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(categories.Count(), result.Value.Count());
        }

        [Fact]
        public async Task ReturnFailureResult_WhenNoCategoriesFound()
        {
            // Arrange
            IEnumerable<Category> categories = null;

            _mockUnitOfWork.Setup(uow => uow.CategoryRepository.GetAllAsync(null, null))
                           .ReturnsAsync(categories);

            var request = new GetAllCategoriesQuery();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Nothing found in DB", result.Errors.First().Message);
        }

        private IEnumerable<Category> GetTestCategories()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, CategoryName = "Fiction" },
                new Category { Id = 2, CategoryName = "Non-Fiction" },
                new Category { Id = 3, CategoryName = "Science Fiction" }
            };
            return categories;
        }
    }
}

