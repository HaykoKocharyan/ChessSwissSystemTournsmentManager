namespace SwissChessTournament.Data
{
    public class Match
    {
        public int Id { get; set; }
        public int Round { get; set; }

        // Foreign keys
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }

        public required string Result { get; set; } // "Win", "Draw", or "Lose"

        // Navigation properties
        public required Player Player1 { get; set; }
        public required Player Player2 { get; set; }
    }
}
