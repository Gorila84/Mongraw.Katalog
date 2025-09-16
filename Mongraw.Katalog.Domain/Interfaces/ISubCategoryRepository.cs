using Mongraw.Katalog.Domain.Models.CategoryEntity;

namespace Mongraw.Katalog.Domain.Interfaces
{
    public interface ISubCategoryRepository
    {
        Task AddSubCategoryAsync(Subcategory subcategory);
        Task DeleteSubCategoryAsync(Subcategory subcategory);
        Task<IEnumerable<Subcategory>> GetAllSubCategoriesAsync();
        Task<Subcategory?> GetSubCategoryByIdAsync(int id);
        Task UpdateSubCategoryAsync(Subcategory subcategory);
    }
}