using System;
using System.Collections.Generic;

namespace FileCopyApps.Entities;

public partial class Lookup
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Value { get; set; }

    public int? OrderNo { get; set; }

    public DateTime? CreatedTime { get; set; }

    public DateTime? UpdatedTime { get; set; }

    public int? CreatedById { get; set; }

    public int? UpdatedById { get; set; }
}
