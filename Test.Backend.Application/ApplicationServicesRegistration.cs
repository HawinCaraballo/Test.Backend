
namespace Test.Backend.Application
{
    using FluentValidation;
    using global::System.Reflection;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddLazyCache();

            return services;
        }
    }
}
