using FileCopyApps.DiscAmountSettlement.Models;
using FileCopyApps.DiscAmountSettlement.Repositories;
using FileCopyApps.Services;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace FileCopyApps.DiscAmountSettlement.Services
{
    internal class DataCardService : BaseService
    {
        public static void SaveDataIconV() {
            
            //untuk bulk copy
            List<DataCardModel> dataCards = new List<DataCardModel>();
            //string excelFilePath = "C:\\converter\\file.xlsx";
            //string destTable = "Table_IConv";
            string excelFilePath = "C:\\converter\\file_rcs2.xlsx";
            string destTable = "[Table_RCS]";
            

            using FileStream fs = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read);
            IWorkbook workbook = new XSSFWorkbook(fs); // Use XSSFWorkbook for .xlsx files

            ISheet worksheet = workbook.GetSheetAt(0); // Assuming data is in the first sheet

            int row = 1; // Start from the second row (assuming the first row is headers)

            while (worksheet.GetRow(row) != null)
            {
                DataCardModel model = new();
                IRow currentRow = worksheet.GetRow(row);
                string id = Guid.NewGuid().ToString();
                model.Id = id;
                model.CardNumber = currentRow.GetCell(0).ToString();
                model.Principal = currentRow.GetCell(3).ToString();
                model.DiscAmount = long.Parse(currentRow.GetCell(1).ToString());
                model.MdrType = currentRow.GetCell(2).ToString();
                dataCards.Add(model);
                row++;
            }
            
            BulkCopy.SqlBulkCopy(dataCards, destTable);
        }
    }
}
