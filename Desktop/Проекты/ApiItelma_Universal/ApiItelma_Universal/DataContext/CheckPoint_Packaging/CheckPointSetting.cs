using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.CheckPoint_Packaging;

public partial class CheckPoint_Setting
{
    public int ProductId { get; set; }

    public int CheckPointId { get; set; }

    public string CheckPointName { get; set; } = null!;

    public string CheckPointIp { get; set; } = null!;
}
