using DotNet.Factory.Generic.DataLayer.WebApi.Models;
using DotNet.Factory.Generic.DataLayer.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.Factory.Generic.DataLayer.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductsAdoController : ControllerBase
  {
    private readonly IProductService _productService;

    public ProductsAdoController(IProductService productService)
    {
      _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    [HttpGet]
    public IActionResult Get()
    {
      var product = _productService.GetProducts();
      return product is { } ? Ok(product) : NotFound("Product Not found...");
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
      var product = _productService.GetProduct(id);
      return product is {} ? Ok(product) : NotFound("Product Not found...");
    }

  }
}
