using Clasification.Abstraction;
using Helpers;
using Models;
using Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clasification
{
    public class ID3Clasifier : IClasifier
    {
        private ID3ResultSet _resultSet;

        public ID3Clasifier(ID3ResultSet resultSet)
        {
            _resultSet = resultSet;
        }

        public void Clasify()
        {
            var minimizedClacified = new List<IClasified>(_resultSet.DetermineMinimizedResultSet());
            if (minimizedClacified is IEnumerable<Game>)
            {
                ClasifyGame(minimizedClacified as IEnumerable<Game>);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void ClasifyGame(IEnumerable<Game> games)
        {

        }
    }
}
