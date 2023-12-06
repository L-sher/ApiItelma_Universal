using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.Products;

public partial class ProductPackingList
{
    public int PackingListId { get; set; }

    public string PackingListName { get; set; } = null!;

    public byte[] PackingListImage { get; set; } = null!;

    public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
}
