using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.Products;

public partial class ProductPassport
{
    public int PassportId { get; set; }

    public string? PassportName { get; set; }

    public byte[] PassportImage { get; set; } = null!;

    public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
}
