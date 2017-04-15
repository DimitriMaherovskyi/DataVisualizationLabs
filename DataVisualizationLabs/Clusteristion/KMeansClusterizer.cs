using System;
using System.Text;

namespace Clusterisation
{
    public class KMeansClusterizer
    {
        public int[] Cluster(double[][] rawData, int numClusters)
        {
            var data = Normalized(rawData);
            var changed = true;
            var success = true;
            var clustering = InitClustering(data.Length, numClusters, 0);
            var means = Allocate(numClusters, data[0].Length);
            var maxCount = data.Length * 10;
            var ct = 0;
            while (changed == true && success == true && ct < maxCount)
            {
                ++ct;
                success = UpdateMeans(data, clustering, means);
                changed = UpdateClustering(data, clustering, means);
            }

            return clustering;
        }

        private double[][] Normalized(double[][] rawData)
        {
            var result = new double[rawData.Length][];
            for (var i = 0; i < rawData.Length; ++i)
            {
                result[i] = new double[rawData[i].Length];
                Array.Copy(rawData[i], result[i], rawData[i].Length);
            }

            for (int j = 0; j < result[0].Length; ++j)
            {
                var colSum = 0d;
                for (var i = 0; i < result.Length; ++i)
                {
                    colSum += result[i][j];
                }

                var mean = colSum / result.Length;
                var sum = 0d;
                for (var i = 0; i < result.Length; ++i)
                {
                    sum += (result[i][j] - mean) * (result[i][j] - mean);
                }

                var sd = sum / result.Length;
                for (var i = 0; i < result.Length; ++i)
                {
                    result[i][j] = (result[i][j] - mean) / sd;
                }
            }

            return result;
        }

        private int[] InitClustering(int numTuples, int numClusters, int seed)
        {
            var random = new Random(seed);
            var clustering = new int[numTuples];
            for (var i = 0; i < numClusters; ++i)
            {
                clustering[i] = i;
            }

            for (var i = numClusters; i < clustering.Length; ++i)
            {
                clustering[i] = random.Next(0, numClusters);
            }

            return clustering;
        }

        private bool UpdateMeans(double[][] data, int[] clustering, double[][] means)
        {
            var numClusters = means.Length;
            var clusterCounts = new int[numClusters];
            for (var i = 0; i < data.Length; ++i)
            {
                var cluster = clustering[i];
                ++clusterCounts[cluster];
            }

            for (var k = 0; k < numClusters; ++k)
            {
                if (clusterCounts[k] == 0)
                {
                    return false;
                }
            }

            for (int k = 0; k < means.Length; ++k)
            {
                for (var j = 0; j < means[k].Length; ++j)
                {
                    means[k][j] = 0.0;
                }
            }

            for (int i = 0; i < data.Length; ++i)
            {
                var cluster = clustering[i];
                for (var j = 0; j < data[i].Length; ++j)
                {
                    means[cluster][j] += data[i][j]; // accumulate sum
                }
            }

            for (var k = 0; k < means.Length; ++k)
            {
                for (var j = 0; j < means[k].Length; ++j)
                {
                    means[k][j] /= clusterCounts[k]; // danger of div by 0
                }
            }
            return true;
        }

        private static double[][] Allocate(int numClusters, int numColumns)
        {
            var result = new double[numClusters][];
            for (var k = 0; k < numClusters; ++k)
            {
                result[k] = new double[numColumns];
            }

            return result;
        }

        private bool UpdateClustering(double[][] data, int[] clustering, double[][] means)
        {
            var numClusters = means.Length;
            var changed = false;

            var newClustering = new int[clustering.Length];
            Array.Copy(clustering, newClustering, clustering.Length);

            var distances = new double[numClusters];

            for (var i = 0; i < data.Length; ++i)
            {
                for (var k = 0; k < numClusters; ++k)
                {
                    distances[k] = Distance(data[i], means[k]);
                }

                var newClusterID = MinIndex(distances);
                if (newClusterID != newClustering[i])
                {
                    changed = true;
                    newClustering[i] = newClusterID;
                }
            }

            if (!changed)
            {
                return false;
            }

            var clusterCounts = new int[numClusters];
            for (var i = 0; i < data.Length; ++i)
            {
                var cluster = newClustering[i];
                ++clusterCounts[cluster];
            }

            for (var k = 0; k < numClusters; ++k)
            {
                if (clusterCounts[k] == 0)
                {
                    return false;
                }
            }

            Array.Copy(newClustering, clustering, newClustering.Length);
            return true; // no zero-counts and at least one change
        }

        private double Distance(double[] tuple, double[] mean)
        {
            var sumSquaredDiffs = 0d;
            for (var j = 0; j < tuple.Length; ++j)
            {
                sumSquaredDiffs += Math.Pow((tuple[j] - mean[j]), 2);
            }

            return Math.Sqrt(sumSquaredDiffs);
        }

        private int MinIndex(double[] distances)
        {
            var indexOfMin = 0;
            var smallDist = distances[0];
            for (var k = 0; k < distances.Length; ++k)
            {
                if (distances[k] < smallDist)
                {
                    smallDist = distances[k];
                    indexOfMin = k;
                }
            }

            return indexOfMin;
        }

        public string ShowData(double[][] data, int decimals,
            bool indices, bool newLine)
        {
            var str = new StringBuilder();
            for (var i = 0; i < data.Length; ++i)
            {
                if (indices)
                {
                    str.Append($"{i.ToString().PadLeft(3)} ");
                }

                for (int j = 0; j < data[i].Length; ++j)
                {
                    if (data[i][j] >= 0.0)
                    {
                        str.Append(" ");
                    }
                    str.Append($"{data[i][j].ToString("F" + decimals)} ");
                }
                str.AppendLine("");
            }

            if (newLine)
            {
                str.AppendLine("");
            }

            return str.ToString();
        }

        public string ShowVector(int[] vector, bool newLine)
        {
            var str = new StringBuilder();
            for (var i = 0; i < vector.Length; ++i)
            {
                str.Append($"{vector[i]} ");
            }
            if (newLine)
            {
                str.AppendLine("");
                //Console.WriteLine("\n");
            }

            return str.ToString();
        }

        public string ShowClustered(double[][] data, int[] clustering,
          int numClusters, int decimals)
        {
            var str = new StringBuilder();
            for (var k = 0; k < numClusters; ++k)
            {
                str.AppendLine("===================");
                for (var i = 0; i < data.Length; ++i)
                {
                    var clusterID = clustering[i];
                    if (clusterID != k)
                    {
                        continue;
                    }
                    str.Append($"{i.ToString().PadLeft(3)} ");
                    for (var j = 0; j < data[i].Length; ++j)
                    {
                        if (data[i][j] >= 0.0)
                        {
                            str.Append(" ");
                        }
                        str.Append($"{data[i][j].ToString("F" + decimals)} ");
                    }
                    str.AppendLine("");
                }
                str.AppendLine("===================");
            } // k

            return str.ToString();
        }
    }
}
