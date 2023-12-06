using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.Preview.SettingNewMes;

public partial class CheckStepsUfk1
{
    public int Id { get; set; }

    public byte[]? Step { get; set; }

    public string MinVal { get; set; } = null!;

    public string MaxVal { get; set; } = null!;

    public string? SettingIdProduct { get; set; }

    public virtual Setting? SettingIdProductNavigation { get; set; }
}
