using Assignment.Utilities.Startup;
using DomainModels.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.DAL;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"), builder =>{
    builder.MigrationsAssembly(nameof(Repository));
    });
});
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.AddRepository();
WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
