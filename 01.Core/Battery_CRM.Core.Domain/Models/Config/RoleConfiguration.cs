using Battery_CRM.Core.Domain.Models.Battery_CRM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Battery_CRM.Core.Domain.Models.Config;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {

        builder.Property(e=>e.Title).HasMaxLength(400);

        builder.HasData(new Role
        {
            Id= 1,
            Title="مدیر"
        });

        builder.HasData(new Role
        {
            Id= 2,
            Title="کاربر عادی"
        });
    }
}
