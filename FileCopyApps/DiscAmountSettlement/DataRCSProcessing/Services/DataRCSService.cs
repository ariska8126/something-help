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
            List<string> fileList = new List<string>
            {
                //"BND350.D2024079.T0143136-18032024.xlsx",
                //"BND350H.D2024076.T2342414-16032024.xlsx",
                //"BND350H.D2024077.T2339495-17032024.xlsx",
                //"BND350UI.D2024077.T0416435-16032024.xlsx",
                //"BND350UI.D2024078.T0047128-17032024.xlsx",
                "BND350UI.D2024079.T0151077-18032024.xlsx"
                //"BNUSUIBGvsBND350UI-20240316.xlsx" //row tidak seragam sepertinya untuk jalin
                //"BNUSUIBGvsBND350UI-20240317.xlsx",
                //"BNUSUIBGvsBND350UI-20240318.xlsx",
                //"BNUSUIJvsBND350UI-20240316.xlsx",
                //"BNUSUIJvsBND350UI-20240317.xlsx",
                //"BNUSUIJvsBND350UI-20240318.xlsx"
            };

            string baseFilePath = "C:\\converter\\";
            string destTable = "[Table_RCS_complete_data]";

            int row = 1;

            foreach (string file in fileList)
            {
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
            }

            BulkCopy.SqlBulkCopyData(data, destTable);
        }
    }
}
