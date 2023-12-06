using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.TimeOffline;

public partial class MrmLog
{
    public int id { get; set; }

    public string? LogDateTime { get; set; }

    public string? TimeOfflineHour { get; set; }


}
