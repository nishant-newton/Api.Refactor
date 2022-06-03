using Api.Entities.Dto;
using Api.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Entities.Extensions
{
    public static class ProductDtoExtension
    {
        
        
        public static Product ToProduct(this ProductDTO productDTO)
        {
            var product = new Product
            {
                Id = productDTO.Id,
                DeliveryPrice = productDTO.DeliveryPrice,
                Description = productDTO.Description,
                Name = productDTO.Name,
                Price = productDTO.Price,
                IsNew = true
            };
            return product;
        }

        public static ProductOption ToProductOption(this ProductOptionDTO productOptionDTO)
        {
            var productOption = new ProductOption
            {
                Id = productOptionDTO.Id,
                Description = productOptionDTO.Description,
                Name = productOptionDTO.Name,
                ProductId = productOptionDTO.ProductId
            };
            return productOption;
        }

        public static ProductOptionDTO ToProductOptionDto(this ProductOption productOption)
        {
            return GetProductOptionDTO(productOption);
        }

        public static List<ProductOptionDTO> ToProductOptionDto(this List<ProductOption> productOption)
        {
            var list = new List<ProductOptionDTO>();
            foreach(var item in productOption)
            {
                list.Add(GetProductOptionDTO(item));
            }
            return list;
        }

        public static ProductDTO ToProductDto(this Product product)
        {
            return GetProductDto(product);
        }

        public static List<ProductDTO> ToProductDto(this List<Product> product)
        {

            var list = new List<ProductDTO>();
            foreach (var item in product)
            {
                list.Add(GetProductDto(item));
            }
            return list;
        }

        private static ProductOptionDTO GetProductOptionDTO(ProductOption productOption)
        {
            return new ProductOptionDTO
            {
                Description = productOption.Description,
                Id = productOption.Id,
                Name = productOption.Name,
                ProductId = productOption.ProductId
            };
        }

        private static ProductDTO GetProductDto(Product product)
        {
            var dto = new ProductDTO
            {
                DeliveryPrice = product.DeliveryPrice,
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
            return dto;
        }
    }
}
