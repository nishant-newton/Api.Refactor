using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Entities.Interface
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
