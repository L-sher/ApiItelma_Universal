using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.LabelSetting;

public partial class Label
{
    public string ProductId { get; set; } = null!;

    public string? ProductName { get; set; }

    //public virtual ICollection<PackageSetting> PackageSettings { get; set; } = new List<PackageSetting>();

    //public virtual ICollection<PassportSetting> PassportSettings { get; set; } = new List<PassportSetting>();

    //public virtual ICollection<ProductSetting> ProductSettings { get; set; } = new List<ProductSetting>();
}
