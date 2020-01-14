using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMC.ViewModel;
namespace BMC.Model
{
    public class SettingModel : Pins
    {
        #region Properties
        public bool? PhaseCheck { get; set; }
        public bool? OperatorPanel { get; set; }
        public bool? TempOut { get; set; }
        public bool? TempIn { get; set; }
        public bool? CO2 { get; set; }
        public string SelectedType { get; set; }
        #endregion

        #region Get Methods
        public static List<string> GetTypes()
        {
            List<string> result = new List<string> { "Комнатный", "Канальный" };
            return result;
        }
        public List<int> GetPins(SettingViewModel settingVM)
        {
            CO2 = settingVM.CO2;
            PhaseCheck = settingVM.PhaseCheck;
            TempOut = settingVM.TempOut;
            TempIn = settingVM.TempIn;
            OperatorPanel = settingVM.OperatorPanel;
            if (CO2 == true)
                AI += 1;
            if (PhaseCheck == true)             //??????
                AI += 1;
            if (TempIn == true)
                AI += 1;
            if (TempOut == true)
                AI += 1;
            if (OperatorPanel == true)
                DI += 1;
            List<int> result = new List<int> { AO, DO, AI, DI };
            return result;
        }
        public List<string> GetInfo()
        {
            var result = new List<string>();
            if(BoolToStringConverter.BTS((bool)CO2)=="Да")
            {
                result.Add("Датчик CO2");
            }
            return result;
        }
        #endregion
    }
}
