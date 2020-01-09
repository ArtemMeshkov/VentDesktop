using System;
using System.Collections.Generic;
using System.Windows.Media;
using BMC.Model;

namespace BMC.ViewModel
{
    public class ElectricalViewModel : BaseViewModel
    {
        #region Properties
        private int _selectedIntvalue;

        public DoubleCollection mass { get; set; }
        public double SelectedValue
        { get; set; }
        public int SelectedNumOfStages { get; set; }
        public  int SelectedThermoSwitch
        {
            get;set;
        }
        public  bool? PCHChecked { get; set; }
        public List<int> NumOfStagesBD { get; set; }
        public List<int> ThermoSwitchBD { get; set; }
        public int SelectedIntValue
        {
            get
            {
                _selectedIntvalue = Convert.ToInt32(SelectedValue);
                return _selectedIntvalue;
            }
            set
            {
                _selectedIntvalue = value;
            }
        }
        public static int Test1
        {get;set;}
        #endregion

        #region Constructor
        public ElectricalViewModel()
        {
            NumOfStagesBD = ElectricModel.GetNumOfStages();
            mass = ElectricModel.GetPowerOfStages();
            ThermoSwitchBD = ElectricModel.GetThermoSwitches();
            PCHChecked = false;
            SelectedNumOfStages = NumOfStagesBD[0];
            SelectedThermoSwitch = ThermoSwitchBD[0];
            SelectedValue = 0;

        }
        #endregion

        #region Data Methods
        /// <summary>
        /// IntData[0]-number of stages, IntData[1]- number of thermoswitches=
        /// </summary>
        /// <returns></returns>
        public override DataClass GetControlData()
        {
            DataClass result = new DataClass();
            result.IntData.Add(SelectedNumOfStages);
            result.IntData.Add(SelectedThermoSwitch);
            if(PCHChecked!=null)
            result.StringData.Add(BoolToStringConverter.BTS((bool)PCHChecked));
            else
            {
                result.StringData.Add("Нет");
            }
            return result;
        }
        /// <summary>
        /// IntData[0]-Value of stage, IntData[1]- Number of stages
        /// </summary>
        /// <returns></returns>
        public override DataClass GetPowerData()
        {
            DataClass result = new DataClass();
            result.IntData.Add(SelectedIntValue);  //value of powerstage
            result.IntData.Add(SelectedNumOfStages);
            result.StringData.Add(BoolToStringConverter.BTS((bool)PCHChecked));
            return result;
        }
            #endregion
        }
}

