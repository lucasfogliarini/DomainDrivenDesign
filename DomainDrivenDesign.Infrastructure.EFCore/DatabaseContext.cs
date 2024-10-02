using DomainDrivenDesign.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DomainDrivenDesign.Infrastructure.EFCore
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
	{
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(thisAssembly);
        }
    }
}
