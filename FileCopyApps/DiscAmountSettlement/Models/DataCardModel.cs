using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopyApps.DiscAmountSettlement.Models
{
    internal class DataCardModel
    {
        public string Id { get; set; }
        public string MdrType { get; set; }

        public string Principal { get; set; } = null!;

        public string CardNumber { get; set; } = null!;

        public long DiscAmount { get; set; }

        public static DataTable ToTempDataTable(List<DataCardModel> data)
        {
            DataTable table = new();
            table.Columns.Add("id", typeof(string));
            table.Columns.Add("card_number", typeof(string));
            table.Columns.Add("disc_amount", typeof(long));
            table.Columns.Add("mdr_type", typeof(string));
            table.Columns.Add("principal", typeof(string));

            foreach (DataCardModel model in data) {
                table.Rows.Add(model.Id, model.CardNumber, model.DiscAmount, model.MdrType, model.Principal);
            }

            return table;
        }
    }
}
