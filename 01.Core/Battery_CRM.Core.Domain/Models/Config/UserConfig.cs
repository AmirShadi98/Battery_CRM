using Battery_CRM.Core.Domain.Models.Battery_CRM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battery_CRM.Core.Domain.Models.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasOne(e => e.Role)
            .WithMany(e => e.Users)
            .HasForeignKey(u => u.RoleId);

        builder
            .HasMany(e => e.UserBranches)
            .WithOne(e => e.User)
            .HasForeignKey(u => u.UserId);

        builder.Property(e=>e.Name).HasMaxLength(400);

        builder.Property(e=>e.Family).HasMaxLength(400);

        builder.Property(e=>e.UserName).HasMaxLength(400);

        builder.Property(e=>e.Password).HasMaxLength(400);

        builder.HasData(new User
        {
            CreateDate = DateTime.Now,
            Password = "gdyb21LQTcIANtvYMT7QVQ==",//1234
            UserName = "amir",
            Name = "امیر",
            Family="شادی",
            IsDelete = false,
            Id=1,
            RoleId=1            
        });
    }
}
