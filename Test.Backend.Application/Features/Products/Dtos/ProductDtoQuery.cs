using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Backend.Application.Features.Products.Dtos
{
    public class ProductDtoQuery
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string StateName { get; set; } = string.Empty;
        public int Stock { get; set; }
        public string? Description { get; set; } = string.Empty;
        public Double? Price { get; set; }
        public int Discount { get; set; }
        public Double? FinalPrice { get; set; }
    }
}
