using FileCopyApps.DiscAmountSettlement.DataIconvProcessing.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.OpenXmlFormats;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Configuration;
using NPOI.POIFS.NIO;

namespace FileCopyApps.DiscAmountSettlement.DataIconvProcessing.Services
{
	internal class IConvSplitService
	{
		private static readonly string pathSplitSource = ConfigurationManager.AppSettings.Get("dirSplitSource");
		private static readonly string pathSplitDestination = ConfigurationManager.AppSettings.Get("dirSplitDest");
		private static readonly string pathSplitDone = ConfigurationManager.AppSettings.Get("dirSplitDone");
		private static readonly string splitSize = ConfigurationManager.AppSettings.Get("splitSize");
		public static void SplitCsvBnd370SuccessToExcel()
		{
			List<string[]> errorRecord = new List<string[]>();
			List<Bnd370_Success> record = new List<Bnd370_Success>();

			Console.WriteLine("Starting split");

			int lineNumber = 0;
			int recordSuccessNumber = 0;
			int recordSplit = Int32.Parse(splitSize);
			int fileGenerateNumber = 1;

			List<string> headers = new List<string>
		{
			"MerchantId",
			"BranchCode",
			"MerchantName",
			"MerchantCompany",
			"MerchantAddress1",
			"MerchantAddress2",
			"MerchantAddress3",
			"AccountBankName",
			"AccountNumber",
			"AccountBeneficiary",
			"OrigBatch",
			"BatchNum",
			"SeqNum",
			"CardType",
			//"TrxDate",
			"AuthCode",
			"InstInd",
			"TrxType",
			"CardNum",
			"TrxAmount",
			"MdrRate",
			"DiscAmount",
			"NetAmount",
			//"FileDate",
			"TerminalId",
			"AirfareTax",
			"NonAirfareTax",
			"Remarks",
			"LastFileID",
			"MdrType",
			"CardPrincipal",
			"RekDr",
			"KetRekDR",
			"RekCr",
			"KetRekCR",
			"file_cemtex_debit_off_us"
			};

			string[] dirs = Directory.GetFiles(pathSplitSource);

			//foreach (string file in fileList)
			foreach (string dir in dirs)
			{
				string filename = Path.GetFileName(dir);

				if (!filename.Contains("bnd370success_"))
				{
					continue;
				}
				//string path = baseFilePath + file;
				using (var reader = new StreamReader(dir))
				{
					string line;
					while ((line = reader.ReadLine()) != null)
					{
						lineNumber++;
						string[] fields = line.Split(';'); // Adjust the delimiter based on your file structure
						
						// Ensure that the number of fields matches the number of properties in Bnd370Success class
						if ((fields.Length - 2) == typeof(Bnd370_Success).GetProperties().Length)
						{
							recordSuccessNumber++;
							// Map fields to Bnd370Success object
							var bndSuccess = new Bnd370_Success
							{
								MerchantId = int.Parse(fields[0]),
								BranchCode = int.Parse(fields[1]),
								MerchantName = fields[2],
								MerchantCompany = fields[3],
								MerchantAddress1 = fields[4],
								MerchantAddress2 = fields[5],
								MerchantAddress3 = fields[6],
								AccountBankName = fields[7],
								AccountNumber = fields[8],
								AccountBeneficiary = fields[9],
								OrigBatch = int.Parse(fields[10]),
								BatchNum = int.Parse(fields[11]),
								SeqNum = int.Parse(fields[12]),
								CardType = fields[13],
								//TrxDate = DateTime.Now,
								AuthCode = fields[15],
								InstInd = fields[16],
								TrxType = fields[17],
								CardNum = fields[18],
								TrxAmount = int.Parse(fields[19]),
								MdrRate = decimal.Parse(fields[20]),
								DiscAmount = int.Parse(fields[21]),
								NetAmount = int.Parse(fields[22]),
								//FileDate = DateTime.Now,
								TerminalId = fields[24],
								AirfareTax = int.Parse(fields[25]),
								NonAirfareTax = int.Parse(fields[26]),
								Remarks = fields[27],
								LastFileID = long.Parse(fields[28]),
								MdrType = fields[29],
								CardPrincipal = fields[30],
								RekDr = fields[31],
								KetRekDR = fields[32],
								RekCr = fields[33],
								KetRekCR = fields[34],
								file_cemtex_debit_off_us = fields[35]
							};
							record.Add(bndSuccess);
						}
						else
						{
							// Handle mismatched number of fields
							Console.WriteLine($"Error: Mismatched number of fields in line {lineNumber}. Skipping line in file {filename.Split(".")[0] + fileGenerateNumber}.xlsx.");
							errorRecord.Add(fields);
						}

						//generate excel with size as record split
						if (recordSuccessNumber == recordSplit && record.Count != 0)
						{
							string successFilePath = $"{filename.Split(".")[0]}_{fileGenerateNumber}.xlsx";
							CreateExcelPackage<Bnd370_Success>(record, headers, $"{pathSplitDestination + successFilePath}");
							record.Clear();
							fileGenerateNumber++;
							recordSuccessNumber = 0;
						}
					}

					//generate excel rest of data less than record split
					if (recordSuccessNumber < recordSplit && record.Count != 0)
					{
						string successFilePath = $"{filename.Split(".")[0]}_{fileGenerateNumber}.xlsx";
						CreateExcelPackage<Bnd370_Success>(record, headers, $"{pathSplitDestination + successFilePath}");
						Console.WriteLine($"Success create {successFilePath} with {recordSuccessNumber} record data");
						record.Clear();
						fileGenerateNumber++;
						recordSuccessNumber = 0;
					}
				}

				if (errorRecord.Count > 0)
				{
					string errorFileName = $"{filename.Split(".")[0]}_{fileGenerateNumber}_error";
					WriteCSV($"{pathSplitDestination + errorFileName}", errorRecord, headers);
					errorRecord.Clear();
				}

				DataIconvServices.MoveFile(dir, pathSplitDone + filename);
			}

			Console.WriteLine("Split was complete, enter to close");
			Console.ReadKey();
		}

