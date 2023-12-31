namespace Battery_CRM.Core.Domain.Models.ViewModels.Factor;

public class FactorsViewModel
{
    public int Id { get; set; }

    public string ProductName { get; set; }

    public decimal Price { get; set; }

    /// <summary>
    /// if this parameter send null that mean user made pre_factor
    /// </summary>
    public DateTime? SaleDate { get; set; }

    public int FactorCode { get; set; }

    /// <summary>
    /// Factor Create For Who
    /// </summary>
    public int CustomerId { get; set; }

    public string CustomerName { get; set; }

    public int UserId { get; set; }

    public string UserName { get; set; }

    public int BranchId { get; set; }

    public string BranchName { get; set; }


}
