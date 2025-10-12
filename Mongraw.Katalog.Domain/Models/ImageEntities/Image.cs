using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongraw.Katalog.Domain.Models.ImageEntities
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string AltText { get; set; }
        public bool IsMain { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
