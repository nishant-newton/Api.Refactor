using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Entities.Dto
{
    public class ProductsDTO
    {
        public List<ProductDTO> Items { get; set; }

        public ProductsDTO()
        {
            Items = new List<ProductDTO>();
        }
    }
}
