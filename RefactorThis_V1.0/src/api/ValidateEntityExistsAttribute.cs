using api.core.Interfaces;
using Api.Entities.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis_V1._0
{
    public class ValidateEntityExistsAttribute<T> : IActionFilter where T : class, IEntity
    {
        private readonly IProductsService productsService;
        public ValidateEntityExistsAttribute(IProductsService productsService)
        {
            this.productsService = productsService;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Guid id = Guid.Empty;
            if (context.ActionArguments.ContainsKey("id"))
            {
                id = (Guid)context.ActionArguments["id"];
            }
            else
            {
                context.Result = new BadRequestObjectResult("Bad id parameter");
                return;
            }
            var entity = productsService.GetProductById(id).Result;
            if (entity == null)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("entity", entity);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
