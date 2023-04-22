using DotNet.Factory.Generic.DataLayer.WebApi.Models;

namespace DotNet.Factory.Generic.DataLayer.WebApi.Services;

public interface IProductService
{
  public Product? GetProduct(Guid id);
  public IReadOnlyCollection<Product>? GetProducts();
}