using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.Permissions;

public partial class PermissionsList
{
    public int PermissionId { get; set; }

    public string PermissionName { get; set; } = null!;

    public string PermissionLevel { get; set; } = null!;

    public virtual ICollection<PermissionRule> PermissionRules { get; set; } = new List<PermissionRule>();
}
