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
        public int PowerExhausted { get; set; }
        public int PowerForced { get; set; }
        public string ForcedChosen { get; set; }
        public string ExhaustedChosen { get; set; }
        public static List<int> GetPower()
        {
            var result = new List<int> { 24, 220 };
            return result;
        }
        /// <summary>
        /// List[0]=AO,List[1]=DO,List[2]=AI,List[3]=DI
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<int> GetPins(GatesViewModel p)
        {
            DataClass GatesControlResult = p.GetControlData();
            if (GatesControlResult.StringData[0] == "Да")
                DO += 1;
            if (GatesControlResult.StringData[1] == "Да")
                DO += 1;
            List<int> result = new List<int> { AO, DO, AI, DI };
            return result;
        }

        public List<PowerObject> GetPowerParts(GatesViewModel p)
        {
            List<PowerObject> GatePower = new List<PowerObject>();
            DataClass result = p.GetPowerData();
            if (result.IntData[0] != 0) {
                if (result.StringData[0] == "Двухпозиционное") { }
                else
                { }
                if (result.StringData[1] == "Да")
                {

                }
                else { }
            }
            if (result.IntData[1] != 0)
            {
                if (result.StringData[0] == "Двухпозиционное") { }
                else
                { }
                if (result.StringData[1] == "Да")
                {

                }
                else { }
            }

            return GatePower;
        }
    }
}
