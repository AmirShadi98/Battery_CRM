using Battery_CRM.Core.Domain.Models.Battery_CRM;
using Battery_CRM.Core.Domain.Models.Config;
using Microsoft.EntityFrameworkCore;

namespace Battery_CRM.Infrastructure.Data.Sql.Common;

public class ApplicationContext : DbContext
{
    #region Constractor
    public ApplicationContext(DbContextOptions options) : base(options)
    {

    }

    #endregion

    #region DbSets

    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<Factor> Factors { get; set; }

    public DbSet<Branch> Branches { get; set; }

    public DbSet<UserBranch> UserBranchs { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Message> Message { get; set; }

    #endregion

    #region Config

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        ConfigurationProvider.ApplyConfigurations(modelBuilder);

        modelBuilder.ApplyConfiguration(new BranchConfiguration());
        modelBuilder.ApplyConfiguration(new UserBranchConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    #endregion

}
