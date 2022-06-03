using Api.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace api.core.Interfaces
{
    public interface IProductsService
    {
        Task<ProductsDTO> GetProductsAsync();
        Task<ProductsDTO> GetProductsByNameAsync(string name);
        Task<ProductDTO> GetProductById(Guid Id);
        Task<bool> CreateProductAsync(ProductDTO product);
        Task<bool> UpdateProductAsync(ProductDTO product);

        Task<bool> DeleteProductWithOptionsAsync(Guid productId);
    }
}
