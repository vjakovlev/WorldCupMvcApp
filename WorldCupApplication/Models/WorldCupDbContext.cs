using System.Data.Entity;

namespace WorldCupApplication.Models
{
    public class WorldCupDbContext : DbContext
    {
        public WorldCupDbContext() : base("WorldCupDb")
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<UserAccount> UserAccount { get; set; }
    }
}