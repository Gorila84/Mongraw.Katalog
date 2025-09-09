using Microsoft.EntityFrameworkCore;
using Mongraw.Katalog.Domain.Interfaces;
using Mongraw.Katalog.Domain.Models.CategoryEntity;
using System.Linq.Expressions;

namespace Mongraw.Katalog.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IGenericRepository<Category> _genericRepository;

        public CategoryRepository(IGenericRepository<Category> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _genericRepository.GetAllAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _genericRepository.GetByIdAsync(id);
        }
        public async Task AddCategoryAsync(Category category)
        {
            await _genericRepository.AddAsync(category);
            await _genericRepository.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _genericRepository.Update(category);
            await _genericRepository.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            _genericRepository.Delete(category);
            await _genericRepository.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Category> Items, int TotalCount)> GetCategoriesAsync(
     Expression<Func<Category, bool>>? filter = null,
     int pageNumber = 1,
     int pageSize = 10)
        {
            return await _genericRepository.GetAsync(filter, pageNumber, pageSize);
        }

        public async Task<IEnumerable<Category>> GetAllCategoryWithSubcategoriesAsync(IEnumerable<int> ids)
        {
            var categoriesWithProducts = await _genericRepository.GetAllWithIcludeAsync(filter: null, include: q => q.Include(c => c.Subcategories));
            return categoriesWithProducts;
        }




    }
}
