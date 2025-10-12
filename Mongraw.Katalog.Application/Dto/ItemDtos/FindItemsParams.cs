using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongraw.Katalog.Application.Dto.ItemDtos
{
    public class FindItemsParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Name { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string SortBy { get; set; } = "Name"; 

    }
}
