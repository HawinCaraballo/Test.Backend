
namespace Test.Backend.Domain.Common
{
    public abstract class BaseDomainModel
    {
        public DateTime CreatedDate { get; set; }
        public string CreateBy { get; set; } = string.Empty;
        public string? UpdateBy { get; set; } = string.Empty;
        public DateTime? UpdatedDate { get; set; }
        public bool Status { get; set; }

    }
}
