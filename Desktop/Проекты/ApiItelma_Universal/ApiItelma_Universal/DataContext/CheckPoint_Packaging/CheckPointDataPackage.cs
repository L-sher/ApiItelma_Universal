using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.CheckPoint_Packaging;

public partial class CheckPointDataPackage
{
    public string BarCode { get; set; } = null!;

    public string? PackageSerial { get; set; }

    public virtual ICollection<CheckPointDatum> CheckPointData { get; set; } = new List<CheckPointDatum>();

    public virtual CheckPointDataSerial? PackageSerialNavigation { get; set; }
}
