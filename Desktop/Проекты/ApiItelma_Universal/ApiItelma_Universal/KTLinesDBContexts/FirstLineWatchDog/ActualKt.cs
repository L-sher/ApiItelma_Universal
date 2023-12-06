namespace ApiItelma_Universal.KTLinesDBContexts.FirstLineWatchDog;

public partial class ActualKt
{

    public int Id { get; set; }

    public string Barcode { get; set; } = null!;

    public int? ActualKt1 { get; set; }

    public int? NextKt { get; set; }

    public virtual CheckPointsList? ActualKt1Navigation { get; set; }

}
