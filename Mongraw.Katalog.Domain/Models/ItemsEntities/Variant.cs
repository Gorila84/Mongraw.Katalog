namespace Mongraw.Katalog.Domain.Models.ItemsEntities
{
    public class Variant
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string ColorCode { get; set; }
        public string ColorIconLink { get; set; }
        public string Name { get; set; }
        public List<Attribute> Attributes { get; set; }
        public List<Image> Images { get; set; }
        public List<Nomenclature> Nomenclatures { get; set; }
    }
}
