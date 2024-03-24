using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopyApps.DiscAmountSettlement.DataIconvProcessing.Models
{
	public class Bnd370_File
	{
		public string filename { get; set; }
		public string status { get; set; }
		public long trx_success { get; set; }
		public long trx_failed { get; set; }
		public string remarks { get; set; }
		public DateTime process_dt { get; set; }
		public string cemtex_file_onus { get; set; }
		public string cemtex_file_offus { get; set; }
		public long trx_onus { get; set; }
		public long trx_offus { get; set; }
		public string cemtex_file_onus_Debit { get; set; }
		public long trx_onus_Debit { get; set; }
		public string cemtex_file_offus_Debit { get; set; }
		public long trx_offus_Debit { get; set; }
		public long LastId { get; set; }

	}
}
