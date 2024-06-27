using Microsoft.EntityFrameworkCore;
using Revenue_Recognition_System.Configurations;
using Revenue_Recognition_System.Interceptors;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Context;

public partial class RRSystemDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public RRSystemDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public RRSystemDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<NaturalPerson> People { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<DiscountType> DiscountTypes { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RRSystemDbContext).Assembly);
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration(_configuration));
    }
}