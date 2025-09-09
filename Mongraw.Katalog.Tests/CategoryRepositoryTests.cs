using Microsoft.EntityFrameworkCore.Query;
using Mongraw.Katalog.Domain.Interfaces;
using Mongraw.Katalog.Domain.Models.CategoryEntity;
using Mongraw.Katalog.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mongraw.Katalog.Tests
{
    public class CategoryRepositoryTests
    {
        private readonly Mock<IGenericRepository<Category>> _genericRepoMock;
        private readonly CategoryRepository _categoryRepository;

        public CategoryRepositoryTests()
        {
            _genericRepoMock = new Mock<IGenericRepository<Category>>();
            _categoryRepository = new CategoryRepository(_genericRepoMock.Object);
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ShouldReturnAllCategories()
        {
            // Arrange
            var categories = new List<Category>
        {
            new Category { Id = 1, Name = "Cat1" },
            new Category { Id = 2, Name = "Cat2" }
        };

            _genericRepoMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(categories);

            // Act
            var result = await _categoryRepository.GetAllCategoriesAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.Name == "Cat1");
            Assert.Contains(result, c => c.Name == "Cat2");
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ShouldReturnCategory_WhenCategoryExists()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Cat1" };

            _genericRepoMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(category);

            // Act
            var result = await _categoryRepository.GetCategoryByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Cat1", result!.Name);
        }

        [Fact]
        public async Task AddCategoryAsync_ShouldCallAddAndSaveChanges()
        {
            // Arrange
            var category = new Category { Name = "NewCategory" };

            _genericRepoMock.Setup(repo => repo.AddAsync(category))
                .Returns(Task.CompletedTask);

            _genericRepoMock.Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(true);

            // Act
            await _categoryRepository.AddCategoryAsync(category);

            // Assert
            _genericRepoMock.Verify(repo => repo.AddAsync(category), Times.Once);
            _genericRepoMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateCategoryAsync_ShouldCallUpdateAndSaveChanges()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Updated" };

            _genericRepoMock.Setup(repo => repo.Update(category));

            _genericRepoMock.Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(true);

            // Act
            await _categoryRepository.UpdateCategoryAsync(category);

            // Assert
            _genericRepoMock.Verify(repo => repo.Update(category), Times.Once);
            _genericRepoMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteCategoryAsync_ShouldCallDeleteAndSaveChanges()
        {
            // Arrange  
            var category = new Category { Id = 1, Name = "ToDelete" };

            _genericRepoMock.Setup(repo => repo.Delete(category));

            _genericRepoMock.Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(true);

            // Act
            await _categoryRepository.DeleteCategoryAsync(category);

            // Assert
            _genericRepoMock.Verify(repo => repo.Delete(category), Times.Once);
            _genericRepoMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetCategoriesAsync_ShouldReturnFilteredAndPagedResults()
        {
            // Arrange
            var allCategories = new List<Category>
            {
                new Category { Id = 1, Name = "Cat1" },
                new Category { Id = 2, Name = "Cat2" },
                new Category { Id = 3, Name = "Cat3" },
                new Category { Id = 4, Name = "Dog" }
            };

            Expression<Func<Category, bool>> filterExpr = c => c.Name.Contains("Cat");

            var compiledFilter = filterExpr.Compile();

            _genericRepoMock
                .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Category, bool>>>(), 1, 2))
                .ReturnsAsync((Expression<Func<Category, bool>> expr, int page, int size) =>
                {
                    var filter = expr.Compile();
                    var filtered = allCategories.Where(filter);
                    var paged = filtered.Skip((page - 1) * size).Take(size);
                    return (paged, filtered.Count());
                });

            // Act
            var (items, totalCount) = await _categoryRepository.GetCategoriesAsync(filterExpr, 1, 2);

            // Assert
            Assert.Equal(3, totalCount);
            Assert.Equal(2, items.Count());
            Assert.All(items, c => Assert.Contains("Cat", c.Name));
        }

        [Fact]
        public async Task GetAllCategoryWithSubcategoriesAsync_ShouldReturnCategoriesWithSubcategories()
        {
            // Arrange
            var categories = new List<Category>
        {
            new Category
            {
                Id = 1,
                Name = "Main Category 1",
                Subcategories = new List<Subcategory>
                {
                    new Subcategory { Id = 2, Name = "Subcategory 1" },
                    new Subcategory { Id = 3, Name = "Subcategory 2" }
                }
            }
        };

            // Setup mock
            _genericRepoMock.Setup(repo => repo.GetAllWithIcludeAsync(
                It.IsAny<Expression<Func<Category, bool>>>(),
                It.IsAny<Func<IQueryable<Category>, IIncludableQueryable<Category, object>>>()))
                .ReturnsAsync((
                    Expression<Func<Category, bool>> filterExpr,
                    Func<IQueryable<Category>, IIncludableQueryable<Category, object>> include) =>
             {
                var query = categories.AsQueryable();

                if (filterExpr != null)
                    query = query.Where(filterExpr);

                if (include != null)
                    query = include(query);

                 return query.ToList();
            });

            // Act
            var result = await _categoryRepository.GetAllCategoryWithSubcategoriesAsync(new[] { 1 });

            // Assert
            Assert.NotNull(result);
            var resultList = result.ToList();
            Assert.Single(resultList);
            Assert.Equal(2, resultList[0].Subcategories.Count);
            Assert.Contains(resultList[0].Subcategories, s => s.Name == "Subcategory 1");
            Assert.Contains(resultList[0].Subcategories, s => s.Name == "Subcategory 2");
        }
    }
}