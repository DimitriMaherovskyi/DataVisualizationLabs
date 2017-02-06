using Models;
using Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Helpers
{
    public class AposteriorResult
    {
        public List<double> PositiveResultCoeficients { get; set; }

        public void CountCoeficients(IClasified clasified, IEnumerable<IClasified> samples, AprioreResult apriore)
        {
            if (clasified is Extusia)
            {
                CountExtusia((Extusia)clasified, (IEnumerable<Extusia>)samples, apriore);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void CountExtusia(Extusia clasifiedExtusia, IEnumerable<Extusia> extusias, AprioreResult apriore)
        {
            apriore.Models = extusias;
            apriore.GetPositiveResult();

            var RpmCoeficient = (from e in extusias
                                 where e.RPM == clasifiedExtusia.RPM &&
                                 e.ClasificationResult == true
                                 select e).Count() / apriore.PositiveResultCount;

            var GranulaSizeCoeficient = (from e in extusias
                                         where e.GranulaSize == clasifiedExtusia.GranulaSize &&
                                         e.ClasificationResult == true
                                         select e).Count() / apriore.PositiveResultCount;

            var ResourceCoeficient = (from e in extusias
                                      where e.ResourceConsumptiongPerHour == clasifiedExtusia.ResourceConsumptiongPerHour &&
                                      e.ClasificationResult == true
                                      select e).Count() / apriore.PositiveResultCount;

            var TemperatureCoeficient = (from e in extusias
                                         where e.Temperature == clasifiedExtusia.Temperature &&
                                         e.ClasificationResult == true
                                         select e).Count() / apriore.PositiveResultCount;

            PositiveResultCoeficients.Add(GranulaSizeCoeficient);
            PositiveResultCoeficients.Add(RpmCoeficient);
            PositiveResultCoeficients.Add(ResourceCoeficient);
            PositiveResultCoeficients.Add(TemperatureCoeficient);
        }
    }
}
