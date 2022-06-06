using Assignment.Utilities.Startup;
using Newtonsoft.Json;
using Repository.Mapper;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling 
    = ReferenceLoopHandling.Ignore;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o =>
{
    o.AddPolicy("",
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.AddAppDbContext(builder.Configuration);
builder.AddRepository();
builder.AddCustomServices();

WebApplication app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(o =>
{
    o.AllowAnyHeader();
    o.AllowAnyMethod()
    .SetIsOriginAllowed(r=>true)
    .AllowCredentials();
});
//app.Seed();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
