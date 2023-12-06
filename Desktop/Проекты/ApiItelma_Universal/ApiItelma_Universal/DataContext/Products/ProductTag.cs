using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.Products;

public partial class ProductTag
{
    public int TagId { get; set; }

    public string? TagName { get; set; }

    public int? TagParametersId { get; set; }

    public int? TagLabelId { get; set; }

    public int? TagPackingListId { get; set; }

    public int? TagPassportId { get; set; }

    public virtual ICollection<ProductSetting> ProductSettings { get; set; } = new List<ProductSetting>();

    public virtual ProductLabel? TagLabel { get; set; }

    public virtual ProductPackingList? TagPackingList { get; set; }

    public virtual ProductTagParameter? TagParameters { get; set; }

    public virtual ProductPassport? TagPassport { get; set; }
}
