using FileCopyApps.DiscAmountSettlement.DataIconvProcessing.Models;
using FileCopyApps.DiscAmountSettlement.DataRCSProcessing.Models;
using FileCopyApps.DiscAmountSettlement.Repositories;
using FileCopyApps.Services;
using FileCopyApps.Utilities;
using Microsoft.Data.SqlClient;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopyApps.DiscAmountSettlement.DataIconvProcessing.Services
{
	internal class DataIconvServices : BaseService
	{
		private static readonly string pathSource = ConfigurationManager.AppSettings.Get("dirConverterServiceSrcFile");
		private static readonly string pathDestination = ConfigurationManager.AppSettings.Get("dirConverterServiceDestFile");
		public static void SaveBnd370Success()
		{
			List<Bnd370_Success> data = new();

			Console.WriteLine("Start Insert");

			string destTable = "[bnd370_success]";

			string[] dirs = Directory.GetFiles(pathSource);
			//foreach (string file in fileList)
			foreach (string dir in dirs)
			{
				//string excelFilePath = pathSource + file;
				string filename = Path.GetFileName(dir);
				if (!filename.Contains("bnd370success_"))
				{
					continue;
				}
				int row = 1;

				ISheet worksheet = ExcelUtils.init(dir, 0);
				while (worksheet.GetRow(row) != null)
				{
					IRow currentRow = worksheet.GetRow(row);
					Bnd370_Success dataPart = ExcelUtils.PopulateModelFromRow<Bnd370_Success>(currentRow);
					//dataPart.SourceFile = file;
					data.Add(dataPart);
					row++;
				}

				BulkCopy.SqlBulkCopyData(data, destTable);
				data.Clear();
				Console.WriteLine($"Success process file {filename} - {row} row - {DateTime.Now.ToString("HH:mm")}");
				MoveFile(dir, $"{pathDestination}{filename}");
			}

			Console.WriteLine("End Insert");
			Console.ReadKey();
		}

		public static void SaveBnd370File()
		{
			List<Bnd370_File> data = new();

			Console.WriteLine("Start Insert");

			string destTable = "[bnd370_file]";

			string[] dirs = Directory.GetFiles(pathSource);
			//foreach (string file in fileList)
			foreach (string dir in dirs)
			{
				//string excelFilePath = pathSource + file;
				string filename = Path.GetFileName(dir);
				if (!filename.Contains("bnd370file_"))
				{
					continue;
				}
				int row = 1;

				ISheet worksheet = ExcelUtils.init(dir, 0);
				while (worksheet.GetRow(row) != null)
				{
					IRow currentRow = worksheet.GetRow(row);
					Bnd370_File dataPart = ExcelUtils.PopulateModelFromRow<Bnd370_File>(currentRow);
					//dataPart.SourceFile = file;
					data.Add(dataPart);
					row++;
				}

				BulkCopy.SqlBulkCopyData(data, destTable);
				data.Clear();
				Console.WriteLine($"Success process file {filename} - {row} row - {DateTime.Now.ToString("HH:mm")}");
				MoveFile(dir, $"{pathDestination}{filename}");
			}

			Console.WriteLine("End Insert");
			Console.ReadKey();
		}

		internal static void MoveFile(string source, string destination)
		{
			if (File.Exists(destination))
				File.Delete(destination);

			File.Move(source, destination);
			Console.WriteLine("File has moved");
		}
	}
}
