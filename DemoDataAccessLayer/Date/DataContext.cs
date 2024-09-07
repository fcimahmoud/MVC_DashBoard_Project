using DemoDataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DemoDataAccessLayer.Date
{
    internal class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("connection string");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        public DbSet<Department> Departments { get; set; }
    }
}
