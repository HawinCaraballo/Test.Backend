
namespace Test.Backend.Application.UnitTests.Mocks
{
    using AutoFixture;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Test.Backend.Application.Contracts.Persistence;
    using Test.Backend.Domain;
    using Test.Backend.Infraestructure.Persistence;
    using Test.Backend.Infraestructure.Repositories;

    public static class MockProductRepository
    {
        public static Mock<IProductRepository> GetProductRepositoryInterface()
        {
            var fixture = new Fixture();
            var products = fixture.CreateMany<Products>().ToList();

            products.Add(
                fixture.Build<Products>()
                .With(tr => tr.ProductId, 1)
                .With(tr => tr.Name, "Prueba name")
                .With(tr => tr.Description, "description prueba")
                .With(tr => tr.Price, 1000)
                .With(tr => tr.Stock, 7)
                .With(tr => tr.Status, true)
                .With(tr => tr.CreateBy, "system")
                .With(tr => tr.CreatedDate, DateTime.Now)
                .Create()
            );

            products.Add(
                fixture.Build<Products>()
                .With(tr => tr.ProductId, 2)
                .With(tr => tr.Name, "Prueba name 2")
                .With(tr => tr.Description, "description prueba 2")
                .With(tr => tr.Price, 5330)
                .With(tr => tr.Stock, 3)
                .With(tr => tr.Status, true)
                .With(tr => tr.CreateBy, "system")
                .With(tr => tr.CreatedDate, DateTime.Now)
                .Create()
            );

            products.Add(
                fixture.Build<Products>()
                .With(tr => tr.ProductId, 3)
                .With(tr => tr.Name, "Prueba name 3")
                .With(tr => tr.Description, "description prueba 3")
                .With(tr => tr.Price, 500)
                .With(tr => tr.Stock, 5)
                .With(tr => tr.Status, true)
                .With(tr => tr.CreateBy, "system")
                .With(tr => tr.CreatedDate, DateTime.Now)
                .Create()
            );


            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x=> x.GetAllAsync()).ReturnsAsync(products);

            return mockRepository;
        }

        public static void AddProductRepository(ApplicationDbContext applicationDbContextFake)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var products = fixture.CreateMany<Products>().ToList();

            products.Add(
                fixture.Build<Products>()
                .With(tr => tr.ProductId, 1)
                .With(tr => tr.Name, "Prueba name")
                .With(tr => tr.Description, "description prueba")
                .With(tr => tr.Price, 1000)
                .With(tr => tr.Stock, 7)
                .With(tr => tr.Status, true)
                .With(tr => tr.CreateBy, "system")
                .With(tr => tr.CreatedDate, DateTime.Now)
                .Create()
            );

            products.Add(
                fixture.Build<Products>()
                .With(tr => tr.ProductId, 2)
                .With(tr => tr.Name, "Prueba name 2")
                .With(tr => tr.Description, "description prueba 2")
                .With(tr => tr.Price, 5330)
                .With(tr => tr.Stock, 3)
                .With(tr => tr.Status, true)
                .With(tr => tr.CreateBy, "system")
                .With(tr => tr.CreatedDate, DateTime.Now)
                .Create()
            );

            products.Add(
                fixture.Build<Products>()
                .With(tr => tr.ProductId, 3)
                .With(tr => tr.Name, "Prueba name 3")
                .With(tr => tr.Description, "description prueba 3")
                .With(tr => tr.Price, 500)
                .With(tr => tr.Stock, 5)
                .With(tr => tr.Status, true)
                .With(tr => tr.CreateBy, "system")
                .With(tr => tr.CreatedDate, DateTime.Now)
                .Create()
            );

            applicationDbContextFake.Products!.AddRange(products);
            applicationDbContextFake.SaveChanges();
        }

        public static Mock<ProductRepository> GetRepositoryDBContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"ApplicationDbContext-{Guid.NewGuid}")
                .Options;

            var applicationDbContextFake = new ApplicationDbContext(options);

            applicationDbContextFake.Database.EnsureDeleted();
            var mockUProduct = new Mock<ProductRepository>(applicationDbContextFake);
            
            return mockUProduct;
        }

    }
}
