using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.SettingSenderMan;

public partial class CopyMail
{
    public int Id { get; set; }

    public string? Mail { get; set; }

    public string? SettingSenderManMailProgrammSend { get; set; }

    public virtual SettingSenderManMail? SettingSenderManMailProgrammSendNavigation { get; set; }
}
