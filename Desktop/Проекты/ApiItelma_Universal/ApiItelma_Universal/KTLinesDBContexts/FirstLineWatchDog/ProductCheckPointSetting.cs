using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.KTLinesDBContexts.FirstLineWatchDog;

public partial class ProductCheckPointSetting
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public string? ProductCheckPointSettings { get; set; }
}
