using Mongraw.Katalog.Domain.Models.CategoryEntity;
using Mongraw.Katalog.Domain.Models.ImageEntities;

namespace Mongraw.Katalog.Application.Dto.ItemDtos
{
    public class AddItemDto
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public List<Image> Images { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
        public string WholesalerName { get; set; }
    }
}
