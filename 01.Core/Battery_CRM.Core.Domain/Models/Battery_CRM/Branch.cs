using Battery_CRM.Core.Domain.Models.Base;

namespace Battery_CRM.Core.Domain.Models.Battery_CRM;

public class Branch : FullEntity
{
    public string Name { get; set; }

    public string Address { get; set; }

    public long PhoneNumber { get; set; }

    public virtual ICollection<UserBranch> UserBranches { get; set; }
    public virtual ICollection<Message> Messages { get; set; }
    public virtual ICollection<Customer> Customers { get; set; }

}
