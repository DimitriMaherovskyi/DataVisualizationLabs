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
        private List<Game> filteredGames;

        private bool _opponentsChecked;
        private bool _placementChecked;
        private bool _leadersChecked;
        private bool _rainChecked;

        public GameResultSetHelper()
        {
            _opponentsChecked = false;
            _placementChecked = false;
            _leadersChecked = false;
            _rainChecked = false;
            filteredGames = new List<Game>();
        }

        public IEnumerable<IClasified> DetermineGameCoeficients(IEnumerable<IClasified> clasified)
        {
            var games = ConvertIClacified(clasified);

            var coeficients = new List<ID3Coeficient>();

            var totalWinGamesCount = (from g in games
                                      where g.ClasificationResult == true
                                      select g).Count();

            while (games.Count() != totalWinGamesCount)
            {
                filteredGames = new List<Game>();
                coeficients = new List<ID3Coeficient>();

                if (!_opponentsChecked)
                {
                    coeficients.Add(CalculateStrongerOpponentCoeficient(games));
                    coeficients.Add(CalculateWeakerOpponentCoeficient(games));
                }
                else
                {
                    coeficients.Add(new ID3Coeficient());
                    coeficients.Add(new ID3Coeficient());
                }
                if (!_placementChecked)
                {
                    coeficients.Add(CalculateHomeGameCoeficient(games));
                    coeficients.Add(CalculateAwayGameCoeficient(games));
                }
                else
                {
                    coeficients.Add(new ID3Coeficient());
                    coeficients.Add(new ID3Coeficient());
                }
                if (!_leadersChecked)
                {
                    coeficients.Add(CalculateGameWithLeadersCoeficient(games));
                    coeficients.Add(CalculateGameWithoutLeadersCoeficient(games));
                }
                else
                {
                    coeficients.Add(new ID3Coeficient());
                    coeficients.Add(new ID3Coeficient());
                }
                if (!_rainChecked)
                {
                    coeficients.Add(CalculateGameWithRainCoeficient(games));
                    coeficients.Add(CalculateGameWithoutRainCoeficient(games));
                }
                else
                {
                    coeficients.Add(new ID3Coeficient());
                    coeficients.Add(new ID3Coeficient());
                }

                var greatestCoeficient = (from coef in coeficients
                                          //where coef.Coeficient != 1d
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
                break;
            }

            return filteredGames;
        }

        private void SetCheckersFalse()
        {
            _opponentsChecked = false;
            _placementChecked = false;
            _leadersChecked = false;
            _rainChecked = false;
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
                        _opponentsChecked = true;
                        break;
                    }
                case 1:
                    {
                        filteredGames = (from g in games
                                         where g.Opponent == GameOpponent.Weaker
                                         select g).ToList();
                        _opponentsChecked = true;
                        break;
                    }
                case 2:
                    {
                        filteredGames = (from g in games
                                         where g.Placement == GamePlacement.Home
                                         select g).ToList();
                        _placementChecked = true;
                        break;
                    }
                case 3:
                    {
                        filteredGames = (from g in games
                                         where g.Placement == GamePlacement.Away
                                         select g).ToList();
                        _placementChecked = true;
                        break;
                    }
                case 4:
                    {
                        filteredGames = (from g in games
                                         where g.Leaders == GameLeaders.Playing
                                         select g).ToList();
                        _leadersChecked = true;
                        break;
                    }
                case 5:
                    {
                        filteredGames = (from g in games
                                         where g.Leaders == GameLeaders.NotPlaying
                                         select g).ToList();
                        _leadersChecked = true;
                        break;
                    }
                case 6:
                    {
                        filteredGames = (from g in games
                                         where g.IsRaining
                                         select g).ToList();
                        _rainChecked = true;
                        break;
                    }
                case 7:
                    {
                        filteredGames = (from g in games
                                         where !g.IsRaining
                                         select g).ToList();
                        _rainChecked = true;
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

        private ID3Coeficient CalculateStrongerOpponentCoeficient(IEnumerable<Game> games)
        {
            double strongerOpponentCount = (from game in games
                                            where game.Opponent == GameOpponent.Stronger
                                            select game).Count();

            if (strongerOpponentCount == 0)
            {
                return new ID3Coeficient();
            }

            double strongerOpponentWinCount = (from game in games
                                               where game.Opponent == GameOpponent.Stronger &&
                                               game.ClasificationResult == true
                                               select game).Count();

            return new ID3Coeficient(strongerOpponentWinCount / strongerOpponentCount, (int)strongerOpponentCount); 
        }

        private ID3Coeficient CalculateWeakerOpponentCoeficient(IEnumerable<Game> games)
        {
            double weakerOpponentCount = (from game in games
                                       where game.Opponent == GameOpponent.Weaker
                                       select game).Count();

            if (weakerOpponentCount == 0)
            {
                return new ID3Coeficient();
            }

            double weakerOpponentWinCount = (from game in games
                                          where game.Opponent == GameOpponent.Weaker &&
                                          game.ClasificationResult == true
                                          select game).Count();

            return new ID3Coeficient(weakerOpponentWinCount / weakerOpponentCount, (int)weakerOpponentCount);
        }

        private ID3Coeficient CalculateHomeGameCoeficient(IEnumerable<Game> games)
        {
            double homeGameCount = (from game in games
                                 where game.Placement == GamePlacement.Home
                                 select game).Count();

            if (homeGameCount == 0)
            {
                return new ID3Coeficient();
            }

            double homeGameWinCount = (from game in games
                                    where game.Placement == GamePlacement.Home &&
                                    game.ClasificationResult == true
                                    select game).Count();

            return new ID3Coeficient(homeGameWinCount / homeGameCount, (int)homeGameCount);
        }

        private ID3Coeficient CalculateAwayGameCoeficient(IEnumerable<Game> games)
        {
            double awayGameCount = (from game in games
                                 where game.Placement == GamePlacement.Away
                                 select game).Count();

            if (awayGameCount == 0)
            {
                return new ID3Coeficient();
            }

            double awayGameWinCount = (from game in games
                                    where game.Placement == GamePlacement.Away &&
                                    game.ClasificationResult == true
                                    select game).Count();

            return new ID3Coeficient(awayGameWinCount / awayGameCount, (int)awayGameCount);
        }

        private ID3Coeficient CalculateGameWithLeadersCoeficient(IEnumerable<Game> games)
        {
            double gameWithLeadersCount = (from game in games
                                        where game.Leaders == GameLeaders.Playing
                                        select game).Count();

            if (gameWithLeadersCount == 0)
            {
                return new ID3Coeficient();
            }

            double gameWithLeadersWinCount = (from game in games
                                           where game.Leaders == GameLeaders.Playing &&
                                           game.ClasificationResult == true
                                           select game).Count();

            return new ID3Coeficient(gameWithLeadersWinCount / gameWithLeadersCount, (int)gameWithLeadersCount);
        }

        private ID3Coeficient CalculateGameWithoutLeadersCoeficient(IEnumerable<Game> games)
        {
            double gameWithoutLeadersCount = (from game in games
                                           where game.Leaders == GameLeaders.NotPlaying
                                           select game).Count();

            if (gameWithoutLeadersCount == 0)
            {
                return new ID3Coeficient();
            }

            double gameWithoutLeadersWinCount = (from game in games
                                              where game.Leaders == GameLeaders.NotPlaying &&
                                              game.ClasificationResult == true
                                              select game).Count();

            return new ID3Coeficient(gameWithoutLeadersWinCount / gameWithoutLeadersCount, (int)gameWithoutLeadersCount);
        }

        private ID3Coeficient CalculateGameWithRainCoeficient(IEnumerable<Game> games)
        {
            double gameWithRainCount = (from game in games
                                     where game.IsRaining
                                     select game).Count();

            if (gameWithRainCount == 0)
            {
                return new ID3Coeficient();
            }

            double gameWithRainWinCount = (from game in games
                                        where game.IsRaining &&
                                        game.ClasificationResult == true
                                        select game).Count();

            return new ID3Coeficient(gameWithRainWinCount / gameWithRainCount, (int)gameWithRainCount);
        }

        private ID3Coeficient CalculateGameWithoutRainCoeficient(IEnumerable<Game> games)
        {
            double gameWithoutRainCount = (from game in games
                                        where !game.IsRaining
                                        select game).Count();

            if (gameWithoutRainCount == 0)
            {
                return new ID3Coeficient();
            }

            double gameWithoutRainWinCount = (from game in games
                                           where !game.IsRaining &&
                                           game.ClasificationResult == true
                                           select game).Count();

            return new ID3Coeficient(gameWithoutRainWinCount / gameWithoutRainCount, (int)gameWithoutRainCount);
        }
    }
}
