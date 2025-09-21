using CSharpFunctionalExtensions;
using Mongraw.Katalog.Domain.Models.CategoryEntity;

namespace Mongraw.Katalog.Application.Service.Interfaces
{
    public interface ISubCategoryService
    {
        Task<Result> AddSubCategoryAsync(Subcategory subcategory);
        Task<Result> DeleteSubCategoryAsync(int id);
        Task<Result<IEnumerable<Subcategory>>> GetAllSubCategoriesAsync();
        Task<Result<Subcategory?>> GetSubCategoryByIdAsync(int id);
        Task<Result> UpdateSubCategoryAsync(Subcategory subcategory);
    }
}