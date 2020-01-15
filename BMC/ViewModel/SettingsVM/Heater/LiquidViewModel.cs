using BMC.Model;
using System.Collections.Generic;

namespace BMC.ViewModel
{
    public class LiquidViewModel :  BaseViewModel
    {
        #region Properties
        public List<int> PumpPowerDB
        { get; set; }
        public List<int> ValvePowerDB { get; set; }
        public bool? IsCheckedAirTemp
        {get;set;}
        public int SelectedPumpPower { get; set; }
        public int SelectedValvePower { get; set; }
        #endregion

        #region Constructor
        public LiquidViewModel()
        {
            PumpPowerDB = LiquidModel.GetPumpPower();
            ValvePowerDB = LiquidModel.GetVentPower();
            IsCheckedAirTemp = false;
            SelectedPumpPower = PumpPowerDB[0];
            SelectedValvePower = ValvePowerDB[0];
        }
        #endregion

        #region Data Methods
        /// <summary>
        /// StringData[0]-CheckedAirTemp
        /// </summary>
        /// <returns></returns>
        public override DataClass GetControlData()
        {
            var result = new DataClass();
            result.StringData.Add(BoolToStringConverter.BTS((bool)IsCheckedAirTemp));
            return result;
        }
        /// <summary>
        /// IntData[0]-Pump power,IntData[1]-valve power
        /// </summary>
        /// <returns></returns>
        public override DataClass GetPowerData()
        {
            var result = new DataClass();
            result.IntData.Add(SelectedPumpPower);
            result.IntData.Add(SelectedValvePower);
            result.StringData.Add(BoolToStringConverter.BTS((bool)IsCheckedAirTemp));
            return result;
        }
        #endregion
    }
}
