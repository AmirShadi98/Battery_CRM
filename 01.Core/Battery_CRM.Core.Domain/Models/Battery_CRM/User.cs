using Battery_CRM.Core.Domain.Models.Base;
using static Battery_CRM.Core.Domain.Models.Enuums.Prototypes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Battery_CRM.Core.Domain.Models.Battery_CRM;

public class User : FullEntity
{
    public string Name { get; set; }

    public string Family { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public long PhoneNumber { get; set; }

    public int RoleId { get; set; }
    public virtual Role Role { get; set; }

    public virtual ICollection<UserBranch> UserBranches { get; set; }
    public virtual ICollection<Message> Messages { get; set; }
    public virtual ICollection<Factor> Factors { get; set; }


    [NotMapped]
    public UserRoleEnum UserRoleEnum
    {
        get
        {
            return (UserRoleEnum)RoleId;
        }
    }
}
