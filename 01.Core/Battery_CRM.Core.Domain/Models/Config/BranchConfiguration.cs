using Battery_CRM.Core.Domain.Models.Battery_CRM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Battery_CRM.Core.Domain.Models.Config;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder
            .HasMany(e => e.UserBranches)
            .WithOne(e => e.Branch)
            .HasForeignKey(u => u.BranchId);

        builder
            .HasMany(e => e.Customers)
            .WithOne(e => e.Branch)
            .HasForeignKey(u => u.BranchId);

        builder.Property(e => e.Name).HasMaxLength(400);

        builder.Property(e => e.Address).HasMaxLength(700);

        builder.HasData(new Branch
        {
            Id = 1,
            CreateDate = DateTime.UtcNow,
            IsDelete = false,
            Address="تبریز",
            Name="امامیه",
            PhoneNumber = 9146400136
        });
    }
}
