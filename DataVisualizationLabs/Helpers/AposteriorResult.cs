using Models;
using System.Collections.Generic;
using System.Linq;

namespace Helpers
{
    public class AposteriorResult
    {
        public List<double> PositiveResultCoeficients { get; set; }

        public void CountCoeficients(Extusia clasifiedExtusia, IEnumerable<Extusia> extusias, AprioreResult apriore)
        {
            apriore.Models = extusias;
            apriore.GetPositiveResult();

            var RpmCoeficient = (from e in extusias
                                 where e.RPM == clasifiedExtusia.RPM &&
                                 e.Stopper == true
                                 select e).Count() / apriore.PositiveResultCount;

            var GranulaSizeCoeficient = (from e in extusias
                                         where e.GranulaSize == clasifiedExtusia.GranulaSize &&
                                         e.Stopper == true
                                         select e).Count() / apriore.PositiveResultCount;

            var ResourceCoeficient = (from e in extusias
                                      where e.ResourceConsumptiongPerHour == clasifiedExtusia.ResourceConsumptiongPerHour &&
                                      e.Stopper == true
                                      select e).Count() / apriore.PositiveResultCount;

            var TemperatureCoeficient = (from e in extusias
                                         where e.Temperature == clasifiedExtusia.Temperature &&
                                         e.Stopper == true
                                         select e).Count() / apriore.PositiveResultCount;

            PositiveResultCoeficients.Add(GranulaSizeCoeficient);
            PositiveResultCoeficients.Add(RpmCoeficient);
            PositiveResultCoeficients.Add(ResourceCoeficient);
            PositiveResultCoeficients.Add(TemperatureCoeficient);
        }
    }
}
