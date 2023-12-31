using Battery_CRM.Core.Domain.Models.Base;

namespace Battery_CRM.Core.Domain.Models.Battery_CRM;

public class Customer : FullEntity
{
    public string Name { get; set; }

    public string Family { get; set; }

    public long PhoneNumber { get; set; }

    public int BranchId { get; set; }
    public virtual Branch Branch { get; set; }

    public virtual ICollection<Message> Messages { get; set; }
    public virtual ICollection<Factor> Factors { get; set; }
}
