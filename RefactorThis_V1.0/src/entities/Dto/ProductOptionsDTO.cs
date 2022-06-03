using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Entities.Dto
{
    public class ProductOptionsDTO
    {
        public List<ProductOptionDTO> Items { get; set; }

        public ProductOptionsDTO()
        {
            Items = new List<ProductOptionDTO>();
        }
    }
}
