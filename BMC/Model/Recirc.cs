using System.Collections.Generic;
using BMC.ViewModel;

namespace BMC.Model
{
    public class Recirc:Pins
    {
        public string VentDisabled { get; set; }
        public static List<int> Power() 
        {
            var result = new List<int> { 24,220 };
            return result;
        }
        /// <summary>
        /// + 2 TEMP AI (?) List[0]=AO,List[1]=DO,List[2]=AI,List[3]=DI
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<int> GetPins(RecircViewModel p)
        {
            DataClass RecircControlResult = p.GetControlData();
            AO += 1;
            if (RecircControlResult.StringData[0] == "Да")
            {
                DO -= 1;
                DI -= 1;
            }
            List<int> result = new List<int> { AO, DO, AI, DI };
            return result;
        }
        public List<PowerObject> GetPower(RecircViewModel p,List<PowerObject> FinalList)
        {
            DataClass result = p.GetPowerData();
            List<PowerObject> PowerResult = new List<PowerObject>();
            PowerResult = GetDrivePower(result);
            return PowerResult;
        }

        private List<PowerObject> GetDrivePower(DataClass data)
        {
            //тут обращение к бд
            if (data.IntData[0] == 24)
                return new List<PowerObject> { new PowerObject("TTR 60", 1) };
            else
                return null;
           
        }
    }

}
