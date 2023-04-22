using DotNet.Factory.Generic.DataLayer.WebApi.Extensions;
using DotNet.Factory.Generic.DataLayer.WebApi.Infrastructure;
using DotNet.Factory.Generic.DataLayer.WebApi.Services;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("SampleDb")));
builder.Services.AddScoped<IProductService, ProductServices>();

var app = builder.Build();

app.MigrateDatabase<ProductContext>((context, service) =>
{
  var logger = service.GetService<ILogger<ProductContextSeed>>();
  ProductContextSeed
    .SeedAsync(context, logger!)
    .Wait();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
