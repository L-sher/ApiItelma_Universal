using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.LabelSetting;

public partial class ProductSetting
{
    public int Id { get; set; }

    public byte[]? ImageLabel { get; set; }

    public string? VersionSettings { get; set; }

    public string? LabelStruct { get; set; }

    public string? Param1 { get; set; }

    public string? Param2 { get; set; }

    public string? Param3 { get; set; }

    public string? Param4 { get; set; }

    public string? Param5 { get; set; }

    public string? Version { get; set; }

    public string? DayOfYear { get; set; }

    public string? Year { get; set; }

    public string? LabelProductId { get; set; }

    public virtual Label? LabelProduct { get; set; }
}
