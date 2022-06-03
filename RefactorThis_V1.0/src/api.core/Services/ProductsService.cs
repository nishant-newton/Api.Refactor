using api.core.Interfaces;
using Api.Entities.Dto;
using Api.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities.Extensions;
using Api.Repositories.Interface;

namespace api.core.Services
{
    public class ProductsService : IProductsService
    {

        private readonly IProductsRepository productsRepository;
        public ProductsService(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<ProductsDTO> GetProductsAsync()
        {
            var list = await productsRepository.GetAllProductsAsync();
            if (list != null)
            {
                var productDto = list.ToProductDto();
                var response = new ProductsDTO { Items = productDto };
                return response;
            }
            return null;
        }

        public async Task<ProductsDTO> GetProductsByNameAsync(string name)
        {
            var list = await productsRepository.GetProductsByNameAsync(name);
            if (list != null && list.Count > 0)
            {
                var productDto = list.ToProductDto();
                var response = new ProductsDTO { Items = productDto };
                return response;
            }
            return null;
        }

        public async Task<ProductDTO> GetProductById(Guid Id)
        {
            var product = await productsRepository.GetProductById(Id);
            return product != null ? product.ToProductDto() : null;
        }

        public async Task<bool> CreateProductAsync(ProductDTO product)
        {
            var result = await productsRepository.CreateProductAsync(product.ToProduct());
            return result == 1;
        }

        public async Task<bool> UpdateProductAsync(ProductDTO product)
        {
            var result = await productsRepository.UpdateProductAsync(product.ToProduct());
            return result == 1;
        }

        public async Task<bool> DeleteProductWithOptionsAsync(Guid productId)
        {
            var result = await productsRepository.DeleteProductWithOptionsAsync(productId);
            return result == 1;
        }
    }
}
