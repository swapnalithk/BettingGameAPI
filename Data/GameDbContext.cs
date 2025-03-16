using BettingGameAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BettingGameAPI.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Bet> Bets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bet>()
                .HasOne(b => b.Player)      
                .WithMany()                 
                .HasForeignKey(b => b.PlayerId) 
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
