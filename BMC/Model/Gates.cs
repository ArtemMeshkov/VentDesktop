using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMC.ViewModel;

namespace BMC.Model
{
    public class Gates:Pins
    {
        #region Properties
        public int PowerExhausted { get; set; }
        public int PowerForced { get; set; }
        public string ForcedChosen { get; set; }
        public string ExhaustedChosen { get; set; }
        #endregion

        #region Get Methods
        public static List<int> GetPower()
        {
            var result = new List<int> { 24, 220 };
            return result;
        }
        /// <summary>
        /// List[0]=AO,List[1]=DO,List[2]=AI,List[3]=DI
        /// </summary>
        /// <param name="gateVM"></param>
        /// <returns></returns>
        public List<int> GetPins(GatesViewModel gateVM)
        {
            DataClass gateControlData = gateVM.GetControlData();
            if (gateControlData.StringData[0] == "Да")
                DO += 1;
            if (gateControlData.StringData[1] == "Да")
                DO += 1;
            List<int> result = new List<int> { AO, DO, AI, DI };
            return result;
        }

        public List<PowerObject> GetPowerParts(GatesViewModel gateVM)
        {
            List<PowerObject> gatePower = new List<PowerObject>();
            DataClass gatePowerData = gateVM.GetPowerData();
            if (gatePowerData.IntData[0] != 0) {
                if (gatePowerData.StringData[0] == "Двухпозиционное") { }
                else
                { }
                if (gatePowerData.StringData[1] == "Да")
                {

                }
                else { }
            }
            if (gatePowerData.IntData[1] != 0)
            {
                if (gatePowerData.StringData[0] == "Двухпозиционное") { }
                else
                { }
                if (gatePowerData.StringData[1] == "Да")
                {

                }
                else { }
            }

            return gatePower;
        }
        #endregion
    }
}
