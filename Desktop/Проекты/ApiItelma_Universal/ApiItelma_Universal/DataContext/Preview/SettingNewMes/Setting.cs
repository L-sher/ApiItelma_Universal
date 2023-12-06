using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.Preview.SettingNewMes;

public partial class Setting
{
    public string IdProduct { get; set; } = null!;

    public string? Number1C { get; set; }

    public string? Name { get; set; }

    public string? TechRomStruct { get; set; }

    public bool Vch { get; set; }

    public byte KodUfk1 { get; set; }

    public byte KodUfk3Nit { get; set; }

    public byte KodUfk3Mts { get; set; }

    public string? LabelStruct { get; set; }

    public string? FileAdress { get; set; }

    public string? Param1 { get; set; }

    public string? Param2 { get; set; }

    public string? Param3 { get; set; }

    public string? Param4 { get; set; }

    public string? Param5 { get; set; }

    public string? Version { get; set; }

    public byte[]? LabelImage { get; set; }

    public bool? IsEasy { get; set; }

    public virtual ICollection<CheckStepsUfk1> CheckStepsUfk1s { get; set; } = new List<CheckStepsUfk1>();

    public virtual ICollection<CheckStepsUfk3> CheckStepsUfk3s { get; set; } = new List<CheckStepsUfk3>();
}
