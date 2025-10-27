namespace Mongraw.Katalog.Domain.Models.ItemsEntities
{
    public class Product
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryCode { get; set; }
        public string? Gender { get; set; }
        public string? GenderCode { get; set; }
        public string? Trademark { get; set; }
        public string? Type { get; set; }
        public string? Subtitle { get; set; }
        public string? Specification { get; set; }
        public string? Description { get; set; }
        public string? ProductCardPdf { get; set; }
        public string? SizeChartPdf { get; set; }
        public string? UserInformationPdf { get; set; }
        public string? DeclarationPdf { get; set; }
        public string? TechnicalSpecificationPdf { get; set; }
        public string? CertificationPdf { get; set; }
        public string? AdditionalInformationPdf { get; set; }
        public List<Alternative> Alternatives { get; set; }
        public List<Variant> Variants { get; set; }
    }
}
