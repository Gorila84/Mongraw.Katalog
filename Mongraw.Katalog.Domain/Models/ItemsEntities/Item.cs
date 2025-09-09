using Mongraw.Katalog.Domain.Models.CategoryEntity;

namespace Mongraw.Katalog.Domain.Models.ItemsEntities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
    }
}
