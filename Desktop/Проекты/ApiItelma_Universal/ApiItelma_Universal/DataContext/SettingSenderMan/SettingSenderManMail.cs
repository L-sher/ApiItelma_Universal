using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.SettingSenderMan;

public partial class SettingSenderManMail
{
    public string ProgrammSend { get; set; } = null!;

    public int NumberSend { get; set; }

    public DateTime PreviosSend { get; set; }

    public string? SubjectMail { get; set; }

    public string? BodyMail { get; set; }

    public string? StructMail { get; set; }

    public virtual ICollection<ClientMail> ClientMails { get; set; } = new List<ClientMail>();

    public virtual ICollection<CopyMail> CopyMails { get; set; } = new List<CopyMail>();

    public virtual ICollection<ProductId> ProductIds { get; set; } = new List<ProductId>();
}
