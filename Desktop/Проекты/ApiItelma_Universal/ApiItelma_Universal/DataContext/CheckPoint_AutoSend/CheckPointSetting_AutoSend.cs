using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.CheckPoint_AutoSend;

public partial class CheckPointSetting_AutoSend
{
    public int ProgramId { get; set; }

    public int NumberSend { get; set; }

    public DateTime PreviousSend { get; set; }

    public string? SubjectMail { get; set; }

    public string? BodyMail { get; set; }

    public string? StructMail { get; set; }

    public string? ProductsJson { get; set; }

    public string? ClientMailsJson { get; set; }

    public string? CopyMailsJson { get; set; }
}
