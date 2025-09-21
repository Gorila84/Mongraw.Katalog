using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Mongraw.Katalog.Application.Service;
using Mongraw.Katalog.Application.Service.Interfaces;
using Mongraw.Katalog.Domain.Interfaces;
using Mongraw.Katalog.Domain.Models.CategoryEntity;
using Moq;
using Xunit;

namespace Mongraw.Katalog.Tests
{
    public class SubCategoryServiceTests
    {
        private readonly Mock<ISubCategoryRepository> _subCategoryRepositoryMock;
        private readonly Mock<IValidator<Subcategory>> _subCategoryValidatorMock;
        private readonly SubCategoryService _subCategoryService;

        public SubCategoryServiceTests()
        {
            _subCategoryRepositoryMock = new Mock<ISubCategoryRepository>();
            _subCategoryValidatorMock = new Mock<IValidator<Subcategory>>();
            _subCategoryService = new SubCategoryService(_subCategoryRepositoryMock.Object, _subCategoryValidatorMock.Object);
        }

        [Fact]
        public async Task GetAllSubCategoriesAsync_ReturnsAllSubCategories() {

            //Arrange
            var subcategories = new List<Subcategory> { new Subcategory { Id = 1, Name = "Test1" }, new Subcategory { Id = 2, Name = "Test2" } };
            _subCategoryRepositoryMock.Setup(r => r.GetAllSubCategoriesAsync())
                                   .ReturnsAsync(subcategories);
            // Act
            var result = await _subCategoryService.GetAllSubCategoriesAsync();
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(subcategories);
            
        }

        [Fact]
        public async Task GetSubcategoryByIdAsync_SubCategoryExist_ReturnSubCategory() {
            //Arrange
            var subcategories = new List<Subcategory> { new Subcategory { Id = 1, Name = "Test1" }, new Subcategory { Id = 2, Name = "Test2" } };
            _subCategoryRepositoryMock.Setup(r => r.GetSubCategoryByIdAsync(1))
                                   .ReturnsAsync(subcategories[0]);
            // Act
            var result = await _subCategoryService.GetSubCategoryByIdAsync(1);
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(subcategories[0]);
        }

        [Fact]
        public async Task GetSubcategoryByIdAsync_SubCategoryDoesNotExist_ReturnsFailure()
        {
            //Arrange
            _subCategoryRepositoryMock.Setup(r => r.GetSubCategoryByIdAsync(1))
                                   .ReturnsAsync((Subcategory?)null);
            // Act
            var result = await _subCategoryService.GetSubCategoryByIdAsync(1);
            // Assert
            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public async Task AddSubCategoryAsync_ValidSubCategory_ReturnsSuccess()
        {
            // Arrange
            var subcategory = new Subcategory { Id = 1, Name = "ValidSubCategory" };
            _subCategoryValidatorMock.Setup(v => v.Validate(subcategory))
                                     .Returns(new FluentValidation.Results.ValidationResult());
            // Act
            var result = await _subCategoryService.AddSubCategoryAsync(subcategory);
            // Assert
            result.IsSuccess.Should().BeTrue();
            _subCategoryRepositoryMock.Verify(r => r.AddSubCategoryAsync(It.IsAny<Subcategory>()), Times.Once);
        }

        [Fact]
        public async Task AddSubCategoryAsync_InvalidSubCategory_ReturnsFailure()
        {
            // Arrange
            var subcategory = new Subcategory { Id = 1, Name = "" };
            var validationFailures = new List<FluentValidation.Results.ValidationFailure>
            {
                new FluentValidation.Results.ValidationFailure("Name", "Name cannot be empty")
            };
            _subCategoryValidatorMock.Setup(v => v.Validate(subcategory))
                                     .Returns(new FluentValidation.Results.ValidationResult(validationFailures));
            // Act
            var result = await _subCategoryService.AddSubCategoryAsync(subcategory);
            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Contain("Name cannot be empty");
            _subCategoryRepositoryMock.Verify(r => r.AddSubCategoryAsync(It.IsAny<Subcategory>()), Times.Never);
        }

        [Fact]
        public async Task UpdateSubCategoryAsync_ValidSubCategory_ReturnsSuccess()
        {
            // Arrange
            var subcategory = new Subcategory { Id = 1, Name = "ValidSubCategory" };
            _subCategoryValidatorMock.Setup(v => v.Validate(subcategory))
                                     .Returns(new FluentValidation.Results.ValidationResult());
            _subCategoryRepositoryMock.Setup(r => r.GetSubCategoryByIdAsync(subcategory.Id))
                                      .ReturnsAsync(subcategory);
            // Act
            var result = await _subCategoryService.UpdateSubCategoryAsync(subcategory);
            // Assert
            result.IsSuccess.Should().BeTrue();
            _subCategoryRepositoryMock.Verify(r => r.UpdateSubCategoryAsync(It.IsAny<Subcategory>()), Times.Once);
        }

        [Fact]
        public async Task UpdateSubCategoryAsync_InvalidSubCategory_ReturnsFailure()
        {
            // Arrange
            var subcategory = new Subcategory { Id = 1, Name = "" };
            var validationFailures = new List<FluentValidation.Results.ValidationFailure>
            {
                new FluentValidation.Results.ValidationFailure("Name", "Name cannot be empty")
            };
            _subCategoryValidatorMock.Setup(v => v.Validate(subcategory))
                                     .Returns(new FluentValidation.Results.ValidationResult(validationFailures));
            // Act
            var result = await _subCategoryService.UpdateSubCategoryAsync(subcategory);
            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Contain("Name cannot be empty");
            _subCategoryRepositoryMock.Verify(r => r.UpdateSubCategoryAsync(It.IsAny<Subcategory>()), Times.Never);
        }

        [Fact]
        public async Task UpdateSubCategoryAsync_SubCategoryDoesNotExist_ReturnsFailure()
        {
            // Arrange
            var subcategory = new Subcategory { Id = 1, Name = "ValidSubCategory" };
            _subCategoryValidatorMock.Setup(v => v.Validate(subcategory))
                                     .Returns(new FluentValidation.Results.ValidationResult());
            _subCategoryRepositoryMock.Setup(r => r.GetSubCategoryByIdAsync(subcategory.Id))
                                      .ReturnsAsync((Subcategory?)null);
            // Act
            var result = await _subCategoryService.UpdateSubCategoryAsync(subcategory);
            // Assert
            result.IsFailure.Should().BeTrue();
            _subCategoryRepositoryMock.Verify(r => r.UpdateSubCategoryAsync(It.IsAny<Subcategory>()), Times.Never);
        }

        [Fact]
        public async Task DeleteSubCategoryAsync_RemmoveeSubCategory()
        {
            // Arrange
            var subcategory = new Subcategory { Id = 1, Name = "ValidSubCategory" };
            _subCategoryRepositoryMock.Setup(r => r.DeleteSubCategoryAsync(subcategory))
                                      .Returns(Task.CompletedTask);
            // Act
            await _subCategoryService.UpdateSubCategoryAsync(new Subcategory { Id = 1, Name = "Test" });
            // Assert
            _subCategoryRepositoryMock.Verify(r => r.DeleteSubCategoryAsync(subcategory), Times.Never);
        }
    }
}
