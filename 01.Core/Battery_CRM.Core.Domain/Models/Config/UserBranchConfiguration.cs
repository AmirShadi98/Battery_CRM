using Battery_CRM.Core.Domain.Models.Battery_CRM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Battery_CRM.Core.Domain.Models.Config;

public class UserBranchConfiguration : IEntityTypeConfiguration<UserBranch>
{
    public void Configure(EntityTypeBuilder<UserBranch> builder)
    {
        builder.HasData(new UserBranch
        {
            Id = 1,
            BranchId = 1,
            UserId = 1
        });
    }
}
