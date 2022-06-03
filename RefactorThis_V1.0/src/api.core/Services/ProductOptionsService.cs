using api.core.Interfaces;
using Api.Entities.Dto;
using Api.Entities.Extensions;
using Api.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace api.core.Services
{
    public class ProductOptionsService : ProductsService, IProductOptionsService
    {
        private readonly IProductOptionsRepository productoptionsRepository;
        public ProductOptionsService(IProductsRepository productsRepository,IProductOptionsRepository productoptionsRepository)
            :base(productsRepository)
        {
            this.productoptionsRepository = productoptionsRepository;
        }

        public async Task<bool> CreateProductOptionsAsync(ProductOptionDTO productOption)
        {
            var product = await GetProductById(productOption.ProductId);

            if(product == null)
            {
                return false;
            }

            var result = await productoptionsRepository.CreateProductOptionsAsync(productOption.ToProductOption());
            return result == 1;
        }

        public async Task<ProductOptionsDTO> GetProductOptionsByProductId(Guid productId)
        {
            var product = await GetProductById(productId);

            if(product != null)
            {
                var productOption = await productoptionsRepository.GetProductOptionsAsyncByProductId(productId);
                var productOptions = productOption.ToProductOptionDto();
                var list = new ProductOptionsDTO { Items = productOptions };
                return list;
            }
            return null;
        }

        public async Task<ProductOptionDTO> GetProductOptionsAsync(Guid Id, Guid optionId)
        {
            var product = await GetProductById(Id);

            if (product != null)
            {
                var productOption = await productoptionsRepository.GetProductOptionAsync(Id, optionId);
                if (productOption != null)
                    return productOption.ToProductOptionDto();
                   
            }
            return null;

        }

        public async Task<bool> UpdateProductOptionAsync(ProductOptionDTO productOptionDTO)
        {
            var result = await productoptionsRepository.UpdateProductOptionAsync(productOptionDTO.ToProductOption());
            return result == 1;
        }

        public async Task<bool> DeleteProductOption(Guid productId, Guid optionId)
        {
            var product = await GetProductOptionsAsync(productId, optionId);
            if (product == null)
                return false;
            var result = await productoptionsRepository.DeleteProductOptionAsync(productId, optionId);
            return result == 1;
        }
    }
}
