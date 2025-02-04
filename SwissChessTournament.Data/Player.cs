using System.Collections.Generic;

namespace SwissChessTournament.Data
{
    public class Player
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int MainRating { get; set; }
        public double Score { get; set; }
        public int TotalGamesPlayed { get; set; }

        // Navigation property for related matches
        public ICollection<Match> Matches { get; set; } = new List<Match>(); // Default value
    }
}
