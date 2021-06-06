using System;
using Microsoft.EntityFrameworkCore;
using Rebalancing.Core;

namespace Rebalancing.Data
{
    public class RebalancingDbContext : DbContext
    {
        public RebalancingDbContext(DbContextOptions<RebalancingDbContext> options)
           : base(options)
        { }

        public DbSet<Security> Securities { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<DesiredPosition> DesiredPositions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Security>()
                .ToTable("Security")
                .HasKey(x => x.SecurityId);

            modelBuilder.Entity<Security>()
                .HasIndex(x => x.Symbol)
                .IsUnique();

            modelBuilder.Entity<Transaction>()
                .ToTable("Transaction")
                .HasKey(x => x.TransactionId);

            modelBuilder.Entity<DesiredPosition>()
                .ToTable("Position")
                .HasKey(x => x.PositionId);
        }
    }
}
