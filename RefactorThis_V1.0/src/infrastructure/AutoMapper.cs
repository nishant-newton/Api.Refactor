using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xero.Common.Infrastructure.Models;

namespace Xero.Common.Infrastructure
{
    public class AutoMapper
    {
        public static List<T> Map<T>(DataResponse response)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            foreach (var rows in response.Rows)
            {
                obj = Activator.CreateInstance<T>();
                foreach (var col in rows.Columns)
                {

                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {
                        if (prop.Name == col.ColumnName)
                        {
                            if (col.ColumnValue != null)
                            {
                                var type = prop.PropertyType;
                                var underlyingType = Nullable.GetUnderlyingType(type);
                                var returnType = underlyingType ?? type;

                                if (prop.CanWrite)
                                {
                                    if (returnType == typeof(Guid))
                                    {
                                        prop.SetValue(obj, Guid.Parse(col.ColumnValue.ToString()), null);
                                    }
                                    else
                                    {
                                        prop.SetValue(obj, Convert.ChangeType(col.ColumnValue, returnType), null);
                                    }
                                }

                                break;
                            }
                        }

                    }


                }
                list.Add(obj);
            }
            return list;
        }
    }
}
