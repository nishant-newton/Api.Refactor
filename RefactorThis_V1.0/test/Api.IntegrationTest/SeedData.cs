using Api.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xero.Common.Infrastructure.Interface;
using Xero.Common.Infrastructure.Models;

namespace Api.IntegrationTest
{
    public static class SeedData
    {
        public static void PopulateTestData(IDBConnection connection)
        {
            var sql = "Drop Table If Exists Products";
            var request = new DataRequest(sql);
            var result = connection.ExecuteNonQueryAsync(request).Result;

            sql = "Create Table Products (Id varchar(36),Name varchar(17), Description nvarchar(35), Price decimal(6,2), DeliveryPrice decimal(4,2))";

            request = new DataRequest(sql);
            result = connection.ExecuteNonQueryAsync(request).Result;

            var list = GetProducts();

            foreach (var item in list)
            {
                sql = "Insert into Products (Id, Name, Description, Price, DeliveryPrice)" +
                " Values(@Id,@Name,@Description,@Price,@DeliveryPrice)";
                request = new DataRequest(sql);
                request.Parameters.Add(new DataParameter { ParameterName = "Id", Value = item.Id });
                request.Parameters.Add(new DataParameter { ParameterName = "Name", Value = item.Name });
                request.Parameters.Add(new DataParameter { ParameterName = "Description", Value = item.Description });
                request.Parameters.Add(new DataParameter { ParameterName = "Price", Value = item.Price });
                request.Parameters.Add(new DataParameter { ParameterName = "DeliveryPrice", Value = item.DeliveryPrice });

                result = connection.ExecuteNonQueryAsync(request).Result;
            }

        }

        

        private static List<Product> GetProducts()
        {
            var products = new List<Product>();
            for (var i = 0; i < 5; i++)
            {
                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = $"Name-{i}",
                    DeliveryPrice = 12.45,
                    Description = $"Description-{i}",
                    Price = 123.45,
                    IsNew = true
                };
                products.Add(product);
            }
            return products;
        }
    }
}
