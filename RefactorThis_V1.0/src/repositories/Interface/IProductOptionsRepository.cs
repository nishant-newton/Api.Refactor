using Api.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repositories.Interface
{
    public interface IProductOptionsRepository
    {
        Task<int> CreateProductOptionsAsync(ProductOption productOption);

        Task<ProductOption> GetProductOptionAsync(Guid Id, Guid optionId);

        Task<List<ProductOption>> GetProductOptionsAsyncByProductId(Guid productId);

        Task<int> UpdateProductOptionAsync(ProductOption productOption);

        Task<int> DeleteProductOptionAsync(Guid productId, Guid optionId);
    }
}
