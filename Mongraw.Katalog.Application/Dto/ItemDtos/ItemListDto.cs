using Mongraw.Katalog.Domain.Models.CategoryEntity;
using Mongraw.Katalog.Domain.Models.ImageEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongraw.Katalog.Application.Dto.ItemDtos
{
    public class ItemListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Quantity { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public Image MainImage { get; set; } 
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
    }
}
