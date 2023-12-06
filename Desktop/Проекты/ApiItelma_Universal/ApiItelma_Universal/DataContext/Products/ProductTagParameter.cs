using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.Products;

public partial class ProductTagParameter
{
    public int ParametersId { get; set; }

    public string? TagParametersName { get; set; }

    public string? LabelParametersJson { get; set; }

    public string? PackingListParametersJson { get; set; }

    public string? PassportParametersJson { get; set; }

    public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
}
