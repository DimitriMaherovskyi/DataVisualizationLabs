using Enums;
using Models.Abstraction;

namespace Models
{
    public class Game : IClasified
    {
        public Game(GameOpponent opponent, GamePlacement placement, GameLeaders leaders,
            bool isRaining, bool clasificationResult)
        {
            Opponent = opponent;
            Placement = placement;
            Leaders = leaders;
            IsRaining = isRaining;
            ClasificationResult = clasificationResult;
        }

        public GameOpponent Opponent { get; set; }
        public GamePlacement Placement { get; set; }
        public GameLeaders Leaders { get; set; }
        public bool IsRaining { get; set; }
        public bool? ClasificationResult { get; set; }
    }
}
