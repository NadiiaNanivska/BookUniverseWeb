using AutoMapper;
using BookUniverse.Application.DTOs.CategoryDTOs;
using BookUniverse.Application.MediatR.Categories.Commands.CreateCategory;
using BookUniverse.Domain.Entities;
using BookUniverse.Infrastructure.Repositories.Base.UnitOfWork;
using Moq;
using Xunit;

namespace BookUniverse.XUnitTests.UnitTestsBookUniverse.CategoryTests
{
    public class CreateCategoryTests
    {
        public class CreateCategoryHandlerTests
        {
            private readonly Mock<IMapper> _mockMapper;
            private readonly Mock<IUnitOfWork> _mockUnitOfWork;
            private readonly CreateCategoryHandler _handler;

            public CreateCategoryHandlerTests()
            {
                _mockMapper = new Mock<IMapper>();
                _mockUnitOfWork = new Mock<IUnitOfWork>();
                _handler = new CreateCategoryHandler(_mockMapper.Object, _mockUnitOfWork.Object);
            }

            [Fact]
            public async Task ReturnSuccessResult_WhenCategoryIsValidAndSavedSuccessfully()
            {
                // Arrange
                var categoryName = "Fiction";

                _mockUnitOfWork.Setup(uow => uow.CategoryRepository.Create(It.IsAny<Category>()))
                    .Returns(new Category { Id = 1, CategoryName = categoryName });

                _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync())
                    .ReturnsAsync(1);

                _mockMapper.Setup(mapper => mapper.Map<CategoryDto>(It.IsAny<Category>()))
                    .Returns(new CategoryDto { Id = 1, CategoryName = categoryName });

                var request = new CreateCategoryCommand(categoryName);

                // Act
                var result = await _handler.Handle(request, CancellationToken.None);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.Equal(categoryName, result.Value.CategoryName);
            }

            [Fact]
            public async Task ReturnFailureResult_WhenCategoryNameIsNullOrWhitespace()
            {
                // Arrange
                var categoryName = "";

                var request = new CreateCategoryCommand(categoryName);

                // Act
                var result = await _handler.Handle(request, CancellationToken.None);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Not valid category name", result.Errors[0].Message);
            }
        }
    }
}

