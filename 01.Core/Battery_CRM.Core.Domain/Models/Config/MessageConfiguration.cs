using Battery_CRM.Core.Domain.Models.Battery_CRM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Battery_CRM.Core.Domain.Models.Config;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder
            .HasOne(e => e.User)
            .WithMany(e => e.Messages)
            .HasForeignKey(u => u.SenderId);

        builder
            .HasOne(e => e.Customer)
            .WithMany(e => e.Messages)
            .HasForeignKey(u => u.ReciverId);

        builder
            .HasOne(e => e.Branch)
            .WithMany(e => e.Messages)
            .HasForeignKey(u => u.BranchId);

        builder.Property(e => e.Text).HasMaxLength(400);

    }
}
