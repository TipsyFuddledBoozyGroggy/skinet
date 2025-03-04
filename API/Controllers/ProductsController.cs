using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand, string? type, string? sort)
        {
            return Ok(await repo.ListAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if(product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.Add(product);
            if(await repo.SaveAllAsync()){
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            return BadRequest("Cannot create the product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            if(id != product.Id || !ProductExists(id))
            {
                return BadRequest("Cannot update the product");
            }

            repo.Update(product);
            if(await repo.SaveAllAsync()){
                return NoContent();
            }
            return BadRequest("Cannot update the product");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if(product == null)
            {
                return NotFound();
            }

            repo.Delete(product);
            if(await repo.SaveAllAsync()){
                return NoContent();
            }
            return BadRequest("Cannot delete the product");
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            return Ok();
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            return Ok();
        }

        private bool ProductExists(int id)
        {
            return repo.Exists(id);
        }
    }
}
