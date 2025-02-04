using System.Collections.Generic;
using System.Linq;
using SwissChessTournament.Data;

namespace SwissChessTournament.Logic
{
    public static class SwissPairing
    {
        public static List<(Player, Player)> GeneratePairs(List<Player> players)
        {
            players = players.OrderByDescending(p => p.Score).ThenByDescending(p => p.MainRating).ToList();
            List<(Player, Player)> pairs = new List<(Player, Player)>();
            HashSet<int> pairedIds = new HashSet<int>();

            for (int i = 0; i < players.Count - 1; i++)
            {
                if (pairedIds.Contains(players[i].Id)) continue;

                for (int j = i + 1; j < players.Count; j++)
                {
                    if (!pairedIds.Contains(players[j].Id))
                    {
                        pairs.Add((players[i], players[j]));
                        pairedIds.Add(players[i].Id);
                        pairedIds.Add(players[j].Id);
                        break;
                    }
                }
            }

            return pairs;
        }
    }
}
