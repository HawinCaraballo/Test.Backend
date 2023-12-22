
namespace Test.Backend.Infraestructure.Repositories
{
    using Test.Backend.Application.Contracts.Persistence;
    using Test.Backend.Domain;
    using Test.Backend.Infraestructure.Persistence;

    public class ProductRepository : RepositoryBase<Products>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }

    }
}
