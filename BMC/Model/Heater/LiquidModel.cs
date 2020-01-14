using System.Collections.Generic;
using System.Linq;

namespace BMC.Model
{
    public class LiquidModel
    {
        #region Properties

        public int PumpPower { get; set; }
        public int ValvePower { get; set; }
        public string AirTemp { get; set; }

        #endregion

        #region Get Methods

        public static List<int> GetPumpPower()
        {
            var result = new List<int> { 220, 380 };
            return result;
        }
        public static List<int> GetVentPower()
        {
            var result = new List<int> { 24, 220 };
            return result;
        }

        #endregion
    }

}
