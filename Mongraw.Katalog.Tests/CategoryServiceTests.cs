using FluentValidation.Results;
using FluentValidation;
using Mongraw.Katalog.Application.Service;
using Mongraw.Katalog.Application.Service.Interfaces;
using Mongraw.Katalog.Domain.Interfaces;
using Mongraw.Katalog.Domain.Models.CategoryEntity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Mongraw.Katalog.Domain.Models.ItemsEntities;

namespace Mongraw.Katalog.Tests
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IValidator<Category>> _categoryValidatorMock;
        private readonly CategoryService _categoryService;

        public CategoryServiceTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _categoryValidatorMock = new Mock<IValidator<Category>>();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object, _categoryValidatorMock.Object);
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ReturnsAllCategories()
        {
            // Arrange
            var categories = new List<Category> { new Category { Id = 1, Name = "Test" } };
            _categoryRepositoryMock.Setup(r => r.GetAllCategoriesAsync())
                                   .ReturnsAsync(categories);

            // Act
            var result = await _categoryService.GetAllCategoriesAsync();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public async Task GetCategoryByIdAsync_CategoryExists_ReturnsCategory()
        {
            var category = new Category { Id = 1, Name = "Test" };
            _categoryRepositoryMock.Setup(r => r.GetCategoryByIdAsync(1))
                                   .ReturnsAsync(category);

            var result = await _categoryService.GetCategoryByIdAsync(1);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(category);
        }

        [Fact]
        public async Task GetCategoryByIdAsync_CategoryDoesNotExist_ReturnsFailure()
        {
            _categoryRepositoryMock.Setup(r => r.GetCategoryByIdAsync(1))
                                   .ReturnsAsync((Category?)null);

            var result = await _categoryService.GetCategoryByIdAsync(1);

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Nie znaleziono kategorii o podanym numerze.");
        }

        [Fact]
        public async Task AddCategoryAsync_ValidCategory_ReturnsSuccess()
        {
            var category = new Category { Id = 1, Name = "Test" };
            _categoryValidatorMock.Setup(v => v.Validate(category))
                                  .Returns(new ValidationResult());

            var result = await _categoryService.AddCategoryAsync(category);

            result.IsSuccess.Should().BeTrue();
            _categoryRepositoryMock.Verify(r => r.AddCategoryAsync(category), Times.Once);
        }

        [Fact]
        public async Task AddCategoryAsync_InvalidCategory_ReturnsFailure()
        {
            var category = new Category { Id = 1, Name = "Invalid" };
            var validationResult = new ValidationResult(new[]
            {
                new ValidationFailure("Name", "Name is required")
            });
            _categoryValidatorMock.Setup(v => v.Validate(category))
                                  .Returns(validationResult);

            var result = await _categoryService.AddCategoryAsync(category);

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("Name is required");
            _categoryRepositoryMock.Verify(r => r.AddCategoryAsync(It.IsAny<Category>()), Times.Never);
        }

        [Fact]
        public async Task UpdateCategoryAsync_CategoryExistsAndValid_ReturnsSuccess()
        {
            var category = new Category { Id = 1, Name = "Updated" };
            _categoryValidatorMock.Setup(v => v.Validate(category)).Returns(new ValidationResult());
            _categoryRepositoryMock.Setup(r => r.GetCategoryByIdAsync(1)).ReturnsAsync(category);

            var result = await _categoryService.UpdateCategoryAsync(category);

            result.IsSuccess.Should().BeTrue();
            _categoryRepositoryMock.Verify(r => r.UpdateCategoryAsync(category), Times.Once);
        }

        [Fact]
        public async Task UpdateCategoryAsync_CategoryDoesNotExist_ReturnsFailure()
        {
            var category = new Category { Id = 1, Name = "Updated" };
            _categoryValidatorMock.Setup(v => v.Validate(category)).Returns(new ValidationResult());
            _categoryRepositoryMock.Setup(r => r.GetCategoryByIdAsync(1)).ReturnsAsync((Category?)null);

            var result = await _categoryService.UpdateCategoryAsync(category);

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Nie znaleziono kategorii o podanym numerze.");
        }

        [Fact]
        public async Task UpdateCategoryAsync_InvalidCategory_ReturnsFailure()
        {
            var category = new Category { Id = 1, Name = "" };
            var validationResult = new ValidationResult(new[] {
                new ValidationFailure("Name", "Name is required")
            });
            _categoryValidatorMock.Setup(v => v.Validate(category)).Returns(validationResult);

            var result = await _categoryService.UpdateCategoryAsync(category);

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("Name is required");
            _categoryRepositoryMock.Verify(r => r.UpdateCategoryAsync(It.IsAny<Category>()), Times.Never);
        }

        [Fact]
        public async Task DeleteCategoryAsync_CategoryExistsAndCanBeDeleted_ReturnsSuccess()
        {
            var category = new Category
            {
                Id = 1,
                Name = "ToDelete",
                Items = new List<Item>(), 
                Subcategories = new List<Subcategory>()
            };
            _categoryRepositoryMock.Setup(r => r.GetCategoryByIdAsync(1)).ReturnsAsync(category);

            var result = await _categoryService.DeleteCategoryAsync(1);

            result.IsSuccess.Should().BeTrue();
            _categoryRepositoryMock.Verify(r => r.DeleteCategoryAsync(category), Times.Once);
        }

        [Fact]
        public async Task DeleteCategoryAsync_CategoryDoesNotExist_ReturnsFailure()
        {
            _categoryRepositoryMock.Setup(r => r.GetCategoryByIdAsync(1)).ReturnsAsync((Category?)null);

            var result = await _categoryService.DeleteCategoryAsync(1);

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Nie znaleziono kategorii o podanym numerze.");
        }

        [Fact]
        public async Task DeleteCategoryAsync_CategoryWithItems_ReturnsFailure()
        {
            var category = new Category
            {
                Id = 1,
                Name = "WithItems",
                Items = new List<Item> { new Item() },
                Subcategories = new List<Subcategory>()
            };
            _categoryRepositoryMock.Setup(r => r.GetCategoryByIdAsync(1)).ReturnsAsync(category);

            var result = await _categoryService.DeleteCategoryAsync(1);

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Nie można usunąć kategorii, która zawiera produkty.");
        }

        [Fact]
        public async Task DeleteCategoryAsync_CategoryWithSubcategories_ReturnsFailure()
        {
            var category = new Category
            {
                Id = 1,
                Name = "WithSub",
                Items = new List<Item>(),
                Subcategories = new List<Subcategory> { new Subcategory { Id = 2, Name = "Sub" } }
            };
            _categoryRepositoryMock.Setup(r => r.GetCategoryByIdAsync(1)).ReturnsAsync(category);

            var result = await _categoryService.DeleteCategoryAsync(1);

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Nie można usunąć kategorii, która zawiera podkategorie.");
        }
    }
}
