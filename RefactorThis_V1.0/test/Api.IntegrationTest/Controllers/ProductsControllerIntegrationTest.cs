using Api.Entities.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using RefactorThis_V1._0;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTest.Controllers
{
    public class ProductsControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {

        private readonly HttpClient client;

        public ProductsControllerIntegrationTest(CustomWebApplicationFactory<Startup> factory)
        {            
            client = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetProducts()
        {
                       
            var httpResponse = await client.GetAsync("api/products");
            

            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<ProductsDTO>(stringResponse);

            Assert.NotNull(products);

            Assert.NotEmpty(products.Items);
        }
        
    }
}
