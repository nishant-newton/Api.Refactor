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
    [Route("api/products")]
    [ApiController]
    public class ProductOptionsController : ControllerBase
    {

        private readonly IProductOptionsService productOptionsService;

        public ProductOptionsController(IProductOptionsService productOptionsService)
        {
            this.productOptionsService = productOptionsService;
        }
        ///products/{id}/options` - adds a new product option to the specified product.
        [HttpPost("{id}/options")]
        public async Task<IActionResult> CreateProductOptions(Guid id, [FromBody] ProductOptionDTO productOptionDTO)
        {
            var existProductOption = await productOptionsService.GetProductOptionsAsync(id, productOptionDTO.Id);

            if(existProductOption != null)
            {
                return StatusCode(422, $"Product Option Id  {productOptionDTO.Id} for Product Id {id} already exists");
            }
            var response = await productOptionsService.CreateProductOptionsAsync(productOptionDTO);
            if(response)
                return CreatedAtRoute("GetById", new { id = id,optionId = productOptionDTO.Id }, productOptionDTO);
            else
                return StatusCode(500, "Internal server error");
        }
        //10. `PUT /products/{id}/options/{optionId}` - updates the specified product option.
        [HttpPut("{id}/options/{optionId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateProducOption(Guid id, [FromBody] ProductOptionDTO productOptionDTO)
        {
            var productOption = await productOptionsService.GetProductOptionsByProductId(id);

            if(productOption == null)
            {
                return NotFound($"Product Option for Product Id {id} not found");
            }
            
            var response = await productOptionsService.UpdateProductOptionAsync(productOptionDTO);

            if (response)
                return NoContent();
            else
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal server error");
        }

        //11. `DELETE /products/{id}/options/{optionId}` - deletes the specified product option.
        [HttpDelete("{id}/options/{optionId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> DeleteProductOptionById(Guid id,Guid optionId)
        {
            var productOption = await productOptionsService.GetProductOptionsAsync(id, optionId);

            if (productOption == null)
                return NotFound();

            var response = await productOptionsService.DeleteProductOption(id, optionId);

            if (response)
                return NoContent();
            else
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal server error");

        }

        //GET /products/{id}/options` - finds all options for a specified product.
        [HttpGet("{id}/options")]
        public async Task<IActionResult> GetOptionsByProductId(Guid id)
        {
            var response = await productOptionsService.GetProductOptionsByProductId(id);
            
            return Ok(response);
        }
        ///GET /products/{id}/options/{optionId}` - finds the specified product option for the specified product.
        [HttpGet("{id}/options/{optionId}")]
        
        public async Task<IActionResult> GetById(Guid id, Guid optionId)
        {
            var response = await productOptionsService.GetProductOptionsAsync(id, optionId);
            if (response == null)
                return NotFound();
            return Ok(response);
        }        
    }
}
