using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Battery_CRM.Core.Domain.Models.Config;

public static class ConfigurationProvider
{
    public static void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        var configurationTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && typeof(IEntityTypeConfiguration<>).IsAssignableFrom(type));

        foreach (var configurationType in configurationTypes)
        {
            dynamic configuration = Activator.CreateInstance(configurationType)!;
            modelBuilder.ApplyConfiguration(configuration);
        }
    }
}