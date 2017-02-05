using Helpers;
using Models;
using System.Collections.Generic;

namespace Clasification
{
    public class BayesClasifier
    {
        public BayesClasifier(IEnumerable<Extusia> extusias, Extusia clasified)
        {
            ClasifiedExtusia = clasified;
            Extusias = new List<Extusia>(extusias);
            Apriore = new AprioreResult();
            Aposterior = new AposteriorResult();
        }

        public List<Extusia> Extusias { get; set; }
        public Extusia ClasifiedExtusia { get; set; }
        public AprioreResult Apriore { get; set; }
        public AposteriorResult Aposterior { get; set; }

        public void Clasify()
        {
            // Counting coeficient.
            var result = 1d;
            Aposterior.CountCoeficients(ClasifiedExtusia, Extusias, Apriore);

            foreach (var p in Aposterior.PositiveResultCoeficients)
            {
                result *= p;
            }

            result *= Apriore.PositiveResultCoeficient;

            // Clasifying.
            if (result > 0.5)
            {
                ClasifiedExtusia.Stopper = true;
            }
            if (result < 0.5)
            {
                ClasifiedExtusia.Stopper = false;
            }
        }
    }
}
