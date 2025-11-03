using BasicApi.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace BasicApi.BackEnd.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Country> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>().HasIndex(c => c.Id).IsUnique();
        modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique();
    }
}