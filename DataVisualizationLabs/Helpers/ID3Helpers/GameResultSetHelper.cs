using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers.Interfaces;
using Models.Abstraction;
using Models;
using Enums;

namespace Helpers.ID3Helpers
{
    public class GameResultSetHelper : IID3Helper
    {
        public IEnumerable<IClasified> DetermineGameCoeficients(IEnumerable<IClasified> clasified)
        {
            var games = ConvertIClacified(clasified);

            var coeficients = new List<double>();
            var filteredGames = new List<Game>();

            var totalWinGamesCount = (from g in games
                                      where g.ClasificationResult == true
                                      select g).Count();

            while (games.Count() != totalWinGamesCount)
            {
                filteredGames = new List<Game>();

                coeficients.Add(CalculateStrongerOpponentCoeficient(games));
                coeficients.Add(CalculateWeakerOpponentCoeficient(games));
                coeficients.Add(CalculateHomeGameCoeficient(games));
                coeficients.Add(CalculateAwayGameCoeficient(games));
                coeficients.Add(CalculateGameWithLeadersCoeficient(games));
                coeficients.Add(CalculateGameWithoutLeadersCoeficient(games));
                coeficients.Add(CalculateGameWithRainCoeficient(games));
                coeficients.Add(CalculateGameWithoutRainCoeficient(games));

                var greatestCoeficient = (from coef in coeficients
                                          where coef != 1d
                                          select coef).Max();

                for (var i = 0; i < coeficients.Count; i++)
                {
                    if (greatestCoeficient == coeficients[i])
                    {
                        filteredGames = FilterGames(i, games);
                        DetermineGameCoeficients(filteredGames);
                        break;
                    }
                }
            }

            return filteredGames;
        }

        private List<Game> FilterGames(int index, List<Game> games)
        {
            var filteredGames = new List<Game>();
            switch (index)
            {
                case 0:
                    {
                        filteredGames = (from g in games
                                         where g.Opponent == GameOpponent.Stronger
                                         select g).ToList();
                        break;
                    }
                case 1:
                    {
                        filteredGames = (from g in games
                                         where g.Opponent == GameOpponent.Weaker
                                         select g).ToList();
                        break;
                    }
                case 2:
                    {
                        filteredGames = (from g in games
                                         where g.Placement == GamePlacement.Home
                                         select g).ToList();
                        break;
                    }
                case 3:
                    {
                        filteredGames = (from g in games
                                         where g.Placement == GamePlacement.Away
                                         select g).ToList();
                        break;
                    }
                case 4:
                    {
                        filteredGames = (from g in games
                                         where g.Leaders == GameLeaders.Playing
                                         select g).ToList();
                        break;
                    }
                case 5:
                    {
                        filteredGames = (from g in games
                                         where g.Leaders == GameLeaders.NotPlaying
                                         select g).ToList();
                        break;
                    }
                case 6:
                    {
                        filteredGames = (from g in games
                                         where g.IsRaining
                                         select g).ToList();
                        break;
                    }
                case 7:
                    {
                        filteredGames = (from g in games
                                         where !g.IsRaining
                                         select g).ToList();
                        break;
                    }
            }

            return filteredGames;
        }

        private List<Game> ConvertIClacified(IEnumerable<IClasified> clacified)
        {
            var games = new List<Game>();
            foreach (var game in clacified)
            {
                games.Add(game as Game);
            }

            return games;
        }

        private double CalculateStrongerOpponentCoeficient(IEnumerable<Game> games)
        {
            double strongerOpponentCount = (from game in games
                                            where game.Opponent == GameOpponent.Stronger
                                            select game).Count();

            if (strongerOpponentCount == 0)
            {
                return 0;
            }

            double strongerOpponentWinCount = (from game in games
                                               where game.Opponent == GameOpponent.Stronger &&
                                               game.ClasificationResult == true
                                               select game).Count();

            return strongerOpponentWinCount / strongerOpponentCount; 
        }

        private double CalculateWeakerOpponentCoeficient(IEnumerable<Game> games)
        {
            double weakerOpponentCount = (from game in games
                                       where game.Opponent == GameOpponent.Weaker
                                       select game).Count();

            if (weakerOpponentCount == 0)
            {
                return 0;
            }

            double weakerOpponentWinCount = (from game in games
                                          where game.Opponent == GameOpponent.Weaker &&
                                          game.ClasificationResult == true
                                          select game).Count();

            return weakerOpponentWinCount / weakerOpponentCount;
        }

        private double CalculateHomeGameCoeficient(IEnumerable<Game> games)
        {
            double homeGameCount = (from game in games
                                 where game.Placement == GamePlacement.Home
                                 select game).Count();

            if (homeGameCount == 0)
            {
                return 0;
            }

            double homeGameWinCount = (from game in games
                                    where game.Placement == GamePlacement.Home &&
                                    game.ClasificationResult == true
                                    select game).Count();

            return homeGameWinCount / homeGameCount;
        }

        private double CalculateAwayGameCoeficient(IEnumerable<Game> games)
        {
            double awayGameCount = (from game in games
                                 where game.Placement == GamePlacement.Away
                                 select game).Count();

            if (awayGameCount == 0)
            {
                return 0;
            }

            double awayGameWinCount = (from game in games
                                    where game.Placement == GamePlacement.Away &&
                                    game.ClasificationResult == true
                                    select game).Count();

            return awayGameWinCount / awayGameCount;
        }

        private double CalculateGameWithLeadersCoeficient(IEnumerable<Game> games)
        {
            double gameWithLeadersCount = (from game in games
                                        where game.Leaders == GameLeaders.Playing
                                        select game).Count();

            if (gameWithLeadersCount == 0)
            {
                return 0;
            }

            double gameWithLeadersWinCount = (from game in games
                                           where game.Leaders == GameLeaders.Playing &&
                                           game.ClasificationResult == true
                                           select game).Count();

            return gameWithLeadersWinCount / gameWithLeadersCount;
        }

        private double CalculateGameWithoutLeadersCoeficient(IEnumerable<Game> games)
        {
            double gameWithoutLeadersCount = (from game in games
                                           where game.Leaders == GameLeaders.NotPlaying
                                           select game).Count();

            if (gameWithoutLeadersCount == 0)
            {
                return 0;
            }

            double gameWithoutLeadersWinCount = (from game in games
                                              where game.Leaders == GameLeaders.NotPlaying &&
                                              game.ClasificationResult == true
                                              select game).Count();

            return gameWithoutLeadersWinCount / gameWithoutLeadersCount;
        }

        private double CalculateGameWithRainCoeficient(IEnumerable<Game> games)
        {
            double gameWithRainCount = (from game in games
                                     where game.IsRaining
                                     select game).Count();

            if (gameWithRainCount == 0)
            {
                return 0;
            }

            double gameWithRainWinCount = (from game in games
                                        where game.IsRaining &&
                                        game.ClasificationResult == true
                                        select game).Count();

            return gameWithRainWinCount / gameWithRainCount;
        }

        private double CalculateGameWithoutRainCoeficient(IEnumerable<Game> games)
        {
            double gameWithoutRainCount = (from game in games
                                        where !game.IsRaining
                                        select game).Count();

            if (gameWithoutRainCount == 0)
            {
                return 0;
            }

            double gameWithoutRainWinCount = (from game in games
                                           where !game.IsRaining &&
                                           game.ClasificationResult == true
                                           select game).Count();

            return gameWithoutRainWinCount / gameWithoutRainCount;
        }
    }
}
