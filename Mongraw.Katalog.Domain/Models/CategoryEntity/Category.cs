using Mongraw.Katalog.Domain.Models.CategoryEntity;
using Mongraw.Katalog.Domain.Models.ItemsEntities;

namespace Mongraw.Katalog.Domain.Models.CategoryEntity
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Subcategory> Subcategories { get; set; }
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
