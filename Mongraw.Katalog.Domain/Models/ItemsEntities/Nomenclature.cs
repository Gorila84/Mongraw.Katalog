namespace Mongraw.Katalog.Domain.Models.ItemsEntities
{
    public class Nomenclature
    {
        public int Id { get; set; }
        public string ProductSizeCode { get; set; }
        public string Ean { get; set; }
        public string SizeName { get; set; }
        public int ExpeditionQuantity { get; set; }
        public string Size { get; set; }
        public string SizeCode { get; set; }
        public double BoxDepth { get; set; }
        public double BoxHeight { get; set; }
        public double BoxWidth { get; set; }
        public int BoxCapacity { get; set; }
        public double NetWeight { get; set; }
        public double GrossWeight { get; set; }
        public string TariffNomenclature { get; set; }
        public double Volume { get; set; }
    }
}
