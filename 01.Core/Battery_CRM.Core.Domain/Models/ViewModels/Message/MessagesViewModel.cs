using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battery_CRM.Core.Domain.Models.ViewModels.Message;

public class MessagesViewModel
{
    public int SenderId { get; set; }

    public string SenderName { get; set; }

    public int ReciverId { get; set; }

    public string ReciverName { get; set; }

    public int BranchId { get; set; }

    public string BranchName { get; set; }

    public string Text { get; set; }

    public DateTime SendDate { get; set; }

    public bool IsSend { get; set; }
}
