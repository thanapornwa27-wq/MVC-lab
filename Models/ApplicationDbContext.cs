using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _7_Elephant.Models;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Phone> Phones { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Phone>()
            .HasKey(e => e.ProductId);

        modelBuilder.Entity<Phone>()
            .ToTable("Phone");

        modelBuilder.Entity<Order>()
            .HasKey(e => e.OrderId);

        modelBuilder.Entity<Order>()
            .ToTable("Order");

        modelBuilder.Entity<Phone>()
            .Property(e => e.Price)
            .HasColumnType("money");

        modelBuilder.Entity<Phone>()
            .Property(e => e.Cost)
            .HasColumnType("money");

        modelBuilder.Entity<Order>()
            .Property(e => e.Total)
            .HasColumnType("money");
    }
}
