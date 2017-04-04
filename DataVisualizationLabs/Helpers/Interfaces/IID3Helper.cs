using Models.Abstraction;
using System.Collections.Generic;

namespace Helpers.Interfaces
{
    public interface IID3Helper
    {
        IEnumerable<IClasified> DetermineGameCoeficients(IEnumerable<IClasified> clasified);
    }
}
