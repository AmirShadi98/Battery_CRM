using Battery_CRM.Core.Domain.Models.Base;
using static Battery_CRM.Core.Domain.Models.Enuums.Prototypes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Battery_CRM.Core.Domain.Models.Battery_CRM;

public class Role : Entity
{
    public string Title { get; set; }

    public virtual ICollection<User> Users { get; set; }

}
