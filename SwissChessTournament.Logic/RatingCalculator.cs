using SwissChessTournament.Data;

namespace SwissChessTournament.Logic
{
    public static class RatingCalculator
    {
        public static void UpdateRatings(Player winner, Player loser, bool isDraw)
        {
            int kFactor = 32;
            double expectedWin = 1 / (1 + Math.Pow(10, (loser.MainRating - winner.MainRating) / 400.0));
            double expectedLoss = 1 - expectedWin;

            if (isDraw)
            {
                winner.MainRating += (int)(kFactor * (0.5 - expectedWin));
                loser.MainRating += (int)(kFactor * (0.5 - expectedLoss));
            }
            else
            {
                winner.MainRating += (int)(kFactor * (1 - expectedWin));
                loser.MainRating += (int)(kFactor * (0 - expectedLoss));
            }
        }
    }
}
