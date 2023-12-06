using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.CheckPoints;

public partial class Plclist
{
    public int Id { get; set; }

    public string? Plcname { get; set; }

    public string? Plcipaddress { get; set; }

    public string? CheckPointName { get; set; }
}
