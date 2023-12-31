namespace Battery_CRM.Core.Domain.Models.ViewModels.Factor;

public class CreateFactorViewModel
{
    public string ProductName { get; set; }

    public decimal Price { get; set; }

    /// <summary>
    /// if this parameter send null that mean user made pre_factor
    /// </summary>
    public DateTime? SaleDate { get; set; }

    /// <summary>
    /// Factor Create For Who
    /// </summary>
    public int CustomerId { get; set; }
}