		public static void SplitCsvBnd370FileToExcel()
		{
			List<string[]> errorRecord = new List<string[]>();
			List<Bnd370_File> record = new List<Bnd370_File>();

			Console.WriteLine("Starting split");

			int lineNumber = 0;
			int recordSuccessNumber = 0;
			int recordSplit = Int32.Parse(splitSize);
			int fileGenerateNumber = 1;

			List<string> headers = new List<string>
		{
			"filename",
			"status",
			"trx_success",
			"trx_failed",
			"remarks",
			"process_dt",
			"cemtex_file_onus",
			"cemtex_file_offus",
			"trx_onus",
			"trx_offus",
			"cemtex_file_onus_Debit",
			"trx_onus_Debit",
			"cemtex_file_offus_Debit",
			"trx_offus_Debit",
			"LastId"

			};

			string[] dirs = Directory.GetFiles(pathSplitSource);

			//foreach (string file in fileList)
			foreach (string dir in dirs)
			{
				string filename = Path.GetFileName(dir);

				if (!filename.Contains("bnd370file_"))
				{
					continue;
				}

				//string path = baseFilePath + file;
				using (var reader = new StreamReader(dir))
				{
					string line;
					while ((line = reader.ReadLine()) != null)
					{
						lineNumber++;
						string[] fields = line.Split(';'); // Adjust the delimiter based on your file structure

						// Ensure that the number of fields matches the number of properties in Bnd370Success class
						if ((fields.Length) == typeof(Bnd370_File).GetProperties().Length)
						{
							recordSuccessNumber++;
							// Map fields to Bnd370Success object
							var bndSuccess = new Bnd370_File
							{
								filename = fields[1],
								status = fields[2],
								trx_success = long.Parse(fields[3]),
								trx_failed = long.Parse(fields[4]),
								remarks = fields[5].Trim('"').Trim(),
								process_dt = DateTime.Now,
								cemtex_file_onus = fields[7],
								cemtex_file_offus = fields[8],
								trx_onus = long.Parse(fields[9]),
								trx_offus = long.Parse(fields[10]),
								cemtex_file_onus_Debit = fields[11],
								trx_onus_Debit = long.Parse(fields[12]),
								cemtex_file_offus_Debit = fields[13],
								trx_offus_Debit = long.Parse(fields[14]),
								LastId = long.Parse(fields[0])

							};
							record.Add(bndSuccess);
						}
						else
						{
							// Handle mismatched number of fields
							Console.WriteLine($"Error: Mismatched number of fields in line {lineNumber}. Skipping line in file {filename.Split(".")[0] + fileGenerateNumber}.xlsx.");
							errorRecord.Add(fields);
						}

						//generate excel with size as record split
						if (recordSuccessNumber == recordSplit && record.Count != 0)
						{
							string successFilePath = $"{filename.Split(".")[0]}_{fileGenerateNumber}.xlsx";
							CreateExcelPackage<Bnd370_File>(record, headers, $"{pathSplitDestination + successFilePath}");
							record.Clear();
							fileGenerateNumber++;
							recordSuccessNumber = 0;
						}
					}

					//generate excel rest of data less than record split
					if (recordSuccessNumber < recordSplit && record.Count != 0)
					{
						string successFilePath = $"{filename.Split(".")[0]}_{fileGenerateNumber}.xlsx";
						CreateExcelPackage<Bnd370_File>(record, headers, $"{pathSplitDestination + successFilePath}");
						Console.WriteLine($"Success create {successFilePath} with {recordSuccessNumber} record data");
						record.Clear();
						fileGenerateNumber++;
						recordSuccessNumber = 0;
					}
				}

				if (errorRecord.Count > 0)
				{
					string errorFileName = $"{filename.Split(".")[0]}_{fileGenerateNumber}_error";
					WriteCSV($"{pathSplitDestination + errorFileName}", errorRecord, headers);
					errorRecord.Clear();
				}

				DataIconvServices.MoveFile(dir, pathSplitDone + filename);
			}

			Console.WriteLine("Split was complete, enter to close");
			Console.ReadKey();
		}

