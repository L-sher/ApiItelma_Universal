using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.CheckPoint_Metra;

public partial class CheckPointData
{
    public string BarCode { get; set; } = null!;

    public int ProductId { get; set; }

    public DateTime TimeCheck { get; set; }

    public bool ResultTest { get; set; }

    public virtual ICollection<CheckPointDataPrinter> CheckPointDataPrinters { get; set; } = new List<CheckPointDataPrinter>();

    public virtual ICollection<CheckPointDataProgrammer> CheckPointDataProgrammers { get; set; } = new List<CheckPointDataProgrammer>();

    public virtual ICollection<CheckPointDataUfk1> CheckPointDataUfk1s { get; set; } = new List<CheckPointDataUfk1>();

    public virtual ICollection<CheckPointDataUfk3> CheckPointDataUfk3s { get; set; } = new List<CheckPointDataUfk3>();
}
