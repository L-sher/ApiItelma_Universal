using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.CheckPoint_Metra;

public partial class CheckPointDataPrinter
{
    public int Id { get; set; }

    public DateTime Time { get; set; }

    public string? QrData { get; set; }

    public string? LocalBarCode { get; set; }

    public string? Name { get; set; }

    public string? File { get; set; }

    public string? Param1 { get; set; }

    public string? Param2 { get; set; }

    public string? Param3 { get; set; }

    public string? Param4 { get; set; }

    public string? Param5 { get; set; }

    public string? Version { get; set; }

    public string? Serial { get; set; }

    public string? DayOfYear { get; set; }

    public string? Year { get; set; }

    public bool Result { get; set; }

    public virtual CheckPointData? LocalBarCodeNavigation { get; set; }
}
