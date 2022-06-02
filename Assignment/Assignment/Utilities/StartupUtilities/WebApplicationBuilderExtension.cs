using Assignment.Factory;
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
                options.UseNpgsql(Environment.GetEnvironmentVariable("Connection_String") ?? "", builder =>
                {
                    builder.MigrationsAssembly(nameof(Repository));
                    builder.MigrationsHistoryTable("__ef_assignment_migrations_history");
                }).ReplaceService
                <Microsoft.EntityFrameworkCore.Migrations.IHistoryRepository
                    , EfMigrationsHistory>();
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
            builder.Services
               .AddScoped<IFunctionalRequirementServices, FunctionalRequirementServices>();
            builder.Services
               .AddScoped<IRuntimeServices, RuntimeServices>();
            builder.Services
               .AddScoped<IJsonFactory, JsonFactory>();
            builder.Services
               .AddScoped<IFunctionalAreaTypeServices, FunctionalAreaTypeServices>();
            builder.Services
                .AddScoped<IDashboardServices, DashboardServices>();
            builder.Services
                .AddScoped<IReportServices, ReportServices>();
            builder.Services
                .AddScoped<IHistoryServices, HistoryServices>();

            return builder;
        }
    }
}
