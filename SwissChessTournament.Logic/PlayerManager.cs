using System.Collections.Generic;
using System.Linq;
using SwissChessTournament.Data;

namespace SwissChessTournament.Logic
{
    public static class PlayerManager
    {
        public static void AddPlayer(string name, int rating)
        {
            var player = new Player { Name = name, MainRating = rating, Score = 0 };
            SavePlayer(player);
        }
        private static void SavePlayer(Player player)
        {
            using (var context = new TournamentContext())
            {
                context.Players.Add(player);
                context.SaveChanges();
            }
        }


        public static List<Player> GetAllPlayers()
        {
            using (var context = new TournamentContext()) // Default constructor
            {
                return context.Players.OrderByDescending(p => p.MainRating).ToList();
            }
        }

        public static void UpdatePlayerRating(int playerId, int newRating)
        {
            using (var context = new TournamentContext()) // Default constructor
            {
                var player = context.Players.Find(playerId);
                if (player != null)
                {
                    player.MainRating = newRating;
                    player.TotalGamesPlayed++;
                    context.SaveChanges();
                }
            }
        }
    }
}
