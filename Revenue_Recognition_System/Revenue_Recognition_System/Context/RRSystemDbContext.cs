using Microsoft.EntityFrameworkCore;
using Revenue_Recognition_System.Controllers;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RRSystemDbContext).Assembly);
    }
}