using Clasification.Abstraction;
using Enums;
using Helpers;
using Models;
using Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clasification
{
    public class ID3Clasifier : IClasifier
    {
        private ID3ResultSet _resultSet;

        public ID3Clasifier(ID3ResultSet resultSet)
        {
            _resultSet = resultSet;
        }

        public string Result { get; private set; }

        public void Clasify()
        {
            var minimizedClacified = new List<IClasified>(_resultSet.DetermineMinimizedResultSet());
            if (minimizedClacified[0] is Game)
            {
                var games = new List<Game>();
                foreach (var game in minimizedClacified)
                {
                    games.Add(game as Game);
                }
                ClasifyGame(games);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void ClasifyGame(IEnumerable<Game> games)
        {
            var gamesCount = games.Count();
            var opponents = (from game in games
                             where game.Opponent == GameOpponent.Stronger
                             select game).Count();

            var place = (from game in games
                         where game.Placement == GamePlacement.Home
                         select game).Count();

            var leaders = (from game in games
                           where game.Leaders == GameLeaders.Playing
                           select game).Count();

            var rain = (from game in games
                        where game.IsRaining
                        select game).Count();

            var str = new StringBuilder();
            str.Append("If: ");
            if (opponents == gamesCount)
            {
                str.Append("stronger opponents, ");
            }
            if (opponents == 0)
            {
                str.Append("weaker opponents, ");
            }
            if (place == gamesCount)
            {
                str.Append("game at home, ");
            }
            if (place == 0)
            {
                str.Append("game away, ");
            }
            if (leaders == gamesCount)
            {
                str.Append("leaders playing, ");
            }
            if (leaders == 0)
            {
                str.Append("leaders not playing, ");
            }
            if (rain == gamesCount)
            {
                str.Append("raining, ");
            }
            if (rain == 0)
            {
                str.Append("not raining, ");
            }
            str.Append("then game is probably won");

            Result = str.ToString();
        }
    }
}
