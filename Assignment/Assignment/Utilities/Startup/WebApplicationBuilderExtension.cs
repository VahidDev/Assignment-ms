using Repository.RepositoryServices.Abstraction;
using Repository.RepositoryServices.Implementation;

namespace Assignment.Utilities.Startup
{
    public static class WebApplicationBuilderExtension
    {
        public static WebApplicationBuilder AddRepository(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            return builder;
        }
    }
}
