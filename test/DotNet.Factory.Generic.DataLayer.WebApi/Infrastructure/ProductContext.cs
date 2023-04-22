using DotNet.Factory.Generic.DataLayer.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNet.Factory.Generic.DataLayer.WebApi.Infrastructure
{
  public class ProductContext : DbContext
  {
    public ProductContext(DbContextOptions<ProductContext> options) :
      base(options)
    {
      
    }
    public DbSet<Product> Products{ get; set; }
  }
}
