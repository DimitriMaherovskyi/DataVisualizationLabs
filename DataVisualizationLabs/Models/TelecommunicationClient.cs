using Models.Abstraction;
using Enums;

namespace Models
{
    public class TelecommunicationClient : IClasified
    {
        public TelecommunicationClient(Sex sex, int age, Tariff tariff,
            int usedUnits)
        {
            Sex = sex;
            Age = age;
            Tariff = tariff;
            UsedUnits = usedUnits;
        }

        public TelecommunicationClient(Sex sex, int age, Tariff tariff,
            int usedUnits, bool clasificationResult) : this (sex, age, tariff, usedUnits)
        {
            ClasificationResult = clasificationResult;
        }

        public Sex Sex { get; set; }
        public int Age { get; set; }
        public Tariff Tariff { get; set; }
        public int UsedUnits { get; set; }
        public bool? ClasificationResult { get; set; }
    }
}
