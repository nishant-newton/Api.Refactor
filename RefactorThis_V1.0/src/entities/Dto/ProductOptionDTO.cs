using Api.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Entities.Dto
{
    public class ProductOptionDTO : IEntity
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
