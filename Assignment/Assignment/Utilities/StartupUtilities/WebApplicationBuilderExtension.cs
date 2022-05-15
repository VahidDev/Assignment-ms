using Assignment.Services.Abstraction;
using Assignment.Services.Implementation;
using DomainModels.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Repository.DAL;
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
        public static WebApplicationBuilder AddAppDbContext
            (this WebApplicationBuilder builder,IConfiguration configuration)
        {
            builder.Services.AddDbContext<AppDbContext>(options => {
                options.UseNpgsql(configuration.GetConnectionString("Default"), builder =>
                {
                    builder.MigrationsAssembly(nameof(Repository));
                    builder.MigrationsHistoryTable("__ef_assignment_migrations_history");
                }).ReplaceService<IHistoryRepository, EfMigrationsHistory>();
                });
            return builder;
        }
        public static WebApplicationBuilder AddCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddScoped<IAssignmentServices, AssignmentServices>();
            builder.Services
                .AddScoped<IRoleOfferServices, RoleOfferServices>();
            builder.Services
                .AddScoped<IFileServices, FileServices>();
            builder.Services
                .AddScoped<ITemplateServices, TemplateServices>();
            return builder;
        }
    }
}
