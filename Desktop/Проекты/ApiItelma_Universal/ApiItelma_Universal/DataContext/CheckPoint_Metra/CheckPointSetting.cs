using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.CheckPoint_Metra;

public partial class CheckPointSetting
{
    public int ProductId { get; set; }

    public int CheckPointId { get; set; }

    public string CheckPointName { get; set; } = null!;

    public string CheckPointIp { get; set; } = null!;

    public string? TechRomStruct { get; set; }

    public bool Vch { get; set; }

    public byte KodUfk1 { get; set; }

    public byte KodUfk3Nit { get; set; }

    public byte KodUfk3Mts { get; set; }

    public virtual ICollection<CheckPointSettingsUfk1> CheckPointSettingsUfk1s { get; set; } = new List<CheckPointSettingsUfk1>();

    public virtual ICollection<CheckPointSettingsUfk3> CheckPointSettingsUfk3s { get; set; } = new List<CheckPointSettingsUfk3>();
}
