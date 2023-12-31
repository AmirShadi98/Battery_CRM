﻿namespace Battery_CRM.Core.Domain.Models.ViewModels.Customer;

public class CustomersViewModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Family { get; set; }

    public long PhoneNumber { get; set; }

    public int BranchId { get; set; }

    public string BranchName { get; set; }
}
