using BMC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace BMC.Model
{
    public class ElectricModel
    {
        #region Properties

        public int NumOfStages { get;set; }
        public double PowerOfStage { get;  set; }
        public string FirstStage { get;  set; }
        public int ThermoSwitch { get; set; }

        #endregion

        #region Get Methods
        public static List<int> GetNumOfStages()
        {
            var result = new List<int> { 1,2,3,4,5,6,7,8 };
            return result;
        }
        public static List<int> GetThermoSwitches()
        {
            var result = new List<int> { 0,1,2};
            return result;
        }
        public static DoubleCollection GetPowerOfStages()
        {
            var result = new DoubleCollection { 4, 6, 9, 12, 15, 18, 21, 24, 26, 32, 36, 41 };
            return result;
        }
        #endregion

    }
}
