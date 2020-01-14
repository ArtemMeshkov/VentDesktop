using System.Collections.Generic;
using System.Linq;
using BMC.ViewModel;
namespace BMC.Model
{
    public class Heater: Pins
    {
        #region Properties
        public string Type { get ; set; }
        public string ExtraType { get; set; }
        #endregion

        #region Get Methods
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
        public List<int> GetPins(HeaterViewModel heaterVM)
        {
            LiquidModel liquidM = new LiquidModel();
            ElectricModel electricM = new ElectricModel();
            LiquidModel extraLiquid = new LiquidModel();
            ElectricModel extraElectric = new ElectricModel();
            DataClass heaterControlData = heaterVM.GetControlData();
            Type = heaterControlData.StringData[0];
            if (Type == "Жидкостный")
            {
                AO += 1;
                DO += 1;
                AI += 1;
                liquidM.AirTemp = heaterControlData.StringData[1];
                if (liquidM.AirTemp == "Да")
                    DI += 1;
            }
            else
            {
                electricM.FirstStage = heaterControlData.StringData[1];
                electricM.NumOfStages = heaterControlData.IntData[0];
                electricM.ThermoSwitch = heaterControlData.IntData[1];
                if (electricM.FirstStage == "Да")
                    AO += 1;
                DO += electricM.NumOfStages;
                DI += electricM.ThermoSwitch; 
            }
            if (heaterVM.ExtraHeater == true)
            {
                ExtraType = heaterControlData.StringData[2];
                if (ExtraType == "Жидкостный")
                {
                    AO += 1;
                    DO += 1;
                    AI += 1;
                    extraLiquid.AirTemp = heaterControlData.StringData[3];
                    if (extraLiquid.AirTemp == "Да")
                        DI += 1;
                }
                else
                {
                    if (Type != "Жидкостный")
                    {
                        extraElectric.FirstStage = heaterControlData.StringData[3];
                        extraElectric.NumOfStages = heaterControlData.IntData[2];
                        extraElectric.ThermoSwitch = heaterControlData.IntData[3];
                        if (electricM.FirstStage == "Да")
                            AO += 1;
                        DO += extraElectric.NumOfStages;
                        DI += extraElectric.ThermoSwitch;
                    }
                    else
                    {
                        extraElectric.FirstStage = heaterControlData.StringData[3];
                        extraElectric.NumOfStages = heaterControlData.IntData[0];
                        extraElectric.ThermoSwitch = heaterControlData.IntData[1];
                        if (electricM.FirstStage == "Да")
                            AO += 1;
                        DO += extraElectric.NumOfStages;
                        DI += extraElectric.ThermoSwitch;
                    }

                }
            }
            List<int> result = new List<int> { AO, DO, AI, DI };
            return result;
        }

        public List<PowerObject> GetPowerParts(HeaterViewModel heaterVM)
        {
            DataClass heaterControlData = heaterVM.GetControlData();
            DataClass heaterPowerData = heaterVM.GetPowerData();
            List<PowerObject> result = new List<PowerObject>();
            if (heaterControlData.StringData[0] == "Жидкостный")
            {
                result.AddRange(GetAuto(heaterPowerData));
                result.AddRange(GetVent(heaterPowerData));
                if (heaterPowerData.StringData[0] == "Да") { //что-то для термостата?
                }
                
            }
            else
            {
                if (heaterPowerData.StringData[0] == "Да")
                    result.AddRange(GetPCH(heaterPowerData));
                else
                    result.AddRange(GetAuto(heaterPowerData));
            }
            if (heaterVM.ExtraHeater == true)
            {
                if (heaterControlData.StringData[1] == "Жидкостный")
                {
                    result.AddRange(GetAuto(heaterPowerData));
                    result.AddRange(GetVent(heaterPowerData));
                    if (heaterPowerData.StringData[1] == "Да")
                    { //что-то для термостата?
                    }

                }
                else
                {
                    if (heaterPowerData.StringData[1] == "Да")
                        result.AddRange(GetPCH(heaterPowerData));
                    else
                        result.AddRange(GetAuto(heaterPowerData));
                }
            }
            return result;
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
