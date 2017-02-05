using Enums;

namespace Models
{
    public class Extusia
    {
        public int RPM { get; set; }
        public int ResourceConsumptiongPerHour { get; set; }
        public Density Density { get; set; }
        public int GranulaSize { get; set; }
        public int Temperature { get; set; }
        public bool? Stopper { get; set; }
    }
}
