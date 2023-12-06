using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.Products;

public partial class ProductLabel
{
    public int LabelId { get; set; }

    public string? LabelName { get; set; }

    public byte[]? LabelImage { get; set; }

    public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
}
