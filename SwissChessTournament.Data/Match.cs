using SwissChessTournament.Data;

public class Match
{
    public int Id { get; set; }
    public int Round { get; set; }
    public int Player1Id { get; set; }
    public int Player2Id { get; set; }

    public int? WinnerId { get; set; } // Nullable for draws
    public double Player1Score { get; set; }
    public double Player2Score { get; set; }

    // NEW: Result Property (Win/Loss/Draw)
    public required string Result { get; set; } // "Win", "Loss", "Draw"

    // Foreign Key Relationships
    public virtual required Player Player1 { get; set; }
    public virtual required Player Player2 { get; set; }
    public virtual required Player Winner { get; set; }
}
