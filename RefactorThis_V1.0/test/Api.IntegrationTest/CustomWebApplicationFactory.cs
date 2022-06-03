using System;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RefactorThis_V1._0;
using Xero.Common.Infrastructure;
using Xero.Common.Infrastructure.Interface;
using Xero.Common.Infrastructure.Models;

namespace Api.IntegrationTest
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");
            builder.ConfigureServices(services =>
            {               

                var sp = services.BuildServiceProvider();

                var appDb = sp.GetRequiredService<IDBConnection>();

                var logger = sp.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                

                try
                {
                    SeedData.PopulateTestData(appDb);

                    Thread.Sleep(5);
                }
                catch (Exception ex)
                {

                    logger.LogError(ex, "An error occurred while populating test data");
                }
            });
        }
    }
}
