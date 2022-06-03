using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Xero.Common.Infrastructure.Models
{
    public class DataRequest
    {
        public DataRequest()
        {
            Parameters = new List<DataParameter>();
        }

        public DataRequest(string commandText)
            : this()
        {
            Command = commandText;
        }

        public string Command { get; set; }

        public CommandType? CommandType { get; set; }

        public List<DataParameter> Parameters { get; set; }
    }
}
