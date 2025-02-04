using Microsoft.EntityFrameworkCore;
using System.Configuration;


namespace SwissChessTournament.Data
{
    public class TournamentContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }

        private readonly string _connectionString;

        // Default constructor for the global players database
        public TournamentContext()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["GlobalPlayersConnection"].ConnectionString;
        }

        // Constructor for a specific connection string (e.g., for tournaments)
        public TournamentContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connectionString);
            }
        }
    



// Configure the database schema
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the Player table
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(p => p.Id); // Primary Key
                entity.Property(p => p.Name).IsRequired(); // Required column
                entity.Property(p => p.MainRating).IsRequired();
                entity.Property(p => p.Score).IsRequired();
                entity.Property(p => p.TotalGamesPlayed).IsRequired();
            });

            // Define the Match table
            modelBuilder.Entity<Match>(entity =>
            {
                entity.HasKey(m => m.Id); // Primary Key
                entity.Property(m => m.Round).IsRequired();
                entity.Property(m => m.Result).IsRequired();

                // Foreign Key: Player1Id references Player.Id
                entity.HasOne<Player>()
                      .WithMany(p => p.Matches)
                      .HasForeignKey(m => m.Player1Id)
                      .OnDelete(DeleteBehavior.Restrict);

                // Foreign Key: Player2Id references Player.Id
                entity.HasOne<Player>()
                      .WithMany(p => p.Matches)
                      .HasForeignKey(m => m.Player2Id)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
