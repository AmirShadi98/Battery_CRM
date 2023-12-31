using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battery_CRM.Core.Domain.Models.Enuums;

public class Prototypes
{
    [Description("نقش کاربر")]
    public enum UserRoleEnum
    {

        [Description("مدیر سیستم")]
        Admin = 1,

        [Description("اپراتور")]
        Operator = 2
    }
}
