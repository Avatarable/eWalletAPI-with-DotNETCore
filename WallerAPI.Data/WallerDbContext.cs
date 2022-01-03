using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WallerAPI.Models.Domain;

namespace WallerAPI.Data
{
    public class WallerDbContext : IdentityDbContext<User>
    {
        public WallerDbContext(DbContextOptions<WallerDbContext> options) : base(options)
        {
        }

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Wallet>()
                .Property(p => p.Balance)
                .HasColumnType("decimal(14,2)");

            builder.Entity<Transaction>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(14,2)");

            builder.Entity<Photo>()
                .HasOne(x => x.User)
                .WithOne(x => x.Photo);

        }
    }
}