		private static void WriteCSV(string pathFile, List<string[]> records, List<string> headers)
		{
			using (StreamWriter writer = new StreamWriter(pathFile))
			{
				foreach (string[] line in records)
				{
					writer.WriteLine(string.Join("|", line));
				}
			}

			Console.WriteLine("The Split Error file has been created");
		}


		//public static void CreateExcelPackage(List<Bnd370_Success> records, List<string> headers, string outputPath)
		//{
		//	// Create a new Excel workbook
		//	IWorkbook workbook = new XSSFWorkbook();
		//	ISheet worksheet = workbook.CreateSheet("Bnd370Success");

		//	// Add headers
		//	IRow headerRow = worksheet.CreateRow(0);
		//	for (int i = 0; i < headers.Count; i++)
		//	{
		//		headerRow.CreateCell(i).SetCellValue(headers[i]);
		//	}

		//	// Cache property accessors
		//	var propertyAccessors = new Func<Bnd370_Success, object>[headers.Count];
		//	for (int i = 0; i < headers.Count; i++)
		//	{
		//		var property = typeof(Bnd370_Success).GetProperty(headers[i]);
		//		propertyAccessors[i] = (record) => property.GetValue(record);
		//	}

		//	// Add data
		//	//for (int i = 0; i < records.Count; i++)
		//	//{
		//	//	Bnd370_Success record = records[i];
		//	//	IRow dataRow = worksheet.CreateRow(i + 1);
		//	//	for (int j = 0; j < headers.Count; j++)
		//	//	{
		//	//		var value = propertyAccessors[j](record);
		//	//		dataRow.CreateCell(j).SetCellValue(value != null ? value.ToString() : "");
		//	//	}
		//	//}

		//	for (int i = 0; i < records.Count; i++)
		//	{
		//		Bnd370_Success record = records[i];
		//		IRow dataRow = worksheet.CreateRow(i + 1);
		//		for (int j = 0; j < headers.Count; j++)
		//		{
		//			var value = propertyAccessors[j](record);
		//			ICell cell = dataRow.CreateCell(j);

		//			// Determine data type and set cell value accordingly
		//			if (value != null)
		//			{
		//				switch (Type.GetTypeCode(value.GetType()))
		//				{
		//					case TypeCode.String:
		//						cell.SetCellValue((string)value);
		//						break;
		//					case TypeCode.Int32:
		//						cell.SetCellValue((int)value);
		//						break;
		//					case TypeCode.Double:
		//						cell.SetCellValue((double)value);
		//						break;
		//					case TypeCode.Int64:
		//						cell.SetCellValue((long)value);
		//						break;
		//					// Add more cases for other data types as needed
		//					default:
		//						// Fallback to string conversion for unknown types
		//						cell.SetCellValue(value.ToString());
		//						break;
		//				}
		//			}
		//			else
		//			{
		//				// Set cell value to empty string if value is null
		//				cell.SetCellValue("");
		//			}
		//		}
		//	}


		//	// Save the Excel workbook
		//	using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
		//	{
		//		workbook.Write(fileStream);
		//	}

		//	//Console.WriteLine($"Success Create file in {outputPath}");
		//}

		public static void CreateExcelPackage<T>(List<T> records, List<string> headers, string outputPath)
		{
			// Create a new Excel workbook
			IWorkbook workbook = new XSSFWorkbook();
			ISheet worksheet = workbook.CreateSheet("Sheet1");

			// Add headers
			IRow headerRow = worksheet.CreateRow(0);
			for (int i = 0; i < headers.Count; i++)
			{
				headerRow.CreateCell(i).SetCellValue(headers[i]);
			}

			// Cache property accessors
			var propertyAccessors = new Func<T, object>[headers.Count];
			for (int i = 0; i < headers.Count; i++)
			{
				var property = typeof(T).GetProperty(headers[i]);
				propertyAccessors[i] = (record) => property.GetValue(record);
			}

			// Add data
			for (int i = 0; i < records.Count; i++)
			{
				T record = records[i];
				IRow dataRow = worksheet.CreateRow(i + 1);
				for (int j = 0; j < headers.Count; j++)
				{
					var value = propertyAccessors[j](record);
					ICell cell = dataRow.CreateCell(j);

					// Determine data type and set cell value accordingly
					if (value != null)
					{
						switch (Type.GetTypeCode(value.GetType()))
						{
							case TypeCode.String:
								cell.SetCellValue((string)value);
								break;
							case TypeCode.Int32:
								cell.SetCellValue((int)value);
								break;
							case TypeCode.Double:
								cell.SetCellValue((double)value);
								break;
							case TypeCode.Int64:
								cell.SetCellValue((long)value);
								break;
							// Add more cases for other data types as needed
							default:
								// Fallback to string conversion for unknown types
								cell.SetCellValue(value.ToString());
								break;
						}
					}
					else
					{
						// Set cell value to empty string if value is null
						cell.SetCellValue((string)value);
					}
				}
			}

			// Write workbook to file
			using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
			{
				workbook.Write(fileStream);
			}
		}

	}
}
