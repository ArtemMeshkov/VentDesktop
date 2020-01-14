using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.Model
{
    class PlateWithBypass
    {
        #region Properties

        public int Power{get;set;}

        #endregion

        #region Get Methods

        public static List<int> GetPower()
        {
            var result = new List<int> { 24, 220 };
            return result;
        }

        #endregion
    }
}
