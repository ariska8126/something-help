using FileCopyApps.DiscAmountSettlement.Models;
using FileCopyApps.DiscAmountSettlement.Services;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FileCopyApps.DiscAmountSettlement.Repositories
{
    internal class BulkCopy
    {
        public static void SqlBulkCopy(List<DataCardModel> data, string dest)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
            {
                connection.Open();
                bulkCopy.DestinationTableName = dest;
                bulkCopy.BatchSize = 5000;
                bulkCopy.BulkCopyTimeout = 300;
                bulkCopy.ColumnMappings.Add("id", "id");
                bulkCopy.ColumnMappings.Add("card_number", "card_number");
                bulkCopy.ColumnMappings.Add("disc_amount", "disc_amount");
                bulkCopy.ColumnMappings.Add("mdr_type", "mdr_type");
                bulkCopy.ColumnMappings.Add("principal", "principal");

                DataTable table = DataCardModel.ToTempDataTable(data);
                data.Clear();
                bulkCopy.WriteToServer(table);
            }
        }

        public static void SqlBulkCopyData<T>(List<T> data, string dest)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
            {
                connection.Open();
                bulkCopy.DestinationTableName = dest;
                bulkCopy.BatchSize = 5000;
                bulkCopy.BulkCopyTimeout = 300;
                AddColumnMappings<T>(bulkCopy);

                DataTable table = DataTableService.ToDataTable(data);
                data.Clear();
                bulkCopy.WriteToServer(table);
            }
        }

        public static void AddColumnMappings<T>(SqlBulkCopy bulkCopy)
        {
            foreach (var property in typeof(T).GetProperties())
            {
                bulkCopy.ColumnMappings.Add(property.Name, property.Name);
            }
        }
    }
}
