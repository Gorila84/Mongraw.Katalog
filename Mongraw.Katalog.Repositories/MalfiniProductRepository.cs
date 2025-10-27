using Microsoft.EntityFrameworkCore;
using Mongraw.Katalog.Domain.Interfaces;
using Mongraw.Katalog.Domain.Models.ItemsEntities;

namespace Mongraw.Katalog.Repositories
{
    public class MalfiniProductRepository : IMalfiniProductRepository
    {
        private readonly IGenericRepository<Product> _genericRepository;

        public MalfiniProductRepository(IGenericRepository<Product> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public IQueryable<Product> QueryWithInclude()
        {
            return _genericRepository.GetQueryable(
                include: q => q
                    .Include(p => p.Variants)
                        .ThenInclude(v => v.Images)
            );
        }
    }
}
