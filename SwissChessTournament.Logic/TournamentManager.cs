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

        public static void SaveMatchResult(string connectionString, int matchId, int player1Score, int player2Score)
        {
            using (var context = new TournamentContext(connectionString))
            {
                var match = context.Matches.Find(matchId);
                if (match != null)
                {
                    match.Player1Score = player1Score;
                    match.Player2Score = player2Score;

                    // Determine the winner
                    if (player1Score > player2Score)
                    {
                        match.WinnerId = match.Player1Id;
                    }
                    else if (player2Score > player1Score)
                    {
                        match.WinnerId = match.Player2Id;
                    }
                    else
                    {
                        match.WinnerId = null; // Draw
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
