using System;
using System.Collections.Generic;
using Models.Abstraction;
using Models;
using Helpers.Interfaces;
using Helpers.ID3Helpers;

namespace Helpers
{
    public class ID3ResultSet
    {
        private List<IClasified> _clasified;
        private IID3Helper _resultSetHelper;

        public ID3ResultSet(IEnumerable<IClasified> clasified)
        {
            _clasified = new List<IClasified>(clasified);
        }

        /// <summary>
        /// Returns minimized set, where all expected results == true.
        /// </summary>
        public IEnumerable<IClasified> DetermineMinimizedResultSet()
        {
            var result = new List<IClasified>();

            if (_clasified[0] is Game)
            {
                _resultSetHelper = new GameResultSetHelper();
                result = new List<IClasified>(_resultSetHelper.DetermineGameCoeficients(_clasified));
            }
            else
            {
                throw new NotImplementedException();
            }

            return result;
        }
    }
}
