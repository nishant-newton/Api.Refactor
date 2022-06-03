using Api.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Entities.Dto
{
    public class ProductDTO : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public double DeliveryPrice { get; set; }
    }
}
