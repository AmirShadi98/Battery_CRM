using Battery_CRM.Core.Domain.Models.Base;

namespace Battery_CRM.Core.Domain.Models.Battery_CRM;

public class Message:Entity
{
    public int SenderId { get; set; }
    public virtual User User { get; set; }

    public int ReciverId { get; set; }
    public virtual Customer  Customer { get; set; }

    public int BranchId { get; set; }
    public virtual Branch Branch { get; set; }

    public string Text { get; set; }

    public DateTime SendDate { get; set; }

    public bool IsSend { get; set; }
}