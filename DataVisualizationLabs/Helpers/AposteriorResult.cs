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

            var sexCoeficient = (from c in clients
                                 where c.Sex == clasifiedClient.Sex &&
                                 c.ClasificationResult == true
                                 select c).Count() / apriore.PositiveResultCount;

            var ageCoeficient = (from c in clients
                                 where c.Age == clasifiedClient.Age &&
                                 c.ClasificationResult == true
                                 select c).Count() / apriore.PositiveResultCount;

            var tariffCoeficient = (from c in clients
                                    where c.Tariff == clasifiedClient.Tariff &&
                                    c.ClasificationResult == true
                                    select c).Count() / apriore.PositiveResultCount;

            var userUnitsCoeficient = (from c in clients
                                       where c.UsedUnits == clasifiedClient.UsedUnits &&
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
