using BMC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.Model
{
    public class HeatExchange:Pins
    {
        #region Properties
        public string Type { get; set; }
        public string TempSensor { get; set; }
        public string PressureSensor { get; set; }
        #endregion

        #region Get Methods
        public static List<string> GetTypes()
        {
            var result = new List<string> { "Роторный", "Пластинчатый", "Пластинчатый с рекуператором", "Гликолевый" };
            return result;
        }

        /// <summary>
        /// List[0]-AO,List[1]-DO,List[2]-AI,List[3]-DI
        /// </summary>
        public List<int> GetPins(HeatExchangeViewModel heatExchangeVM)
        {
            Glycol glycolM = new Glycol();
            Rotor rotorM = new Rotor();
           // PlateWithBypass plateBP = new PlateWithBypass();
            DataClass heatExchangeControlData = heatExchangeVM.GetControlData();
            Type = heatExchangeControlData.StringData[0];
            TempSensor = heatExchangeControlData.StringData[1];
            PressureSensor = heatExchangeControlData.StringData[2];
            if (TempSensor == "Да") AI += 1;
            if (PressureSensor == "Да") DI += 1;
            if (Type == "Роторный")
            {
                AO += 1; DO += 1; DI += 1; AI += 2;                            //standart speed regulation 0-10V + 2Temp sensors in channels
                rotorM.SpeedSignal = heatExchangeControlData.StringData[3];
                if (rotorM.SpeedSignal == "MODBUS RTU")
                {
                    AO -= 1; DO -= 1; DI -= 1;
                }
            }
            else if (Type == "Пластинчатый с рекуператором") ;
            else if (Type == "Гликолевый")
            {
                DO += 1;
                glycolM.VentStatus = heatExchangeControlData.StringData[3];
                if (glycolM.VentStatus == "Да")
                {
                    AO += 1;
                    AI += 2;
                }
            }            
            List<int> result = new List<int> { AO, DO, AI, DI };
            return result;
        }

        public List<PowerObject> GetPowerParts(HeatExchangeViewModel heatExchangerVM)
        {
            DataClass heatExchangeControlData = heatExchangerVM.GetControlData();
            DataClass heatExchangePowerData = heatExchangerVM.GetPowerData();
            List<PowerObject> heatExchangeResult = new List<PowerObject>();
            if (heatExchangeControlData.StringData[0] == "Роторный")
            {
                if (heatExchangePowerData.StringData[1] == "Частотный преобразователь")
                {
                    heatExchangeResult.AddRange(GetPCH(heatExchangePowerData));
                    if (heatExchangePowerData.StringData[2] == "0-10 В")
                    {
                        //organize 0-10В
                    }
                    else
                    {
                        //MODBUS RTU?
                    }
                }
                else heatExchangeResult.AddRange(GetAuto(heatExchangePowerData));
            }
            else if (heatExchangeControlData.StringData[0] == "Гликолевый")
            {
                if (heatExchangeControlData.StringData[3] == "Да")
                {
                    //привод 24/220 бд
                }
                heatExchangeResult.AddRange(GetAuto(heatExchangePowerData));
            }
            else if(heatExchangeControlData.StringData[0]=="Пластинчатый с байпасом")
            {
                //24/220 - бд?
            }
            return null;
        }
        #endregion

        #region Database
        private List<PowerObject> GetPCH(DataClass PCH)
        {
            //считать с бд выбор пч 
            return null;
        }

        private List<PowerObject> GetAuto(DataClass PCH)
        {
            //считать с бд автоматы и контакты
            return null;
        }
        #endregion
    }
}
