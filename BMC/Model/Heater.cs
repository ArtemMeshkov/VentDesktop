using System.Collections.Generic;
using System.Linq;
using BMC.ViewModel;
namespace BMC.Model
{
    public class Heater: Pins
    {
        public string Type { get ; set; }
        public string ExtraType { get; set; }
        //private LiquidModel LiquidM { get; set; }
        //private ElectricModel ElectricM { get; set; }
        public static List<string> GetHeater()
        {
            List<string> result = new List<string>();
            result.Add("Жидкостный");
            result.Add("Электрический");
            return result;
        }
        /// <summary>
        /// List[0]-AO,List[1]-DO,List[2]-AI,List[3]-DI(+AI with /no recirc 2/1)
        /// </summary>
        public List<int> GetPins(HeaterViewModel p)
        {
            LiquidModel LiquidM = new LiquidModel();
            ElectricModel ElectricM = new ElectricModel();
            LiquidModel ExtraLiquid = new LiquidModel();
            ElectricModel ExtraElectric = new ElectricModel();
            DataClass HeaterControlData = p.GetControlData();
            Type = HeaterControlData.StringData[0];
            if (Type == "Жидкостный")
            {
                AO += 1;
                DO += 1;
                AI += 1;
                LiquidM.AirTemp = HeaterControlData.StringData[1];
                if (LiquidM.AirTemp == "Да")
                    DI += 1;
            }
            else
            {
                ElectricM.FirstStage = HeaterControlData.StringData[1];
                ElectricM.NumOfStages = HeaterControlData.IntData[0];
                ElectricM.ThermoSwitch = HeaterControlData.IntData[1];
                if (ElectricM.FirstStage == "Да")
                    AO += 1;
                DO += ElectricM.NumOfStages;
                DI += ElectricM.ThermoSwitch; 
            }
            if (p.ExtraHeater == true)
            {
                ExtraType = HeaterControlData.StringData[2];
                if (ExtraType == "Жидкостный")
                {
                    AO += 1;
                    DO += 1;
                    AI += 1;
                    ExtraLiquid.AirTemp = HeaterControlData.StringData[3];
                    if (ExtraLiquid.AirTemp == "Да")
                        DI += 1;
                }
                else
                {
                    if (Type != "Жидкостный")
                    {
                        ExtraElectric.FirstStage = HeaterControlData.StringData[3];
                        ExtraElectric.NumOfStages = HeaterControlData.IntData[2];
                        ExtraElectric.ThermoSwitch = HeaterControlData.IntData[3];
                        if (ElectricM.FirstStage == "Да")
                            AO += 1;
                        DO += ExtraElectric.NumOfStages;
                        DI += ExtraElectric.ThermoSwitch;
                    }
                    else
                    {
                        ExtraElectric.FirstStage = HeaterControlData.StringData[3];
                        ExtraElectric.NumOfStages = HeaterControlData.IntData[0];
                        ExtraElectric.ThermoSwitch = HeaterControlData.IntData[1];
                        if (ElectricM.FirstStage == "Да")
                            AO += 1;
                        DO += ExtraElectric.NumOfStages;
                        DI += ExtraElectric.ThermoSwitch;
                    }

                }
            }
            List<int> result = new List<int> { AO, DO, AI, DI };
            return result;
        }

        public List<PowerObject> GetPowerParts(HeaterViewModel p)
        {
            DataClass HeaterControlData = p.GetControlData();
            DataClass HeaterPowerData = p.GetPowerData();
            List<PowerObject> Result = new List<PowerObject>();
            if (HeaterControlData.StringData[0] == "Жидкостный")
            {
                Result.AddRange(GetAuto(HeaterPowerData));
                Result.AddRange(GetVent(HeaterPowerData));
                if (HeaterPowerData.StringData[0] == "Да") { //что-то для термостата?
                }
                
            }
            else
            {
                if (HeaterPowerData.StringData[0] == "Да")
                    Result.AddRange(GetPCH(HeaterPowerData));
                else
                    Result.AddRange(GetAuto(HeaterPowerData));
            }
            if (p.ExtraHeater == true)
            {
                if (HeaterControlData.StringData[1] == "Жидкостный")
                {
                    Result.AddRange(GetAuto(HeaterPowerData));
                    Result.AddRange(GetVent(HeaterPowerData));
                    if (HeaterPowerData.StringData[1] == "Да")
                    { //что-то для термостата?
                    }

                }
                else
                {
                    if (HeaterPowerData.StringData[1] == "Да")
                        Result.AddRange(GetPCH(HeaterPowerData));
                    else
                        Result.AddRange(GetAuto(HeaterPowerData));
                }
            }
            return Result;
        }

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
    }
}
