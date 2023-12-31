namespace Battery_CRM.Core.Domain.Models.ViewModels.User;

public class UpdateUserViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Family { get; set; }

    public long PhoneNumber { get; set; }

    public int RoleId { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public int BranchId { get; set; }
}
