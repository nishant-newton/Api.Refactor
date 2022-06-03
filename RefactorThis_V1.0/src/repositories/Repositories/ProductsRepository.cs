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
    public class ProductsRepository : IProductsRepository
    {

        private readonly IDBConnection connection;
        public ProductsRepository(IDBConnection connection)
        {
            this.connection = connection;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var sql = "Select Id, Name, Description, Price, DeliveryPrice From Products";
            var request = new DataRequest(sql);
            var response = await GetProductsAsync(request);
            return response;
        }

        public async Task<List<Product>> GetProductsByNameAsync(string name)
        {
            var sql = "Select Id, Name, Description, Price, DeliveryPrice From Products Where Name = @Name";
            var request = new DataRequest(sql);
            request.Parameters.Add(new DataParameter { ParameterName = "Name", Value = name });
            var response = await GetProductsAsync(request);
            return response;
        }

        public async Task<Product> GetProductById(Guid Id)
        {
            var sql = "Select Id, Name, Description, Price, DeliveryPrice From Products Where Id = @Id";
            var request = new DataRequest(sql);
            request.Parameters.Add(new DataParameter { ParameterName = "Id", Value = Id });
            var response = await GetProductAsync(request);
            return response;
        }

        public async Task<int> CreateProductAsync(Product product)
        {
            var sql = "Insert into Products (Id, Name, Description, Price, DeliveryPrice) " +
                       "Values (@Id,@Name, @Description, @Price, @DeliveryPrice)";
            var request = new DataRequest(sql);
            request.Parameters.Add(new DataParameter { ParameterName = "Id", Value = product.Id });
            request.Parameters.Add(new DataParameter { ParameterName = "Name", Value = product.Name });
            request.Parameters.Add(new DataParameter { ParameterName = "Description", Value = product.Description });
            request.Parameters.Add(new DataParameter { ParameterName = "Price", Value = product.Price });
            request.Parameters.Add(new DataParameter { ParameterName = "DeliveryPrice", Value = product.DeliveryPrice });
            //request.Parameters.Add(new DataParameter { ParameterName = "IsNew", Value = product.IsNew });

            return await connection.ExecuteNonQueryAsync(request);
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            var sql = "Update products set Name = @Name, Description = @Description, Price = @Price, DeliveryPrice = @DeliveryPrice" +
                      " Where Id = @Id";
            var request = new DataRequest(sql);
            request.Parameters.Add(new DataParameter { ParameterName = "Id", Value = product.Id });
            request.Parameters.Add(new DataParameter { ParameterName = "Name", Value = product.Name });
            request.Parameters.Add(new DataParameter { ParameterName = "Description", Value = product.Description });
            request.Parameters.Add(new DataParameter { ParameterName = "Price", Value = product.Price });
            request.Parameters.Add(new DataParameter { ParameterName = "DeliveryPrice", Value = product.DeliveryPrice });
            //request.Parameters.Add(new DataParameter { ParameterName = "IsNew", Value = product.IsNew });

            return await connection.ExecuteNonQueryAsync(request);

        }

        public async Task<int> DeleteProductWithOptionsAsync(Guid productId)
        {
            var sql = "Delete from ProductOptions Where ProductId = @ProductId";
            var request = new DataRequest(sql);
            request.Parameters.Add(new DataParameter { ParameterName = "ProductId", Value = productId });
            try
            {
                connection.BeginTransaction();
                await connection.ExecuteNonQueryAsync(request);
                sql = "Delete from Products Where Id = @Id";
                request = new DataRequest(sql);
                request.Parameters.Add(new DataParameter { ParameterName = "Id", Value = productId });
                var productsDeleted = await connection.ExecuteNonQueryAsync(request);
                connection.Commit();
                return productsDeleted;
            }
            catch (Exception)
            {
                connection.Rollback();
                throw;
            }
            
        }

        //private async Task<int> UpsertProductAsync(DataRequest request)
        //{

        //}

        private async Task<List<Product>> GetProductsAsync(DataRequest request)
        {
            var response = await connection.GetDataResponseAsync(request);
            var list = AutoMapper.Map<Product>(response);
            return list;
        }

        private async Task<Product> GetProductAsync(DataRequest request)
        {
            var response = await connection.GetDataResponseAsync(request);
            var list = AutoMapper.Map<Product>(response);
            return list.FirstOrDefault();
        }


    }
}
