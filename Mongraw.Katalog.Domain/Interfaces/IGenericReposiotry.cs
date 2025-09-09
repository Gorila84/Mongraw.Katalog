using Microsoft.EntityFrameworkCore.Query;
using Mongraw.Katalog.Domain.Models.CategoryEntity;
using System.Linq.Expressions;

namespace Mongraw.Katalog.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(object id);

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<bool> SaveChangesAsync();
        Task<(IEnumerable<T> Items, int TotalCount)> GetAsync(
              Expression<Func<T, bool>>? filter = null,
              int pageNumber = 1,
              int pageSize = 10);
        Task<IEnumerable<T>> GetAllWithIcludeAsync(
    Expression<Func<T, bool>>? filter = null,
    Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    }
}