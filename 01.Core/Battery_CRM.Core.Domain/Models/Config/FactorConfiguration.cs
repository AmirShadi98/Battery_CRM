using Battery_CRM.Core.Domain.Models.Battery_CRM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Battery_CRM.Core.Domain.Models.Config;

public class FactorConfiguration : IEntityTypeConfiguration<Factor>
{
    public void Configure(EntityTypeBuilder<Factor> builder)
    {
        builder
            .HasOne(e => e.User)
            .WithMany(e => e.Factors)
            .HasForeignKey(u => u.UserId);

        builder
            .HasOne(e => e.Customer)
            .WithMany(e => e.Factors)
            .HasForeignKey(u => u.CustomerId);


        builder.Property(e => e.ProductName).HasMaxLength(400);

    }
}
