using FileCopyApps.DiscAmountSettlement.DataRCSProcessing.Models;
using FileCopyApps.DiscAmountSettlement.Models;
using FileCopyApps.DiscAmountSettlement.Repositories;
using FileCopyApps.Services;
using FileCopyApps.Utilities;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopyApps.DiscAmountSettlement.DataRCSProcessing.Services
{
    internal class DataRCSService : BaseService
    {
        public static void save()
        {
            List<DataRCS> data = new();

            const string fileNameList = "file.txt";
            List<string> fileList = fileUtils.ReadEachRow(fileNameList);

            string baseFilePath = "C:\\converter\\";
            string destTable = "[Table_RCS_complete_data]";

            int row = 1;

            foreach (string file in fileList)
            {
                data = new();
                string excelFilePath = baseFilePath + file;
                
                ISheet worksheet = ExcelUtils.init(excelFilePath, 1);
                while (worksheet.GetRow(row) != null)
                {
                    IRow currentRow = worksheet.GetRow(row);
                    DataRCS dataPart = ExcelUtils.PopulateModelFromRow<DataRCS>(currentRow);
                    dataPart.SourceFile = file;
                    data.Add(dataPart);
                    row++;
                }
                BulkCopy.SqlBulkCopyData(data, destTable);
            }
        }
    }
}
