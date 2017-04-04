using Clasification;
using Enums;
using Helpers;
using Models;
using Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // -- Lab 3 --
            // Creating samples.
            var sample = new List<IClasified>();
            sample.Add(new Extusia(120, 30, Density.Low, 3, 165, false));
            sample.Add(new Extusia(110, 35, Density.Low, 2, 150, false));
            sample.Add(new Extusia(130, 40, Density.High, 7, 155, true));
            sample.Add(new Extusia(120, 35, Density.Low, 5, 165, false));
            sample.Add(new Extusia(130, 25, Density.High, 2, 180, false));
            sample.Add(new Extusia(90, 60, Density.High, 6, 150, true));
            sample.Add(new Extusia(100, 50, Density.Low, 5, 150, true));
            sample.Add(new Extusia(140, 30, Density.High, 3, 200, false));
            sample.Add(new Extusia(110, 40, Density.High, 3, 160, false));
            sample.Add(new Extusia(120, 50, Density.High, 2, 155, false));
            sample.Add(new Extusia(130, 35, Density.Low, 4, 165, false));
            sample.Add(new Extusia(120, 40, Density.High, 5, 150, true));
            sample.Add(new Extusia(115, 35, Density.Low, 4, 155, false));
            sample.Add(new Extusia(135, 40, Density.High, 7, 150, true));
            sample.Add(new Extusia(120, 35, Density.Low, 4, 170, false));
            sample.Add(new Extusia(115, 25, Density.High, 3, 140, false));
            sample.Add(new Extusia(100, 60, Density.High, 3, 180, true));
            sample.Add(new Extusia(100, 50, Density.High, 2, 175, false));
            sample.Add(new Extusia(140, 30, Density.Low, 3, 200, false));
            sample.Add(new Extusia(110, 50, Density.High, 4, 165, true));
            sample.Add(new Extusia(120, 25, Density.Low, 2, 180, false));
            sample.Add(new Extusia(130, 30, Density.Low, 3, 200, false));

            // Init clasified.
            var clasifier = new Extusia(100, 35, Density.High, 5, 150);

            // Init clasifier.
            var bayesClasifier = new BayesClasifier(sample, clasifier);
            bayesClasifier.Clasify();
            Console.WriteLine(bayesClasifier.Clasified.ClasificationResult);

            // -- Lab 4 --
            // Creating samples.
            var games = new List<IClasified>();
            games.Add(new Game(GameOpponent.Stronger, GamePlacement.Home, GameLeaders.Playing, true, false));
            games.Add(new Game(GameOpponent.Stronger, GamePlacement.Home, GameLeaders.Playing, false, true));
            games.Add(new Game(GameOpponent.Stronger, GamePlacement.Home, GameLeaders.NotPlaying, false, true));
            games.Add(new Game(GameOpponent.Weaker, GamePlacement.Home, GameLeaders.NotPlaying, false, true));
            games.Add(new Game(GameOpponent.Weaker, GamePlacement.Away, GameLeaders.NotPlaying, false, false));
            games.Add(new Game(GameOpponent.Weaker, GamePlacement.Home, GameLeaders.NotPlaying, true, true));
            games.Add(new Game(GameOpponent.Stronger, GamePlacement.Away, GameLeaders.Playing, true, false));
            games.Add(new Game(GameOpponent.Weaker, GamePlacement.Away, GameLeaders.Playing, false, true));

            var id3Clasifier = new ID3Clasifier(new ID3ResultSet(games));
            id3Clasifier.Clasify();

        }
    }
}
