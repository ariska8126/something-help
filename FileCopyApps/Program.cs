using FileCopyApps.Services;
using System;
using System.Linq;
using NPOI.XSSF;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using FileCopyApps.Entities;
using NPOI.SS.Formula.Functions;
using FileCopyApps.DiscAmountSettlement.Services;
using FileCopyApps.DiscAmountSettlement.DataRCSProcessing.Services;
using FileCopyApps.DiscAmountSettlement.DataIconvProcessing.Services;

namespace FileCopyApps
{
    internal class Program : BaseService
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start.");
            #region Data RCS Full Process
            //DataRCSService.save();
            #endregion

            #region write excel
            //WriteExcelTest();
            #endregion

            #region Read Excel iConv
            //DataCardService.SaveDataIconV();
            #endregion

            #region filecopy
            //filecopy();
            #endregion

            #region Split & Insert Data Iconverter
                IConverterProcess.RunProcess();
			#endregion

			Console.WriteLine("End.");
        }

        static void WriteExcelTest()
        {
            string templatePath = @"C:\converter\template.xlsx";
            var date = DateTime.Now;
            var now = date.ToString("ddMMyyyy");
            string newfile = @"new_" + now;
            string outputPath = $@"C:\converter\{newfile}.xlsx";

            using (FileStream templateFile = new FileStream(templatePath, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook workbook = new XSSFWorkbook(templateFile);
                ISheet sheet = workbook.GetSheetAt(1);

                //List<ReportDatum> data = db.ReportData.ToList();
                //for (int i = 1; i <= 1000; i++)
                //{
                //    IRow row = sheet.CreateRow(i);
                //    ICell cell = row.CreateCell(0);
                //    cell.SetCellValue(data[i - 1].CardNumber);
                //    ICell cell2 = row.CreateCell(1);
                //    cell2.SetCellValue(data[i - 1].LastTran);
                //    ICell cell3 = row.CreateCell(2);
                //    cell3.SetCellValue(data[i - 1].TranDate);
                //    ICell cell4 = row.CreateCell(3);
                //    int code = Int32.Parse(data[i - 1].Code);
                //    cell4.SetCellValue(code);
                //    ICell cell5 = row.CreateCell(4);
                //    cell5.SetCellValue((double)data[i - 1].Amt);
                //    ICell cell6 = row.CreateCell(5);
                //    cell6.SetCellValue(data[i - 1].Pb);
                //    ICell cell7 = row.CreateCell(6);
                //    cell7.SetCellValue(data[i - 1].Pbasli);
                //    ICell cell8 = row.CreateCell(7);
                //    cell8.SetCellValue(data[i - 1].Nwk);
                //    ICell cell9 = row.CreateCell(8);
                //    cell9.SetCellValue(data[i - 1].Ket);
                //    ICell cell10 = row.CreateCell(9);
                //    cell10.SetCellValue(data[i - 1].KetAsli);
                //}

                // Simpan workbook ke file baru
                using (FileStream outputFile = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(outputFile);
                }
            }
        }
        static void filecopy() {
            Console.WriteLine("File Copy Start.");
            string sourceFilePath = "C:\\iGoAML\\Temp\\XML\\LTKL-20231130-S06ITR0738800001.xml"; // Ganti dengan path file sumber yang ingin disalin
            string destinationDirectory = "C:\\iGoAML\\Temp\\XML\\"; // Ganti dengan path direktori tempat file akan disimpan
            int numberOfCopies = 55000; // Ganti dengan jumlah copy yang diinginkan (x)

            CopyFileWithIncrement(sourceFilePath, destinationDirectory, numberOfCopies);
            Console.WriteLine("File Copy Completed.");
        }

        static void CopyFileWithIncrement(string sourceFilePath, string destinationDirectory, int numberOfCopies)
        {
            if (!File.Exists(sourceFilePath))
            {
                Console.WriteLine("File sumber tidak ditemukan.");
                return;
            }

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(sourceFilePath);
            fileNameWithoutExtension = fileNameWithoutExtension.Substring(0, fileNameWithoutExtension.Length - 6);
            string fileExtension = Path.GetExtension(sourceFilePath);

            for (int i = 2; i <= numberOfCopies; i++)
            {
                string incrementedFileName = $"{fileNameWithoutExtension}{i:D6}{fileExtension}";
                string destinationFilePath = Path.Combine(destinationDirectory, incrementedFileName);

                File.Copy(sourceFilePath, destinationFilePath);

                Console.WriteLine($"File {sourceFilePath} berhasil disalin sebagai {destinationFilePath}");
            }
        }
    }
}