using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.TimeOffline;

public partial class AllDevice
{
    public int Id { get; set; }

    public string? DeviceName { get; set; }

    public string? IpAddress { get; set; }

    public string? OfflineStatusDate { get; set; }
}
