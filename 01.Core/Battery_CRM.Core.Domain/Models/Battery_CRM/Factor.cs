using Battery_CRM.Core.Domain.Models.Base;

namespace Battery_CRM.Core.Domain.Models.Battery_CRM;

public class Factor : Entity
{
    public string ProductName { get; set; }

    public decimal Price { get; set; }

    public DateTime? SaleDate { get; set; }

    public int FactorCode { get; set; }

    /// <summary>
    /// Factor Create By Who
    /// </summary>
    public int UserId { get; set; }
    public virtual User User { get; set; }

    /// <summary>
    /// Factor Create For Who
    /// </summary>
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
}