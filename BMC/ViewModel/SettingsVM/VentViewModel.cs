using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BMC.Model;

namespace BMC.ViewModel
{
    public class VentViewModel:BaseViewModel
    {
        #region Properties
        public bool VisibleGrid { get; set; }
        public string SelectedType { get; set; }
        public int SelectedVentPower { get; set; }
        public DoubleCollection DrivePower { get; set; }
        public List<int> VentPower { get; set; }
        public List<string> PCHTypes { get; set; }
        public double SelectedDrivePower { get; set; }
        public bool? PCHChecked { get; set; }
        public bool? ReserveChecked { get; set; }
        #endregion

        #region Constructor
        public VentViewModel()
        {
            VentPower = VentSettings.GetVentPower();
            DrivePower = VentSettings.GetDrivePower();
            PCHTypes = VentSettings.GetPCHTypes();
            SelectedDrivePower = DrivePower[0];
            SelectedVentPower = VentPower[0];
            SelectedType = PCHTypes[0];
            PCHChecked = false;
            ReserveChecked = false;
        }
        #endregion

        #region Data Methods
        /// <summary>
        /// StringData[0]-PCH; StringData[1] - Reserve; StringData[2] - TypeOfPCH
        /// </summary>
        /// <returns></returns>
        public override DataClass GetControlData()
        {
            var result = new DataClass();
            result.StringData.Add(BoolToStringConverter.BTS((bool)PCHChecked));
            result.StringData.Add(BoolToStringConverter.BTS((bool)ReserveChecked));
            result.StringData.Add(SelectedType);
            return result;
        }
        /// <summary>
        /// IntData[0] - VentPower(220/380), StringData[0]- DrivePower, StringData[1] - PCHCheck;
        /// StringData[2] - ReserveCheck, StringData[3] - TypeOfPchControl
        /// </summary>
        /// <returns></returns>
        public override DataClass GetPowerData()
        {
            var result = new DataClass();
            result.IntData.Add(SelectedVentPower);
            result.StringData.Add(SelectedDrivePower.ToString());
            result.StringData.Add(BoolToStringConverter.BTS((bool)PCHChecked));
            result.StringData.Add(BoolToStringConverter.BTS((bool)ReserveChecked));
            result.StringData.Add(SelectedType);           
            return result;
        }
        #endregion
    }
}
