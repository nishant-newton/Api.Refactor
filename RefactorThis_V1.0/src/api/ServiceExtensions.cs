using api.core;
using api.core.Interfaces;
using api.core.Services;
using Api.Entities.Dto;
using Api.Repositories.Interface;
using Api.Repositories.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xero.Common.Infrastructure;
using Xero.Common.Infrastructure.Interface;

namespace RefactorThis_V1._0
{
    public static class ServiceExtensions
    {
        public static void AddApplicationDependencies(this IServiceCollection services )
        {
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IProductOptionsService, ProductOptionsService>();
            services.AddScoped<IProductOptionsRepository, ProductOptionsRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<ValidationFilterAttribute>();
            services.AddScoped<ValidateEntityExistsAttribute<ProductDTO>>();
        }

        public static void AddDBContext(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDBConnection, DBConnection>(x =>
            {
                return new DBConnection(connectionString);
            });
        }
    }
}
