using DemoDataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DemoDataAccessLayer.Date
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connection string");
        //}
        //public DataContext() : base()
        //{
        //}
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //    => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        public DbSet<Department> Departments { get; set; }
    }
}
