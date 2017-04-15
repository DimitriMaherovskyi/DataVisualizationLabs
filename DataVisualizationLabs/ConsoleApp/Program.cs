using Clasification;
using Clusterisation;
using Enums;
using Helpers;
using Models;
using Models.Abstraction;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Lab3();
            Lab4();
            Lab5();
        }

        private static void Lab3()
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
        }

        private static void Lab4()
        {
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

            // Init clasifier.
            var id3Clasifier = new ID3Clasifier(new ID3ResultSet(games));
            id3Clasifier.Clasify();
            Console.WriteLine(id3Clasifier.Result);
        }

        private static void Lab5()
        {
            var kmeansClusterizer = new KMeansClusterizer();

            // Creating samples.
            var rawData = new double[20][];
            rawData[0] = new double[] { 65.0, 220.0 };
            rawData[1] = new double[] { 73.0, 160.0 };
            rawData[2] = new double[] { 59.0, 110.0 };
            rawData[3] = new double[] { 61.0, 120.0 };
            rawData[4] = new double[] { 75.0, 150.0 };
            rawData[5] = new double[] { 67.0, 240.0 };
            rawData[6] = new double[] { 68.0, 230.0 };
            rawData[7] = new double[] { 70.0, 220.0 };
            rawData[8] = new double[] { 62.0, 130.0 };
            rawData[9] = new double[] { 66.0, 210.0 };
            rawData[10] = new double[] { 77.0, 190.0 };
            rawData[11] = new double[] { 75.0, 180.0 };
            rawData[12] = new double[] { 74.0, 170.0 };
            rawData[13] = new double[] { 70.0, 210.0 };
            rawData[14] = new double[] { 61.0, 110.0 };
            rawData[15] = new double[] { 58.0, 100.0 };
            rawData[16] = new double[] { 66.0, 230.0 };
            rawData[17] = new double[] { 59.0, 120.0 };
            rawData[18] = new double[] { 68.0, 210.0 };
            rawData[19] = new double[] { 61.0, 130.0 };

            Console.WriteLine("Raw unclustered data:");
            Console.WriteLine("    Height Weight");
            Console.WriteLine("-------------------");
            Console.WriteLine(kmeansClusterizer.ShowData(rawData, 1, true, true));

            var numClusters = 3;
            Console.WriteLine("Setting numClusters to " + numClusters);

            var clustering = kmeansClusterizer.Cluster(rawData, numClusters);

            Console.WriteLine("K-means clustering complete");

            Console.WriteLine("Final clustering in internal form:");
            Console.WriteLine(kmeansClusterizer.ShowVector(clustering, true));

            Console.WriteLine("Raw data by cluster:");
            Console.WriteLine(kmeansClusterizer.ShowClustered(rawData, clustering, numClusters, 1));

            Console.WriteLine("End k-means clustering");
        }        
    }
}
