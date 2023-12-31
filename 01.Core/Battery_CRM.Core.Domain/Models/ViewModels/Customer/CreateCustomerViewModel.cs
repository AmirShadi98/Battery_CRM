namespace Battery_CRM.Core.Domain.Models.ViewModels.Customer;

public class CreateCustomerViewModel
{
    public string Name { get; set; }

    public string Family { get; set; }

    public long PhoneNumber { get; set; }

    public int BranchId { get; set; }
}
