using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.LabelSetting;

public partial class PackageSetting
{
    public int Id { get; set; }

    public string? VersionSettings { get; set; }

    public byte[]? ImageLabel { get; set; }

    public string? Number1C { get; set; }

    public string? ProductName { get; set; }

    public int? CountPack { get; set; }

    public string? LabelName { get; set; }

    public string? PackSheet { get; set; }

    public string? Number { get; set; }

    public string? Param1 { get; set; }

    public string? Param2 { get; set; }

    public string? Param3 { get; set; }

    public string? Param4 { get; set; }

    public string? Param5 { get; set; }

    public string? Param6 { get; set; }

    public string? Param7 { get; set; }

    public string? Param8 { get; set; }

    public string? Param9 { get; set; }

    public string? Param10 { get; set; }

    public string? Param11 { get; set; }

    public string? Param12 { get; set; }

    public string? Param13 { get; set; }

    public string? Param14 { get; set; }

    public string? LabelProductId { get; set; }

    public virtual Label? LabelProduct { get; set; }
}
