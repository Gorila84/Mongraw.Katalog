using Mongraw.Katalog.Domain.Models.ItemsEntities;

namespace Mongraw.Katalog.Domain.Models.CategoryEntity
{
    public class Subcategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public ICollection<Item>? Items { get; set; } = new List<Item>();
    }
}
