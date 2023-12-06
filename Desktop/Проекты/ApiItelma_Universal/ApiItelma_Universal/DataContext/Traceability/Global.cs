using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DataContext.Traceability;

public partial class Global
{
    public string BarCode { get; set; } = null!;

    public int? ProductId { get; set; }

    public DateTime? CheckTime { get; set; }

    public int LastCheckPointId { get; set; }

    public bool Result { get; set; }

    public string? LastCheckPointErrors { get; set; }
}
