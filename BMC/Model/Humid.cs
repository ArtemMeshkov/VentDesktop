using BMC.ViewModel;
using System.Collections.Generic;

namespace BMC.Model
{
    public class Humid:Pins
    {
        public string Type { get; set; }
        public static List<string> GetTypes()
        {
            var result = new List<string>();
            result.Add("Паровой");
            result.Add("Сотовый");
            return result;

        }
        /// <summary>
        /// List[0]=AO,List[1]=DO,List[2]=AI,List[3]=DI
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<int> GetPins(HumidViewModel p)
        {
            HoneyCombsModel HoneyCombsM = new HoneyCombsModel();
            DataClass HumidControlResult = p.GetControlData();
            Type = HumidControlResult.StringData[0];
            AI += 2;   //humid sensors
            if(Type=="Сотовый")
            {
                DO += 1;
                HoneyCombsM.WaterType = HumidControlResult.StringData[1];
                HoneyCombsM.ByPass = HumidControlResult.StringData[2];
                HoneyCombsM.NumOfStages = HumidControlResult.IntData[0];
                if (HoneyCombsM.ByPass == "Да") AO += 1;
                if (HoneyCombsM.WaterType == "Оборотная")
                {
                    HoneyCombsM.InWater = HumidControlResult.StringData[3];
                    HoneyCombsM.OutWater = HumidControlResult.StringData[4];
                    HoneyCombsM.LevelCheck = HumidControlResult.StringData[5];
                    if (HoneyCombsM.InWater == "Да") DO += 1;
                    if (HoneyCombsM.OutWater == "Да") DO += 1;
                    if (HoneyCombsM.LevelCheck == "Да") DI += 2;
                    DO += (HoneyCombsM.NumOfStages-1);
                }
                else DO += HoneyCombsM.NumOfStages;
            }
            else
            {
                AO += 1;
                DO += 1;
            }
            List<int> result = new List<int> { AO, DO, AI, DI };
            return result;
        }


        public List<PowerObject> GetPowerParts(HumidViewModel p)
        {
            DataClass result = p.GetPowerData();
            List<PowerObject> HumidResult = new List<PowerObject>();
            //метод управления или выбора автомата/трансформатора для клапана?
            return HumidResult;
        }
    }
}
