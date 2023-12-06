using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.ProductionLines;

public partial class ProcessesAndSetting
{
    public int Id { get; set; }

    public string ProcessName { get; set; } = null!;

    public string? IsThereKtzero { get; set; }

    public string? ListKtinJson { get; set; }

    public string? ProductId { get; set; }

    public string? ProductRoute { get; set; }
}
