using System.Data;

using DotNet.Factory.Generic.DataLayer.WebApi.Models;

namespace DotNet.Factory.Generic.DataLayer.WebApi.Services;

public class ProductServices : IProductService
{
  private readonly DbManager _dbManager;
  private readonly IConfiguration _configuration;
  public ProductServices(IConfiguration configuration)
  {
    _configuration = configuration;
    _dbManager = new DbManager("SampleDb", _configuration);
  }

  public Product? GetProduct(Guid id)
  {
    var productDt = _dbManager.GetDataTable($"select * from Products where Id = '{id}';", CommandType.Text);
    var product = GetProductFromDataTable(productDt);
    return product;
  }

  public IReadOnlyCollection<Product>? GetProducts()
  {
    var productDt = _dbManager.GetDataTable($"select * from Products;", CommandType.Text);
    var product = GetAllProductFromDataTable(productDt);
    return product;
  }

  private IReadOnlyCollection<Product> GetAllProductFromDataTable(DataTable dataTable)
  {
    var products = new List<Product>();
    if (dataTable == null && dataTable!.Rows.Count <= 0)
    {
      return (IReadOnlyCollection<Product>)Enumerable.Empty<Product>();
    }

    foreach (DataRow row in dataTable.Rows)
    {
      var product = new Product()
      {
        Id = Guid.Parse(row["ID"].ToString()!),
        Name = row["Name"].ToString()!,
        Description = row["Description"].ToString()!,
        Price = Convert.ToDouble(row["Price"].ToString()!)
      };
      products.Add(product);
    }

    return products;
  }

  private Product? GetProductFromDataTable(DataTable dataTable)
  {
    Product? product = null;
    if (dataTable == null && dataTable!.Rows.Count <= 0) return product;
    product = new Product()
    {
      Id = Guid.Parse(dataTable.Rows[0]["ID"].ToString()!),
      Name = dataTable.Rows[0]["Name"].ToString()!,
      Description = dataTable.Rows[0]["Description"].ToString()!,
      Price = Convert.ToDouble(dataTable.Rows[0]["Price"].ToString()!)
    };

    return product;

  }
}