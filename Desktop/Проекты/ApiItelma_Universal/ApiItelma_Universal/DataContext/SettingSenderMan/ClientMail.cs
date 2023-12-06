using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.SettingSenderMan;

public partial class ClientMail
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Mail { get; set; }

    public string? SettingSenderManMailProgrammSend { get; set; }

    public virtual SettingSenderManMail? SettingSenderManMailProgrammSendNavigation { get; set; }
}
