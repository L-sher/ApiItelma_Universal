using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.KTLinesDBContexts.FirstLineWatchDog;

public partial class CheckPointsList
{
    public int CheckPointId { get; set; }

    public string? CheckPointName { get; set; }

    public string? CheckPointIp { get; set; }

    public string? CheckPointSettingsJson { get; set; }

    public string? CheckPointDataJson { get; set; }

    public virtual ICollection<PassedKt> PassedKts { get; set; } = new List<PassedKt>();
}
