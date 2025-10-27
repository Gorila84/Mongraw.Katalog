using Mongraw.Katalog.Domain.Models.ItemsEntities;
using Mongraw.Katalog.Domain.Models.ItemsEntities.Dtos;

namespace Mongraw.Katalog.Domain.Interfaces
{
    public interface IMalfiniProductRepository
    {
        IQueryable<Product> QueryWithInclude();
    }
}