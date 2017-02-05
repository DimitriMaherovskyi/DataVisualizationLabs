using Models;
using System.Collections.Generic;
using System.Linq;

namespace Helpers
{
    public class AprioreResult
    {
        public double PositiveResultCount { get; set; }
        public double PositiveResultCoeficient { get; set; }

        public IEnumerable<Extusia> Models { get; set; }

        public void GetPositiveResult()
        {
            var count = (from m in Models
                         where m.Stopper == true
                         select m).Count();

            PositiveResultCount = count;
            GetPositiveResultCoeficient();
        }

        private void GetPositiveResultCoeficient()
        {
            PositiveResultCoeficient = PositiveResultCount / Models.Count();
        }
    }
}
