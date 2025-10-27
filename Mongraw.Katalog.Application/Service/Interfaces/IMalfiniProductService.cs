using Mongraw.Katalog.Application.Helpers;
using Mongraw.Katalog.Domain.Models.ItemsEntities.Dtos;

namespace Mongraw.Katalog.Application.Service.Interfaces
{
    public interface IMalfiniProductService
    {
        Task<PageResult<ProductListDto>> GetPagedMalfiniProductsAsync(ProductParams productParams);
    }
}