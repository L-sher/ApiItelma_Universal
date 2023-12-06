using System;
using System.Collections.Generic;

namespace ApiItelma_Universal.DBContext.Permissions;

public partial class EmployeesList
{
    public int EmployeeId { get; set; }

    public string? EmployeeName { get; set; }

    public string? EmployeeLogin { get; set; }

    public string? EmployeeCard { get; set; }

    public virtual ICollection<PermissionRule> PermissionRules { get; set; } = new List<PermissionRule>();
}
