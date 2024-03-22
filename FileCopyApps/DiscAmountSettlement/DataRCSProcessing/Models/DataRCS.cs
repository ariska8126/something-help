using FileCopyApps.DiscAmountSettlement.Models;
using FileCopyApps.DiscAmountSettlement.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopyApps.DiscAmountSettlement.DataRCSProcessing.Models
{
    internal class DataRCS
    {
        public string BATCH { get; set; }
        public string IDK { get; set; }
        public string SEQ { get; set; }
        public string TYPE { get; set; }
        public string TXN_DATE { get; set; }
        public string AUTH { get; set; }
        public string CARD_NUMBER { get; set; }
        public string EH { get; set; }
        public int AMOUNT { get; set; }
        public string OP { get; set; }
        public string RATE { get; set; }
        public string OH { get; set; }
        public int DISC_AMT { get; set; }
        public string No_Merchant { get; set; }
        public string Nama_Cabang { get; set; }
        public string Tanggal_ { get; set; }
        public string rek { get; set; }
        public string No { get; set; }
        public string bin { get; set; }
        public string deskripsi { get; set; }
        public string deskripsi1 { get; set; }
        public string New1 { get; set; }
        public string SourceFile { get; set; }
    }
}
