using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Xero.Common.Infrastructure.Models
{
    public class DataParameter
    {
        public DataParameter()
        {

        }

        public DataParameter(string parameterName, object value)
        {
            ParameterName = parameterName;
            Value = value;
        }      
        

        public string ParameterName { get; set; }
        
        
        public object Value { get; set; }

        
    }
}
