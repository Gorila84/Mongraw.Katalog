using Mongraw.Katalog.Domain.Models.ItemsEntities;
using System.Linq.Expressions;

namespace Mongraw.Katalog.Domain.Interfaces
{
    public interface IItemRepository
    {
        Task AddItemAsync(Item item);
        Task DeleteItemAsync(Item item);
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item?> GetItemByIdAsync(int id);
        Task<(IEnumerable<Item> Items, int TotalCount)> GetItemsAsync(Expression<Func<Item, bool>>? filter, int pageNumber = 1, int pageSize = 10);
        Task UpdateItemAsync(Item item);
    }
}