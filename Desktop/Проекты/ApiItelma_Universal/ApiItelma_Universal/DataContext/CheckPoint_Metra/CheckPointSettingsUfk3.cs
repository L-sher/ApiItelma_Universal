using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.CheckPoint_Metra;

public partial class CheckPointSettingsUfk3
{
    public int Id { get; set; }

    public byte[]? Step { get; set; }

    public int MinVal { get; set; }

    public int MaxVal { get; set; }

    public int ProductId { get; set; }

    public virtual CheckPointSetting Product { get; set; } = null!;
}
