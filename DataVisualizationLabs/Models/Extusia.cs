using Enums;
using Models.Abstraction;

namespace Models
{
    public class Extusia : IClasified
    {
        public Extusia(int rpm, int resorseConsumptionPerHour, Density density, int granulaSize,
            int temperature)
        {
            RPM = rpm;
            ResourceConsumptiongPerHour = resorseConsumptionPerHour;
            Density = density;
            GranulaSize = granulaSize;
            Temperature = temperature;
        }

        public Extusia(int rpm, int resorseConsumptionPerHour, Density density, int granulaSize,
            int temperature, bool clasificationResult) : this(rpm, resorseConsumptionPerHour, density,
                granulaSize, temperature)
        {
            ClasificationResult = clasificationResult;
        }

        public int RPM { get; set; }
        public int ResourceConsumptiongPerHour { get; set; }
        public Density Density { get; set; }
        public int GranulaSize { get; set; }
        public int Temperature { get; set; }
        public bool? ClasificationResult { get; set; }
    }
}
