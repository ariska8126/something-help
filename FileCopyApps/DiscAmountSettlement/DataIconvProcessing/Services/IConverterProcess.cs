using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FileCopyApps.DiscAmountSettlement.DataIconvProcessing.Services
{
	internal class IConverterProcess
	{
		public static void RunProcess()
		{
			Console.WriteLine("Pilih Menu Berikut");
			Console.WriteLine("1. BND370 File - Split Csv => filename (bnd370file_)");
			Console.WriteLine("2. BND370 File - Insert from Excel");
			Console.WriteLine("3. BND370 Success - Split Csv => filename (bnd370success_)");
			Console.WriteLine("4. BND370 Success - Insert from Excel");

			Console.WriteLine();
			Console.Write("Main Menu : ");
			switch (Console.ReadLine())
			{
				case "1":
					IConvSplitService.SplitCsvBnd370FileToExcel();
					break;
				case "2":
					DataIconvServices.SaveBnd370File();
					break;
				case "3":
					IConvSplitService.SplitCsvBnd370SuccessToExcel();
					break;
				case "4":
					DataIconvServices.SaveBnd370Success();
					break;
				default:
					Console.WriteLine("Input was not valid !");
					break;
			}
		}
	}
}
