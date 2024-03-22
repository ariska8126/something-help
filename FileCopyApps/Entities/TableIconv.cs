using System;
using System.Collections.Generic;

namespace FileCopyApps.Entities;

public partial class TableIconv
{
    public string Id { get; set; } = null!;

    public string CardNumber { get; set; } = null!;

    public long DiscAmount { get; set; }

    public string MdrType { get; set; } = null!;

    public string Principal { get; set; } = null!;
}
