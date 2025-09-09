using Mongraw.Katalog.Domain.Models.CategoryEntity;
using System.Linq.Expressions;

namespace Mongraw.Katalog.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task AddCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<(IEnumerable<Category> Items, int TotalCount)> GetCategoriesAsync(
      Expression<Func<Category, bool>>? filter = null,
      int pageNumber = 1,
      int pageSize = 10);
        Task<Category?> GetCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(Category category);
    }
}