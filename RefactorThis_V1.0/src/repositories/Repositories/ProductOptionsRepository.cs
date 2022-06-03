using Api.Entities.Models;
using Api.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xero.Common.Infrastructure;
using Xero.Common.Infrastructure.Interface;
using Xero.Common.Infrastructure.Models;

namespace Api.Repositories.Repositories
{
    public class ProductOptionsRepository : IProductOptionsRepository
    {

        private readonly IDBConnection connection;

        public ProductOptionsRepository(IDBConnection connection)
        {
            this.connection = connection;
        }

        public async Task<int> CreateProductOptionsAsync(ProductOption productOption)
        {
            var sql = "Insert into ProductOptions (Id, ProductId, Name, Description) " +
                      "Values (@Id, @ProductId, @Name, @Description)";
            var request = new DataRequest(sql);
            request.Parameters.Add(new DataParameter { ParameterName = "Id", Value = productOption.Id });
            request.Parameters.Add(new DataParameter { ParameterName = "ProductId", Value = productOption.ProductId });
            request.Parameters.Add(new DataParameter { ParameterName = "Name", Value = productOption.Name });
            request.Parameters.Add(new DataParameter { ParameterName = "Description", Value = productOption.Description });
            return await connection.ExecuteNonQueryAsync(request);
        }

        public async Task<List<ProductOption>> GetProductOptionsAsyncByProductId(Guid productId)
        {
            var sql = "Select Id, ProductId, Name, Description From ProductOptions Where ProductId = @ProductId";
            var request = new DataRequest(sql);            
            request.Parameters.Add(new DataParameter { ParameterName = "ProductId", Value = productId });

            var response = await connection.GetDataResponseAsync(request);
            var list = AutoMapper.Map<ProductOption>(response);
            return list;
        }

        public async Task<ProductOption> GetProductOptionAsync(Guid Id, Guid optionId)
        {
            var sql = "Select Id, ProductId, Name, Description From ProductOptions Where Id = @Id and ProductId = @ProductId";
            var request = new DataRequest(sql);
            request.Parameters.Add(new DataParameter { ParameterName = "Id", Value = optionId });
            request.Parameters.Add(new DataParameter { ParameterName = "ProductId", Value = Id });

            var response = await connection.GetDataResponseAsync(request);
            var list = AutoMapper.Map<ProductOption>(response);
            return list.FirstOrDefault();
        }

        public async Task<int> UpdateProductOptionAsync(ProductOption productOption)
        {
            var sql = "Update ProductOptions Set ProductId = @ProductId, Name = @Name, Description = @Description" +
                      " Where Id = @Id";
            var request = new DataRequest(sql);
            request.Parameters.Add(new DataParameter { ParameterName = "Id", Value = productOption.Id });
            request.Parameters.Add(new DataParameter { ParameterName = "ProductId", Value = productOption.ProductId });
            request.Parameters.Add(new DataParameter { ParameterName = "Name", Value = productOption.Name });
            request.Parameters.Add(new DataParameter { ParameterName = "Description", Value = productOption.Description });
            return await connection.ExecuteNonQueryAsync(request);
        }

        public async Task<int> DeleteProductOptionAsync(Guid productId, Guid optionId)
        {
            var sql = "Delete from ProductOptions where Id = @Id and ProductId = @ProductId";
            var request = new DataRequest(sql);
            request.Parameters.Add(new DataParameter { ParameterName = "Id", Value = optionId });
            request.Parameters.Add(new DataParameter { ParameterName = "ProductId", Value = productId });
            return await connection.ExecuteNonQueryAsync(request);
        }

        public async Task<int> DeleteProductOptionAsyncByProductId(Guid productId)
        {
            var sql = "Delete from ProductOptions where ProductId = @ProductId";
            var request = new DataRequest(sql);            
            request.Parameters.Add(new DataParameter { ParameterName = "ProductId", Value = productId });
            return await connection.ExecuteNonQueryAsync(request);
        }
    }
}
