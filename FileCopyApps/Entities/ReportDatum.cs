using System;
using System.Collections.Generic;

namespace FileCopyApps.Entities;

public partial class ReportDatum
{
    public long Id { get; set; }

    public string CardNumber { get; set; } = null!;

    public string? LastTran { get; set; }

    public string? TranDate { get; set; }

    public string? Code { get; set; }

    public long? Amt { get; set; }

    public string? Pb { get; set; }

    public string? Pbasli { get; set; }

    public string? Nwk { get; set; }

    public string? Ket { get; set; }

    public string? KetAsli { get; set; }

    public string? Src { get; set; }

    public string? Daerah { get; set; }

    public string? DaerahAsli { get; set; }

    public string? Kota { get; set; }

    public string? RekeningDebet { get; set; }

    public string? RekeningKredit { get; set; }

    public string? AccountNameDebet { get; set; }

    public string? AccountNameKredit { get; set; }

    public string? Remarks { get; set; }

    public long FileId { get; set; }
}
