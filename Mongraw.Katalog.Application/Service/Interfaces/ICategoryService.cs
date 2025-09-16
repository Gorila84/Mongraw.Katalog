using CSharpFunctionalExtensions;
using Mongraw.Katalog.Domain.Models.CategoryEntity;

namespace Mongraw.Katalog.Application.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<Result> AddCategoryAsync(Category category);
        Task<Result> DeleteCategoryAsync(int id);
        Task<Result<IEnumerable<Category>>> GetAllCategoriesAsync();
        Task<Result<Category?>> GetCategoryByIdAsync(int id);
        Task<Result> UpdateCategoryAsync(Category category);
    }
}