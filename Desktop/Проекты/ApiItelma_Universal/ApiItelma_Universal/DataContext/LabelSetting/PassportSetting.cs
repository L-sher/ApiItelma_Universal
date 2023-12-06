using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.LabelSetting;

public partial class PassportSetting
{
    public int Id { get; set; }

    public string? LabelName { get; set; }

    public byte[]? LabelImage { get; set; }

    public string? ProductName { get; set; }

    public string? LabelProductId { get; set; }

    public virtual Label? LabelProduct { get; set; }
}
