using Api.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace api.core.Interfaces
{
    public interface IProductOptionsService
    {
        Task<bool> CreateProductOptionsAsync(ProductOptionDTO productOption);

        Task<ProductOptionDTO> GetProductOptionsAsync(Guid Id, Guid optionId);

        Task<ProductOptionsDTO> GetProductOptionsByProductId(Guid productId);

        Task<bool> UpdateProductOptionAsync(ProductOptionDTO productOptionDTO);

        Task<bool> DeleteProductOption(Guid productId, Guid optionId);
    }
}
