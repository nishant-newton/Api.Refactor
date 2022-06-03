using System;
using System.Collections.Generic;
using System.Text;

namespace Xero.Common.Infrastructure.Models
{
    public class DataResponse
    {

        public List<DataRow> Rows { get; set; } = new List<DataRow>();


    }

    public class DataRow
    {
        public List<Column> Columns { get; set; } = new List<Column>();
    }

    public class Column
    {
        public string ColumnName { get; set; }
        public object ColumnValue { get; set; }
        public string ColumnDataType { get; set; }

        public string ColumProperty { get; set; }

        public int NumericPrecision { get; set; }
        public uint NumericScale { get; set; }
    }
}
