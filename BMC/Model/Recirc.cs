using System.Collections.Generic;
using BMC.ViewModel;

namespace BMC.Model
{
    public class Recirc:Pins
    {
        #region Properties
        public string VentDisabled { get; set; }
        #endregion

        #region Get Methods
        public static List<int> Power() 
        {
            var result = new List<int> { 24,220 };
            return result;
        }
        /// <summary>
        /// + 2 TEMP AI (?) List[0]=AO,List[1]=DO,List[2]=AI,List[3]=DI
        /// </summary>
        /// <param name="recircVM"></param>
        /// <returns></returns>
        public List<int> GetPins(RecircViewModel recircVM)
        {
            DataClass recircControlData = recircVM.GetControlData();
            AO += 1;
            if (recircControlData.StringData[0] == "Да")
            {
                DO -= 1;
                DI -= 1;
            }
            List<int> result = new List<int> { AO, DO, AI, DI };
            return result;
        }
        public List<PowerObject> GetPower(RecircViewModel recircVM)
        {
            DataClass powerData = recircVM.GetPowerData();
            List<PowerObject> powerResult = GetDrivePower(powerData);
            return powerResult;
        }

        private List<PowerObject> GetDrivePower(DataClass data)
        {
            //тут обращение к бд
            if (data.IntData[0] == 24)
                return new List<PowerObject> { new PowerObject("TTR 60", 1) };
            else
                return null;           
        }
        #endregion
    }

}
