using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.core.Interfaces;
using Api.Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RefactorThis_V1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }
        
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Post(ProductDTO product)
        {
            var existProduct = await productsService.GetProductById(product.Id);

            if(existProduct != null)
            {
                return StatusCode(422, $"Product Id {product.Id} already exists");
            }

            var result = await productsService.CreateProductAsync(product);
            if (result)
                return CreatedAtRoute("GetById", new { id = product.Id }, product);
            else
                return StatusCode(500, "Internal server error");//Problem($"Product with Id {product.Id} already exists", null, (int)HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string name)
        {
            var result = string.IsNullOrEmpty(name) ? await productsService.GetProductsAsync() : await productsService.GetProductsByNameAsync(name);

            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{Id}", Name = "GetById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<ProductDTO>))]
        public IActionResult GetById(Guid Id)
        {
            var result = HttpContext.Items["entity"] as ProductDTO;            
            return Ok(result);
        }        

        [HttpPut("{Id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateProduct(Guid Id, ProductDTO product)
        {
            var existingProduct = await productsService.GetProductById(Id);

            if(existingProduct == null)
            {
                return NotFound($"Product Id {Id} not found");
            }
            var result = await productsService.UpdateProductAsync(product);
            if (result)
                return NoContent();
            else
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal server error");
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<ProductDTO>))]
        public async Task<IActionResult> DeleteProduct(Guid Id)
        {
            var result = await productsService.DeleteProductWithOptionsAsync(Id);
            if (result)
                return NoContent();
            else
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal server error");
            
        }


    }
}
