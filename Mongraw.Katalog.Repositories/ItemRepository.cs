using Microsoft.EntityFrameworkCore;
using Mongraw.Katalog.Domain.Interfaces;
using Mongraw.Katalog.Domain.Models.ItemsEntities;
using System.Linq.Expressions;

namespace Mongraw.Katalog.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IGenericRepository<Item> _genericRepository;

        public ItemRepository(IGenericRepository<Item> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _genericRepository.GetAllAsync();
        }

        public async Task<Item?> GetItemByIdAsync(int id)
        {
            return await _genericRepository.GetByIdAsync(id);
        }

        public async Task AddItemAsync(Item item)
        {
            await _genericRepository.AddAsync(item);
            await _genericRepository.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            _genericRepository.Update(item);
            await _genericRepository.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(Item item)
        {
            _genericRepository.Delete(item);
            await _genericRepository.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Item> Items, int TotalCount)> GetItemsAsync(
                Expression<Func<Item, bool>>? filter,
                int pageNumber = 1,
                int pageSize = 10)
        {
            if (pageNumber <= 0) throw new ArgumentOutOfRangeException(nameof(pageNumber));
            if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));

            var query = _genericRepository.GetQueryable(filter);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}