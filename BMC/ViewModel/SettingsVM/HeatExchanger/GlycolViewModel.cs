using BMC.Model;
using System.Collections.Generic;

namespace BMC.ViewModel
{
    public class GlycolViewModel : BaseViewModel
    {
        #region Properties
        public int SelectedDrivePower { get; set; }
        public int SelectedValvePower { get; set; }
        public bool? IsVentChecked { get; set; }
        public List<int> ValvePower { get; set; }
        public List<int> DrivePower { get; set; }
        #endregion

        #region Constructor
        public GlycolViewModel()
        {
            ValvePower =Glycol.GetValvePower();
            DrivePower =Glycol.GetDrivePower();
            SelectedDrivePower = DrivePower[0];
            SelectedValvePower = ValvePower[0];
            IsVentChecked = false;
        }
        #endregion

        #region Data Methods
        /// <summary>
        /// StringData[0] - Have/have no fan
        /// </summary>
        /// <returns></returns>
        public override DataClass GetControlData()
        {
            DataClass result = new DataClass();
            result.StringData.Add(BoolToStringConverter.BTS((bool)IsVentChecked));
            return result;

        }

        /// <summary>
        /// IntData[0]-Drive power(=0 if there is no fan)
        /// IntData[1]- Valve power
        /// </summary>
        /// <returns></returns>
        public override DataClass GetPowerData()
        {
            DataClass result = new DataClass();
            result.IntData.Add(SelectedValvePower);
            result.IntData.Add(SelectedDrivePower);
            return result;
        }
        #endregion
    }
}