using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopyApps.DiscAmountSettlement.Services
{
    public class DataTableService
    {
        public static DataTable CreateDataTableFromType<T>()
        {
            DataTable table = new DataTable();
            foreach (var property in typeof(T).GetProperties())
            {
                table.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            return table;
        }

        public static DataTable ToDataTable<T>(List<T> data)
        {
            DataTable table = CreateDataTableFromType<T>();
            foreach (T model in data)
            {
                DataRow row = table.NewRow();

                foreach (var property in typeof(T).GetProperties())
                {
                    row[property.Name] = property.GetValue(model);
                }
                table.Rows.Add(row);
            }

            return table;
        }

    }
}
