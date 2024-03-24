using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopyApps.DiscAmountSettlement.DataIconvProcessing.Models
{
	internal class Bnd370_Success
	{
		public int MerchantId { get; set; }
		public int BranchCode { get; set; }
		public string MerchantName { get; set; }
		public string MerchantCompany { get; set; }
		public string MerchantAddress1 { get; set; }
		public string MerchantAddress2 { get; set; }
		public string MerchantAddress3 { get; set; }
		public string AccountBankName { get; set; }
		public string AccountNumber { get; set; }
		public string AccountBeneficiary { get; set; }
		public int OrigBatch { get; set; }
		public int BatchNum { get; set; }
		public int SeqNum { get; set; }
		public string CardType { get; set; }
		//public DateTime TrxDate { get; set; }
		public string AuthCode { get; set; }
		public string InstInd { get; set; }
		public string TrxType { get; set; }
		public string CardNum { get; set; }
		public int TrxAmount { get; set; }
		public decimal MdrRate { get; set; }
		public int DiscAmount { get; set; }
		public int NetAmount { get; set; }
		//public DateTime FileDate { get; set; }
		public string TerminalId { get; set; }
		public int AirfareTax { get; set; }
		public int NonAirfareTax { get; set; }
		public string Remarks { get; set; }
		public long LastFileID { get; set; }
		public string MdrType { get; set; }
		public string CardPrincipal { get; set; }
		public string RekDr { get; set; }
		public string KetRekDR { get; set; }
		public string RekCr { get; set; }
		public string KetRekCR { get; set; }
		public string file_cemtex_debit_off_us { get; set; }
	}
}
