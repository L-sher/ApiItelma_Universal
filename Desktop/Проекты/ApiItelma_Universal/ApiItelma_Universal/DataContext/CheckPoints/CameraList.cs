using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.CheckPoints;

public partial class CameraList
{
    public int Id { get; set; }

    public string? CameraName { get; set; }

    public string? CameraIpaddress { get; set; }

    public string? CheckPointName { get; set; }
}
