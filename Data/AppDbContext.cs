using Microsoft.EntityFrameworkCore;
using EmployeeVacationDB.Models;

namespace EmployeeVacationDB.Data
{    
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=app.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasDiscriminator<string>("EmployeeType")
                .HasValue<HourlyEmployee>("Hourly")
                .HasValue<SalariedEmployee>("Salaried")
                .HasValue<Manager>("Manager");
        }
    }
 }
