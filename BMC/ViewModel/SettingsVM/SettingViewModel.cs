 using BMC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.ViewModel
{
    public class SettingViewModel : BaseViewModel
    {
        #region Properties
        public bool? PhaseCheck { get; set; }
        public bool? OperatorPanel { get; set; }
        public bool? TempOut { get; set; }
        public bool? TempIn { get; set; }
        public bool? CO2 { get; set; }
        public List<string> CO2Type { get; set; }
        public string SelectedType { get; set; }
        #endregion

        #region Constructor
        public SettingViewModel()
        {
            CO2Type = SettingModel.GetTypes();
            PhaseCheck = false;
            OperatorPanel = false;
            TempOut = false;
            TempIn = true;
            CO2 = false;
            SelectedType = CO2Type[0];
        }
        #endregion

        #region Data Methods
        /// <summary>
        /// StringData[0] - relay control, StringData[1]- operator panel check, StringData[2] - sensor of outter temp, StringData[3] - sensor of input temp,
        /// StringData[4] - CO2 sensor, StringData[5] - type of CO2,"Нет" if not chosen
        /// </summary>
        /// <returns></returns>
        public override DataClass GetControlData()
        {
            DataClass result = new DataClass();
            result.StringData.Add(BoolToStringConverter.BTS((bool)PhaseCheck));
            result.StringData.Add(BoolToStringConverter.BTS((bool)OperatorPanel));
            result.StringData.Add(BoolToStringConverter.BTS((bool)TempOut));
            result.StringData.Add(BoolToStringConverter.BTS((bool)TempIn));
            result.StringData.Add(BoolToStringConverter.BTS((bool)CO2));
            if (CO2 == true)
                result.StringData.Add(SelectedType);
            else
                result.StringData.Add("Нет");
            return result;
        }

        public override DataClass GetPowerData()
        {
            return base.GetPowerData();
        }

        #endregion
    }
}
