using BMC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BMC.ViewModel
{
    public class RotorViewModel: BaseViewModel
    {
        #region Private members

        private int _selectedIntValue;

        #endregion

        #region Properties
        public List<int> RotorPower { get; set; }
        public DoubleCollection PowerStages220 { get; set; }
        public DoubleCollection PowerStages380 { get; set; }
        public List<string> SpeedRegulator { get; set; }
        public List<string> Signal { get; set; }
        public string SelectedSpeedRegulator { get; set; }
        public int SelectedRotorPower { get; set; }
        public string SelectedSignal { get; set; }
        public double SelectedValue { get; set; }
        public int SelectedIntValue
        {
            get
            {
                _selectedIntValue = Convert.ToInt32(SelectedValue);
                return _selectedIntValue;
            }
            set
            {
                _selectedIntValue = value;
            }
        }
        #endregion

        #region Contstructor
        public RotorViewModel()
        {
            RotorPower = Rotor.GetPower();
            PowerStages220 = Rotor.GetPower220();
            SpeedRegulator = Rotor.GetSpeedRegulator();
            Signal = Rotor.GetSignal();
            SelectedRotorPower = RotorPower[0];
            SelectedValue = PowerStages220[0];
            SelectedSpeedRegulator = SpeedRegulator[0];
            SelectedSignal = Signal[0];
        }
        #endregion

        #region Data Methods
        /// <summary>
        /// StringData[0]- Type of signal of speed regulator,standart 0-10V
        /// </summary>
        /// <returns></returns>
        public override DataClass GetControlData()
        {
            DataClass result = new DataClass();
            result.StringData.Add(SelectedSignal);
            return result;
        }

        /// <summary>
        /// IntData[0] - Power of rotor
        /// StringData[0]-Type of regulation
        /// !!!!!StringData[1] - Power of engine in STRING type
        /// </summary>
        /// <returns></returns>
        public override DataClass GetPowerData()
        {
            DataClass result = new DataClass();
            result.IntData.Add(SelectedRotorPower);
            result.StringData.Add(SelectedValue.ToString()); //3
            result.StringData.Add(SelectedSpeedRegulator);//4
            result.StringData.Add(SelectedSignal);//5
            return result;
        }
        
        #endregion
    }
}