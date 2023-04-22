using DotNet.Factory.Generic.DataLayer.WebApi.Models;

namespace DotNet.Factory.Generic.DataLayer.WebApi.Infrastructure
{
  public class ProductContextSeed
  {
    public static async Task SeedAsync(ProductContext orderContext, ILogger<ProductContextSeed> logger)
    {
      if (!orderContext.Products.Any())
      {
        await orderContext.Products.AddRangeAsync(GetPreconfiguredOrders());
        await orderContext.SaveChangesAsync();

        logger.LogInformation("Seed database associated with context {DbContextName}", typeof(ProductContext).Name);
      }
    }

    private static IEnumerable<Product> GetPreconfiguredOrders()
    {
      return new List<Product>
      {
        new()
        {
          Id = Guid.NewGuid(),
          Name = "Product 1",
          Description = "Product Description 1",
          Price = 79.99
        },
        new(){
          Id = Guid.NewGuid(),
          Name = "Product 2",
          Description = "Product Description 2",
          Price = 49.99
        },
        new(){
          Id = Guid.NewGuid(),
          Name = "Product 3",
          Description = "Product Description 3",
          Price = 99.99
        }
      };
    }
  }
}
