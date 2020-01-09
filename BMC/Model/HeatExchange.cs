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
        public string Type { get; set; }
        public string TempSensor { get; set; }
        public string PressureSensor { get; set; }

        public static List<string> GetTypes()
        {
            var result = new List<string> { "Роторный", "Пластинчатый", "Пластинчатый с рекуператором", "Гликолевый" };
            return result;
        }
        /// <summary>
        /// List[0]-AO,List[1]-DO,List[2]-AI,List[3]-DI
        /// </summary>
        public List<int> GetPins(HeatExchangeViewModel bm)
        {
            Glycol GlycolM = new Glycol();
            Rotor RotorM = new Rotor();
            PlateWithBypass PlateBPM = new PlateWithBypass();
            DataClass HeatExchangeControlResult = bm.GetControlData();
            Type = HeatExchangeControlResult.StringData[0];
            TempSensor = HeatExchangeControlResult.StringData[1];
            PressureSensor = HeatExchangeControlResult.StringData[2];
            if (TempSensor == "Да") AI += 1;
            if (PressureSensor == "Да") DI += 1;
            if (Type == "Роторный")
            {
                AO += 1; DO += 1; DI += 1; AI += 2;                            //standart speed regulation 0-10V + 2Temp sensors in channels
                RotorM.SpeedSignal = HeatExchangeControlResult.StringData[3];
                if (RotorM.SpeedSignal == "MODBUS RTU")
                {
                    AO -= 1; DO -= 1; DI -= 1;
                }
            }
            else if (Type == "Пластинчатый с рекуператором") ;
            else if (Type == "Гликолевый")
            {
                DO += 1;
                GlycolM.VentStatus = HeatExchangeControlResult.StringData[3];
                if (GlycolM.VentStatus == "Да")
                {
                    AO += 1;
                    AI += 2;
                }
            }
            
            List<int> result = new List<int> { AO, DO, AI, DI };
            return result;
        }

        public List<PowerObject> GetPowerParts(HeatExchangeViewModel p)
        {
            DataClass HeatExchangeControlResult = p.GetControlData();
            DataClass HeatExchangePowerResult = p.GetPowerData();
            List<PowerObject> HeatExchangeResult = new List<PowerObject>();
            if (HeatExchangeControlResult.StringData[0] == "Роторный")
            {
                if (HeatExchangePowerResult.StringData[1] == "Частотный преобразователь")
                {
                    HeatExchangeResult.AddRange(GetPCH(HeatExchangePowerResult));
                    if (HeatExchangePowerResult.StringData[2] == "0-10 В")
                    {
                        //organize 0-10В
                    }
                    else
                    {
                        //MODBUS RTU?
                    }
                }
                else HeatExchangeResult.AddRange(GetAuto(HeatExchangePowerResult));
            }
            else if (HeatExchangeControlResult.StringData[0] == "Гликолевый")
            {
                if (HeatExchangeControlResult.StringData[3] == "Да")
                {
                    //привод 24/220 бд
                }
                HeatExchangeResult.AddRange(GetAuto(HeatExchangePowerResult));
            }
            else if(HeatExchangeControlResult.StringData[0]=="Пластинчатый с байпасом")
            {
                //24/220 - бд?
            }
            return null;
        }

        private List<PowerObject> GetPCH(DataClass p)
        {
            //считать с бд выбор пч 
            return null;
        }

        private List<PowerObject> GetAuto(DataClass p)
        {
            //считать с бд автоматы и контакты
            return null;
        }
    }
}
