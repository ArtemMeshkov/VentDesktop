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
        /// <param name="p">CoolerViewModel</param>
        /// <returns></returns>
        public List<int> GetPins(CoolerViewModel p)
        {
            Heater HeaterM = new Heater();
            LiquidModel LiquidM = new LiquidModel();
            ElectricModel ElectricM = new ElectricModel();
            DataClass CoolerControlResult = p.GetControlData();
            Type = CoolerControlResult.StringData[0];
            Dryer = CoolerControlResult.StringData[1];
            if (Type == "Фреоновый")
            {
                AI += 1;                //Room temperature
                NumOfStages = CoolerControlResult.IntData[0];
                DO += NumOfStages;
            }
            else
            {
               AO += 1;   
            }
            if (Dryer == "Да")
                AI += 2;   //Добавляет датчики??
            ExtraHeater = CoolerControlResult.StringData[2];
            if (ExtraHeater == "Да")
            {
                HeaterM.Type=CoolerControlResult.StringData[3];
                if (HeaterM.Type == "Жидкостный")
                {
                    AO += 1;
                    AI += 1;
                    DO += 1;
                    LiquidM.AirTemp = CoolerControlResult.StringData[4];
                    if (LiquidM.AirTemp == "Да")
                        DI += 1;
                }
                else
                {
                    ElectricM.FirstStage = CoolerControlResult.StringData[4];
                    ElectricM.NumOfStages = CoolerControlResult.IntData[1];
                    ElectricM.ThermoSwitch = CoolerControlResult.IntData[2];
                    if (ElectricM.FirstStage == "Да")
                        AO += 1;
                    DO += ElectricM.NumOfStages;
                    DI += ElectricM.ThermoSwitch;
                }
            }
            List<int> result = new List<int> { AO, DO, AI, DI };
            return result;
        }
        public List<PowerObject> GetPowerParts(CoolerViewModel p)
        {
            DataClass CoolerControlResult = p.GetControlData();
            DataClass CoolerPowerResult = p.GetPowerData();
            List<PowerObject> Result = new List<PowerObject>();
            ExtraHeater = CoolerControlResult.StringData[2];
            if (CoolerControlResult.StringData[0] == "Жидкостный")
            {
                Result.AddRange(GetVent(CoolerPowerResult));
            }
            else
            {//Тут зависимость фреонового охладителя от количества ступеней, как оформить? написать мощность? 
            }
            if(p.ExtraHeaterChecked==true)
            {
                if (CoolerControlResult.StringData[0] == "Жидкостный")
                {
                    Result.AddRange(GetAuto(CoolerControlResult));
                    Result.AddRange(GetVent(CoolerControlResult));
                    if (CoolerControlResult.StringData[0] == "Да")
                    { //что-то для термостата?
                    }
                }
                else
                {
                    if (CoolerControlResult.StringData[0] == "Да")
                        Result.AddRange(GetPCH(CoolerControlResult));
                    else
                        Result.AddRange(GetAuto(CoolerControlResult));
                }
                if (ExtraHeater == "Да") //доп нагреватель
                {
                    if (CoolerControlResult.StringData[3] == "Жидкостный")
                    {
                        Result.AddRange(GetAuto(CoolerControlResult));
                        Result.AddRange(GetVent(CoolerControlResult));
                        if (CoolerControlResult.StringData[4] == "Да")
                        { //что-то для термостата?
                        }
                    }
                    else
                    {
                        if (CoolerControlResult.StringData[4] == "Да")
                            Result.AddRange(GetPCH(CoolerControlResult));
                        else
                            Result.AddRange(GetAuto(CoolerControlResult));
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
