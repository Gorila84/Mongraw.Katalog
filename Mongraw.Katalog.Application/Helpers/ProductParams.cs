using Mongraw.Katalog.Domain.Models.ItemsEntities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongraw.Katalog.Application.Helpers
{
    public class ProductParams 
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string CategoryCode { get; set; } = string.Empty;
        public string GenderCode { get; set; } = string.Empty;
    }
}
