using System;
using System.Collections.Generic;
using Flower_Management.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flower_Management.Data;

public partial class FlowerDbContext : DbContext
{
    public FlowerDbContext()
    {
    }

    public FlowerDbContext(DbContextOptions<FlowerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Flower> Flowers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    private string? GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DefaultConnectionStringDB"];
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flower>(entity =>
        {
            entity.HasKey(e => e.FlowerId).HasName("PK__Flowers__97C47C59B5840601");

            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
