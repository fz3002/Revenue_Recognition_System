using Microsoft.EntityFrameworkCore;
using Revenue_Recognition_System.Controllers;
using Revenue_Recognition_System.Interceptors;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Context;

public partial class RRSystemDbContext : DbContext
{
    public RRSystemDbContext()
    {
    }

    public RRSystemDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<NaturalPerson> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RRSystemDbContext).Assembly);
    }
}