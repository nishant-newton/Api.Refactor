using Api.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repositories.Interface
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllProductsAsync();

        Task<List<Product>> GetProductsByNameAsync(string name);

        Task<Product> GetProductById(Guid Id);

        Task<int> CreateProductAsync(Product product);

        Task<int> UpdateProductAsync(Product product);

        Task<int> DeleteProductWithOptionsAsync(Guid productId);
    }
}
