using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.SettingSenderMan;

public partial class ProductId
{
    public int Id { get; set; }

    public int ProdId { get; set; }

    public string? Name { get; set; }

    public string? Potrebitel { get; set; }

    public string? SettingSenderManMailProgrammSend { get; set; }

    public virtual SettingSenderManMail? SettingSenderManMailProgrammSendNavigation { get; set; }
}
