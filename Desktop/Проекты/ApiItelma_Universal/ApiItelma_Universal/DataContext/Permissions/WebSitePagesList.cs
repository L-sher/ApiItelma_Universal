using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.Permissions;

public partial class WebSitePagesList
{
    public int WebSitePageId { get; set; }

    public string? WebSitePageName { get; set; }

    public virtual ICollection<PermissionRule> PermissionRules { get; set; } = new List<PermissionRule>();
}
