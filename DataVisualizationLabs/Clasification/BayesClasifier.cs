using Clasification.Abstraction;
using Helpers;
using Models.Abstraction;
using System.Collections.Generic;

namespace Clasification
{
    public class BayesClasifier : IClasifier
    {
        public BayesClasifier(IEnumerable<IClasified> samples, IClasified clasified)
        {
            Clasified = clasified;
            Samples = new List<IClasified>(samples);
            Apriore = new AprioreResult();
            Aposterior = new AposteriorResult();
        }

        public List<IClasified> Samples { get; set; }
        public IClasified Clasified { get; set; }
        public AprioreResult Apriore { get; set; }
        public AposteriorResult Aposterior { get; set; }

        public void Clasify()
        {
            // Counting coeficient.
            var result = 1d;
            Aposterior.CountCoeficients(Clasified, Samples, Apriore);

            foreach (var p in Aposterior.PositiveResultCoeficients)
            {
                result *= p;
            }

            result *= Apriore.PositiveResultCoeficient;

            // Clasifying.
            if (result > 0.5)
            {
                Clasified.ClasificationResult = true;
            }
            if (result < 0.5)
            {
                Clasified.ClasificationResult = false;
            }
        }
    }
}
