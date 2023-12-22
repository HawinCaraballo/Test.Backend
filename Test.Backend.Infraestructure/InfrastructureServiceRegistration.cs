
namespace Test.Backend.Infraestructure
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Test.Backend.Application.Contracts.Persistence;
    using Test.Backend.Infraestructure.Persistence;
    using Test.Backend.Infraestructure.Repositories;

    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastuctureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IExternalApi, ExternalApi>();
            
            return services;
        }
    }
}
