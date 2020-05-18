using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Data
{
    public class DataContext:DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.Load("Infrastructure.Data").GetTypes().
               Where(type => !string.IsNullOrEmpty(type.Namespace)).
               Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("config.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("AreabitesConnection");
                optionsBuilder.UseSqlServer(connectionString);
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }
    }
}
