using BMC.ViewModel;
using System.Collections.Generic;

namespace BMC.Model
{
    public class Humid:Pins
    {
        #region Properties
        public string Type { get; set; }
        #endregion

        #region GetMethods
        public static List<string> GetTypes()
        {
            var result = new List<string> { "Паровой", "Сотовый" };
            return result;

        }

        /// <summary>
        /// List[0]=AO,List[1]=DO,List[2]=AI,List[3]=DI
        /// </summary>
        /// <param name="humidVM"></param>
        /// <returns></returns>
        public List<int> GetPins(HumidViewModel humidVM)
        {
            HoneyCombsModel honeyCombsM = new HoneyCombsModel();
            DataClass humidControlData = humidVM.GetControlData();
            Type = humidControlData.StringData[0];
            AI += 2;   //humid sensors
            if(Type=="Сотовый")
            {
                DO += 1;
                honeyCombsM.WaterType = humidControlData.StringData[1];
                honeyCombsM.ByPass = humidControlData.StringData[2];
                honeyCombsM.NumOfStages = humidControlData.IntData[0];
                if (honeyCombsM.ByPass == "Да") AO += 1;
                if (honeyCombsM.WaterType == "Оборотная")
                {
                    honeyCombsM.InWater = humidControlData.StringData[3];
                    honeyCombsM.OutWater = humidControlData.StringData[4];
                    honeyCombsM.LevelCheck = humidControlData.StringData[5];
                    if (honeyCombsM.InWater == "Да") DO += 1;
                    if (honeyCombsM.OutWater == "Да") DO += 1;
                    if (honeyCombsM.LevelCheck == "Да") DI += 2;
                    DO += (honeyCombsM.NumOfStages-1);
                }
                else DO += honeyCombsM.NumOfStages;
            }
            else
            {
                AO += 1;
                DO += 1;
            }
            List<int> result = new List<int> { AO, DO, AI, DI };
            return result;
        }
        
        public List<PowerObject> GetPowerParts(HumidViewModel humidVM)
        {
            DataClass humidPowerData = humidVM.GetPowerData();
            List<PowerObject> humidPower = new List<PowerObject>();
            //метод управления или выбора автомата/трансформатора для клапана?
            return humidPower;
        }
        #endregion
    }
}
