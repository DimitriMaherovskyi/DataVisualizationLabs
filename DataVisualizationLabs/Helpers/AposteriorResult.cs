using Models;
using Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Helpers
{
    public class AposteriorResult
    {
        public AposteriorResult()
        {
            PositiveResultCoeficients = new List<double>();
        }

        public List<double> PositiveResultCoeficients { get; set; }

        public void CountCoeficients(IClasified clasified, IEnumerable<IClasified> samples, AprioreResult apriore)
        {
            if (clasified is Extusia)
            {
                var extusias = new List<Extusia>();
                foreach (var ext in samples)
                {
                    extusias.Add(ext as Extusia);
                }
                CountExtusia((Extusia)clasified, extusias, apriore);
            }
            else if (clasified is TelecommunicationClient)
            {
                var clients = new List<TelecommunicationClient>();
                foreach (var client in samples)
                {
                    clients.Add(client as TelecommunicationClient);
                }
                CountTelecommunicationClient((TelecommunicationClient)clasified, clients, apriore);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void CountTelecommunicationClient(TelecommunicationClient clasifiedClient, List<TelecommunicationClient> clients,
            AprioreResult apriore)
        {
            apriore.Models = clients;
            apriore.GetPositiveResult();
            var ageLimits = new List<Tuple<int, int>>()
            {
                {new Tuple<int, int>(0, 18) },
                {new Tuple<int, int>(18, 30) },
                {new Tuple<int, int>(30, 45) },
                {new Tuple<int, int>(45, 60) },
                {new Tuple<int, int>(60, Int32.MaxValue) }
            };
            var userUnitsLimits = new List<Tuple<int, int>>()
            {
                { new Tuple<int, int>(0, 100)},
                { new Tuple<int, int>(100, 500)},
                { new Tuple<int, int>(500, 2000)},
                { new Tuple<int, int>(2000, 5000)},
                { new Tuple<int, int>(5000, Int32.MaxValue)},
            };

            var sexCoeficient = (from c in clients
                                 where c.Sex == clasifiedClient.Sex &&
                                 c.ClasificationResult == true
                                 select c).Count() / apriore.PositiveResultCount;

            // Age
            var ageLimit = (from age in ageLimits
                            where clasifiedClient.Age >= age.Item1 &&
                            clasifiedClient.Age < age.Item2
                            select age).FirstOrDefault();

            var ageCoeficient = (from c in clients
                                 where c.Age >= ageLimit.Item1 &&
                                 c.Age < ageLimit.Item2 &&
                                 c.ClasificationResult == true
                                 select c).Count() / apriore.PositiveResultCount;

            var tariffCoeficient = (from c in clients
                                    where c.Tariff == clasifiedClient.Tariff &&
                                    c.ClasificationResult == true
                                    select c).Count() / apriore.PositiveResultCount;

            var userUnitsLimit = (from unitsLimit in userUnitsLimits
                                  where clasifiedClient.UsedUnits >= unitsLimit.Item1 &&
                                  clasifiedClient.UsedUnits < unitsLimit.Item2
                                  select unitsLimit).FirstOrDefault();

            var userUnitsCoeficient = (from c in clients
                                       where c.UsedUnits >= userUnitsLimit.Item1 &&
                                       c.UsedUnits < userUnitsLimit.Item2 &&
                                       c.ClasificationResult == true
                                       select c).Count() / apriore.PositiveResultCount;

            PositiveResultCoeficients.Add(sexCoeficient);
            PositiveResultCoeficients.Add(ageCoeficient);
            PositiveResultCoeficients.Add(tariffCoeficient);
            PositiveResultCoeficients.Add(userUnitsCoeficient);
        }

        private void CountExtusia(Extusia clasifiedExtusia, List<Extusia> extusias, AprioreResult apriore)
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
