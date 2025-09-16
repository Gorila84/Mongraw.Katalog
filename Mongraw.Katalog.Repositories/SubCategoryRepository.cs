using Mongraw.Katalog.Domain.Interfaces;
using Mongraw.Katalog.Domain.Models.CategoryEntity;

namespace Mongraw.Katalog.Repositories
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly IGenericRepository<Subcategory> _genericRepository;

        public SubCategoryRepository(IGenericRepository<Subcategory> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<Subcategory>> GetAllSubCategoriesAsync()
        {
            return await _genericRepository.GetAllAsync();
        }

        public async Task<Subcategory?> GetSubCategoryByIdAsync(int id)
        {
            return await _genericRepository.GetByIdAsync(id);
        }

        public async Task AddSubCategoryAsync(Subcategory subcategory)
        {
            await _genericRepository.AddAsync(subcategory);
            await _genericRepository.SaveChangesAsync();
        }

        public async Task UpdateSubCategoryAsync(Subcategory subcategory)
        {
            _genericRepository.Update(subcategory);
            await _genericRepository.SaveChangesAsync();
        }

        public async Task DeleteSubCategoryAsync(Subcategory subcategory)
        {
            _genericRepository.Delete(subcategory);
            await _genericRepository.SaveChangesAsync();
        }
    }
}
