using Battery_CRM.Core.Domain.Models.Base;

namespace Battery_CRM.Core.Domain.Models.Battery_CRM;

public class UserBranch : Entity
{
    public int UserId { get; set; }
    public virtual User User { get; set; }

    public int BranchId { get; set; }
    public virtual Branch Branch { get; set; }
}
