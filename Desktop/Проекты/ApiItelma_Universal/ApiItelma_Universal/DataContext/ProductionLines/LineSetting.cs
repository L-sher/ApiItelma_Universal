using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.ProductionLines;

public partial class LineSetting
{
    public int LineId { get; set; }

    public string? LineName { get; set; }

    public int? LineProductId { get; set; }

    public string? LineCheckPointsJson { get; set; }

    public bool LineZeroCheckPoint { get; set; }

    public string? LineProcess { get; set; }
}
