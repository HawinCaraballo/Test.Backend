using Test.Backend.Domain.Common;

namespace Test.Backend.Domain
{
    public class Products : BaseDomainModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Stock { get; set; }
        public string? Description { get; set; } = string.Empty;
        public Double? Price { get; set; }
    }
}
