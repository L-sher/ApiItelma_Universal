using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.CheckPoint_Packaging;

public partial class CheckPointDataSerial
{
    public string Serial { get; set; } = null!;

    public int? ProductId { get; set; }

    public DateTime? Date { get; set; }

    public int Id { get; set; }

    public virtual ICollection<CheckPointDataPackage> CheckPointDataPackages { get; set; } = new List<CheckPointDataPackage>();
}
