using System.Collections.Generic;
using BMC.ViewModel;

namespace BMC.Model
{
    public class Cooler:Pins
    {
        #region Properties
        public string Dryer { get; set; }
        public string ExtraHeater { get; set; }
        public string Type { get; set; }
        public int NumOfStages{get;set;}
        public int PumpPower { get; set; }
        #endregion

        #region Get Methods
        public static List<string> GetTypes()
        {
            var result = new List<string> { "Водяной", "Фреоновый" };
            return result;
        }
        public static List<int> GetNumOfStages()
        {
            var result = new List<int> { 1, 2, 3, 4 };
            return result;
        }
        public static List<int> GetPumpPower()
        {
            var result = new List<int> { 24, 220 };
            return result;
        }
        /// <summary>
        /// List[0]=AO,List[1]=DO,List[2]=AI,List[3]=DI  //2 temp AI - standart(not enabled)(with recirc)
        /// </summary>
        /// <param name="coolerVM">CoolerViewModel</param>
        /// <returns></returns>
        public List<int> GetPins(CoolerViewModel coolerVM)
        {
            var heaterM = new Heater();
            var liquidM = new LiquidModel();
            var electricM = new ElectricModel();
            DataClass coolerControlData = coolerVM.GetControlData();
            Type = coolerControlData.StringData[0];
            Dryer = coolerControlData.StringData[1];
            if (Type == "Фреоновый")
            {
                AI += 1;                //Room temperature
                NumOfStages = coolerControlData.IntData[0];
                DO += NumOfStages;
            }
            else
            {
               AO += 1;   
            }
            if (Dryer == "Да")
                AI += 2;   //Добавляет датчики??
            ExtraHeater = coolerControlData.StringData[2];
            if (ExtraHeater == "Да")
            {
                heaterM.Type=coolerControlData.StringData[3];
                if (heaterM.Type == "Жидкостный")
                {
                    AO += 1;
                    AI += 1;
                    DO += 1;
                    liquidM.AirTemp = coolerControlData.StringData[4];
                    if (liquidM.AirTemp == "Да")
                        DI += 1;
                }
                else
                {
                    electricM.FirstStage = coolerControlData.StringData[4];
                    electricM.NumOfStages = coolerControlData.IntData[1]; 
                    electricM.ThermoSwitch = coolerControlData.IntData[2];
                    if (electricM.FirstStage == "Да")
                        AO += 1;
                    DO += electricM.NumOfStages;
                    DI += electricM.ThermoSwitch;
                }
            }
            var result= new List<int> { AO, DO, AI, DI };
            return result;
        }
        public List<PowerObject> GetPowerParts(CoolerViewModel coolerVM)
        {
            DataClass coolerControlData = coolerVM.GetControlData();
            DataClass coolerPowerData = coolerVM.GetPowerData();
            var result = new List<PowerObject>();
            ExtraHeater = coolerControlData.StringData[2];
            if (coolerControlData.StringData[0] == "Жидкостный")
            {
                result.AddRange(GetVent(coolerPowerData));
            }
            else
            {//Тут зависимость фреонового охладителя от количества ступеней, как оформить? написать мощность? 
            }
            if(coolerVM.ExtraHeaterChecked==true)
            {
                if (coolerControlData.StringData[0] == "Жидкостный")
                {
                    result.AddRange(GetAuto(coolerControlData));
                    result.AddRange(GetVent(coolerControlData));
                    if (coolerControlData.StringData[0] == "Да")
                    { //что-то для термостата?
                    }
                }
                else
                {
                    if (coolerControlData.StringData[0] == "Да")
                        result.AddRange(GetPCH(coolerControlData));
                    else
                        result.AddRange(GetAuto(coolerControlData));
                }
                if (ExtraHeater == "Да") //доп нагреватель
                {
                    if (coolerControlData.StringData[3] == "Жидкостный")
                    {
                        result.AddRange(GetAuto(coolerControlData));
                        result.AddRange(GetVent(coolerControlData));
                        if (coolerControlData.StringData[4] == "Да")
                        { //что-то для термостата?
                        }
                    }
                    else
                    {
                        if (coolerControlData.StringData[4] == "Да")
                            result.AddRange(GetPCH(coolerControlData));
                        else
                            result.AddRange(GetAuto(coolerControlData));
                    }
                }
            }

                return null;
        }
        #endregion

        #region Database    
        private List<PowerObject> GetAuto(DataClass p)
        {
            //снять автоматы с бд 220/380
            return null;
        }
        private List<PowerObject> GetVent(DataClass p)
        {
            //снять для клапанов 24/220 с бд
            return null;
        }
        private List<PowerObject> GetPCH(DataClass p)
        {
            //снять пч+автоматы с бд(atv)?
            return null;
        }
        #endregion
    }
}
