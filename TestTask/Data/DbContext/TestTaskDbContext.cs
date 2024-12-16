using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TestTask.Data.Models;

namespace TestTask.Data.DbContext
{
    public class TestTaskDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TestTaskDbContext(DbContextOptions<TestTaskDbContext> options) : base(options) { }
        
        public DbSet<ProductFacility> Products => Set<ProductFacility>();
        public DbSet<EquipmentType> EquipmentTypes => Set<EquipmentType>();
        public DbSet<Contract> Contracts => Set<Contract>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
