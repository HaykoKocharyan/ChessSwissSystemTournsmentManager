using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using SwissChessTournament.Data;

namespace SwissChessTournament.Logic
{
    public static class TournamentManager
    {
        public static string CreateTournamentDatabase(string tournamentName)
        {
            string dbName = $"tournament_{Guid.NewGuid()}";
            string globalConnectionString = ConfigurationManager.ConnectionStrings["GlobalPlayersConnection"].ConnectionString;

            using (var connection = new NpgsqlConnection(globalConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"CREATE DATABASE \"{dbName}\"";
                    command.ExecuteNonQuery();
                }
            }

            // Return the connection string for the new tournament database
            return $"Host=localhost;Database={dbName};Username=postgres;Password=yourpassword";
        }

        public static void ApplyMigrations(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TournamentContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using (var context = new TournamentContext(connectionString))
            {
                context.Database.Migrate();
            }
        }

        public static void AddPlayersToTournament(string connectionString, List<Player> players)
        {
            using (var context = new TournamentContext(connectionString))
            {
                foreach (var player in players)
                {
                    context.Players.Add(new Player
                    {
                        Name = player.Name,
                        MainRating = player.MainRating,
                        Score = 0,
                        TotalGamesPlayed = player.TotalGamesPlayed
                    });
                }
                context.SaveChanges();
            }
        }

        public static List<Player> GetTournamentPlayers(string connectionString)
        {
            using (var context = new TournamentContext(connectionString))
            {
                return context.Players.ToList();
            }
        }
    }
}
