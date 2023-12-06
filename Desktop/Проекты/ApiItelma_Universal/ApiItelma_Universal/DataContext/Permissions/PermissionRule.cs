using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.Permissions;

public partial class PermissionRule
{
    public int RuleId { get; set; }

    public string? RuleName { get; set; }

    public int EmployeeId { get; set; }

    public int PermissionId { get; set; }

    public int WebSitePageId { get; set; }

    public int? CheckPointId { get; set; }

    public virtual EmployeesList Employee { get; set; } = null!;

    public virtual PermissionsList Permission { get; set; } = null!;

    public virtual WebSitePagesList WebSitePage { get; set; } = null!;
}
