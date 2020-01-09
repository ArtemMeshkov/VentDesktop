using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.Model
{
    class PlateWithBypass
    {
        public int Power{get;set;}
        public static List<int> GetPower()
        {
            var result = new List<int> { 24, 220 };
            return result;
        }
    }
}
