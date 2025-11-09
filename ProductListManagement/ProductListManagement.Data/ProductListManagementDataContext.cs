using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductListManagement.Data.Contracts;
using ProductListManagement.Model;
using System.Reflection;

namespace ProductListManagement.Data
{
    public class ProductListManagementDataContext : DbContext, IDataContext
    {
        private DbConnectionSettings _settings;
        protected readonly IConfiguration Configuration;
        public DbSet<Product> Products { get; set; }

        public ProductListManagementDataContext(DbConnectionSettings settings)
        {
            _settings = settings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                _settings.MSSQLDatabase,
                options => options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
