using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNet.Factory.Generic.DataLayer.WebApi.Infrastructure;
using DotNet.Factory.Generic.DataLayer.WebApi.Models;

namespace DotNet.Factory.Generic.DataLayer.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductsEfController : ControllerBase
  {
    private readonly ProductContext _context;

    public ProductsEfController(ProductContext context)
    {
      _context = context;
    }

    // GET: api/ProductsEf
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
      if (_context.Products == null)
      {
        return NotFound();
      }
      return await _context.Products.ToListAsync();
    }

    // GET: api/ProductsEf/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(Guid id)
    {
      if (_context.Products == null)
      {
        return NotFound();
      }
      var product = await _context.Products.FindAsync(id);

      if (product == null)
      {
        return NotFound();
      }

      return product;
    }

    // PUT: api/ProductsEf/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(Guid id, Product product)
    {
      if (id != product.Id)
      {
        return BadRequest();
      }

      _context.Entry(product).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ProductExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/ProductsEf
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
      if (_context.Products == null)
      {
        return Problem("Entity set 'ProductContext.Products'  is null.");
      }
      _context.Products.Add(product);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }

    // DELETE: api/ProductsEf/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
      if (_context.Products == null)
      {
        return NotFound();
      }
      var product = await _context.Products.FindAsync(id);
      if (product == null)
      {
        return NotFound();
      }

      _context.Products.Remove(product);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool ProductExists(Guid id)
    {
      return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}
