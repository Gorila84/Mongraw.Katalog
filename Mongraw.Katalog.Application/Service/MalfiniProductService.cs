using Microsoft.EntityFrameworkCore;
using Mongraw.Katalog.Application.Helpers;
using Mongraw.Katalog.Application.Service.Interfaces;
using Mongraw.Katalog.Domain.Interfaces;
using Mongraw.Katalog.Domain.Models.ItemsEntities.Dtos;

namespace Mongraw.Katalog.Application.Service
{


    public class MalfiniProductService : IMalfiniProductService
    {
        private readonly IMalfiniProductRepository _malfiniProductRepository;

        public MalfiniProductService(IMalfiniProductRepository malfiniProductRepository)
        {
            _malfiniProductRepository = malfiniProductRepository;
        }

        public async Task<PageResult<ProductListDto>> GetPagedMalfiniProductsAsync(ProductParams productParams)
        {
            var query = _malfiniProductRepository.QueryWithInclude();

            if (!string.IsNullOrWhiteSpace(productParams.Name))
            {
                var nameLower = productParams.Name.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(nameLower));
            }

            if (!string.IsNullOrWhiteSpace(productParams.Color))
            {
                var colorLower = productParams.Color.ToLower();
                query = query.Where(p => p.Variants.Any(v => v.ColorCode.ToLower().Contains(colorLower)));
            }

            if (!string.IsNullOrWhiteSpace(productParams.CategoryCode))
            {
                query = query.Where(p => p.CategoryCode == productParams.CategoryCode);
            }

            if (!string.IsNullOrWhiteSpace(productParams.GenderCode))
            {
                query = query.Where(p => p.GenderCode == productParams.GenderCode);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(p => p.Id)
                .Skip((productParams.PageNumber - 1) * productParams.PageSize)
                .Take(productParams.PageSize)
                .Select(p => new ProductListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Subtitle = p.Subtitle,
                    ImageUrl = p.Variants
                        .Select(v => v.Images.FirstOrDefault().Link)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return new PageResult<ProductListDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = productParams.PageNumber,
                PageSize = productParams.PageSize
            };
        }
    }
}
