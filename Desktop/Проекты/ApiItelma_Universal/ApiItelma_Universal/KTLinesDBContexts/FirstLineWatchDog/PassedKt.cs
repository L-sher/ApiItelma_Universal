using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.KTLinesDBContexts.FirstLineWatchDog;

public partial class PassedKt
{
    public int Id { get; set; }

    public string Barcode { get; set; } = null!;

    public int? PassedKt1 { get; set; }

    public string? NextKt { get; set; }

    public virtual CheckPointsList? PassedKt1Navigation { get; set; }
}
