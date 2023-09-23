using Catalog.Data.Dtos;
using Catalog.Data.Entities;
using Catalog.Infrastructure.IRepositories;
using Catalog.Infrastructure.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("service/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IBaseRepository<Product> repository;
        private readonly ILogger<CatalogController> logger;
        public CatalogController(IBaseRepository<Product> repository, ILogger<CatalogController> logger)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            try
            {
                var products = await repository.GetAllAsync();
                var productDTOs = products.Select(x => x.MapToDTO()).ToList();
                return Ok(productDTOs);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching products.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching products.");
            }
        }

        [HttpGet("{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductDTO>> GetProductById(string id)
        {
            try
            {
                var product = await repository.GetByIdAsync(id);
                if (product == null) return NotFound("Product not found.");
                
                var productDTO = product.MapToDTO();
                return Ok(productDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching a product by ID.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching a product by ID.");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody] ProductDTO productDTO)
        {
            try
            {
                if (productDTO == null) return BadRequest("Invalid product data.");
               
                var product = productDTO.MapToEntity();
                await repository.InsertAsync(product);

                return CreatedAtRoute("GetProductById", new { id = product.Id }, productDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating a product.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while creating a product.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(string id, [FromBody] ProductDTO productDTO)
        {
            try
            {
                if (productDTO == null) return BadRequest("Invalid product data.");               

                var existingProduct = await repository.GetByIdAsync(id);
                if (existingProduct == null)  return NotFound("Product not found.");               

                await repository.UpdateAsync(id, existingProduct);
                return Ok(productDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while updating a product.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while updating a product.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                var existingProduct = await repository.GetByIdAsync(id);
                if (existingProduct == null)  return NotFound("Product not found.");                

                await repository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while deleting a product.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while deleting a product.");
            }
        }

    }
}
