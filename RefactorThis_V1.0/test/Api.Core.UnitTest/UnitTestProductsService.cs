using System;
using Xunit;
using Moq;
using Api.Repositories.Interface;
using System.Collections.Generic;
using Api.Entities.Dto;
using System.Threading.Tasks;
using Api.Entities.Models;
using api.core.Services;
using System.Linq;

namespace Api.Core.UnitTest
{
    public class UnitTestProductsService
    {
        private ProductsService service;        
        private List<Product> products;
        private Random random;
        

        private int count = 5;

        public UnitTestProductsService()
        {
            random = new Random();
            products = new List<Product>();
            for (var i =0; i<count;i++)
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
        }
        [Fact]
        public async void TestGetProducts()
        {
            var mock = new Mock<IProductsRepository>();
            mock.Setup(x => x.GetAllProductsAsync()).Returns(Task.FromResult(products));

            service = new ProductsService(mock.Object);

            var result = await service.GetProductsAsync();
            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
            Assert.All(products, item => Assert.Contains(result.Items, x => x.Id == item.Id));
            Assert.All(products, item => Assert.Contains(result.Items, x => x.Name == item.Name));
            Assert.All(products, item => Assert.Contains(result.Items, x => x.Description == item.Description));
            Assert.All(products, item => Assert.Contains(result.Items, x => x.Price == item.Price));
            Assert.All(products, item => Assert.Contains(result.Items, x => x.DeliveryPrice == item.DeliveryPrice));
        }

        [Fact]
        public async void TestGetProductsByName()
        {
            var mock = new Mock<IProductsRepository>();

            var productName = $"Name-{random.Next(0, 4)}";
            var product = products.Where(x => x.Name == productName).ToList();            
            mock.Setup(x => x.GetProductsByNameAsync(productName)).Returns(Task.FromResult(product));

            service = new ProductsService(mock.Object);
            var result = await service.GetProductsByNameAsync(productName);
            Assert.NotNull(result);

            Assert.NotEmpty(result.Items);
            Assert.All(product, item => Assert.Contains(result.Items, x => x.Id == item.Id));
            Assert.All(product, item => Assert.Contains(result.Items, x => x.Name == item.Name));
            Assert.All(product, item => Assert.Contains(result.Items, x => x.Description == item.Description));
            Assert.All(product, item => Assert.Contains(result.Items, x => x.Price == item.Price));
            Assert.All(product, item => Assert.Contains(result.Items, x => x.DeliveryPrice == item.DeliveryPrice));
        }

        [Fact]
        public async void TestGetProductById()
        {
            var mock = new Mock<IProductsRepository>();
            var productName = $"Name-{random.Next(0, 4)}";
            var product = products.Where(x => x.Name == productName).FirstOrDefault();
            mock.Setup(x => x.GetProductById(product.Id)).Returns(Task.FromResult(product));

            service = new ProductsService(mock.Object);
            var result = await service.GetProductById(product.Id);

            Assert.NotNull(result);
            Assert.IsType<ProductDTO>(result);
            Assert.Equal(product.Id, result.Id);            
        }

        #region Negative Test Case

        [Fact]
        public async void TestGetProductByIdNotPresent()
        {
            var mock = new Mock<IProductsRepository>();
            var id = Guid.NewGuid();
            mock.Setup(x => x.GetProductById(id)).Returns(Task.FromResult<Product>(null));
            service = new ProductsService(mock.Object);
            var result = await service.GetProductById(id);
            Assert.Null(result);
        }
        #endregion
    }
}
