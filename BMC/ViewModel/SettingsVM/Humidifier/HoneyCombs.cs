using BMC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.ViewModel
{
    public class HoneyCombs: BaseViewModel
    {
        #region Properties
        public List<string> WaterTypes { get; set; }
        public List<int> NumOfStages { get; set; }
        public int SelectedNumOfStages { get; set; }
        public string SelectedWaterTypes { get; set; }
        public bool? ByPass { get; set; }
        public bool? InWater { get; set; }
        public bool? OutWater { get; set; }
        public bool? LevelCheck { get; set; }
        #endregion

        #region Constructor
        public HoneyCombs()
        {
            WaterTypes = HoneyCombsModel.GetTypes();
            NumOfStages = HoneyCombsModel.GetNumOfStages();
            SelectedWaterTypes = WaterTypes[0];
            SelectedNumOfStages = NumOfStages[0];
            ByPass = false;
            OutWater = false;
            LevelCheck = false;
            InWater = false;
        }
        #endregion

        #region Data Methods
        /// <summary>
        /// IntData[0]-number of stages
        /// StringData[0] - type of water supplying, StringData[1] - air bypass, StringData[2]-water supply valve, StringData[3]- water return valve, StringData[4]-level sensor 
        /// </summary>
        /// <returns></returns>
        public override DataClass GetControlData()
        {
            DataClass result = new DataClass(); 
            result.IntData.Add(SelectedNumOfStages);
            result.StringData.Add(SelectedWaterTypes);
            result.StringData.Add(BoolToStringConverter.BTS((bool)ByPass));
            if (SelectedWaterTypes == WaterTypes[0])
            {
                result.StringData.Add(BoolToStringConverter.BTS((bool)InWater));
                result.StringData.Add(BoolToStringConverter.BTS((bool)OutWater));
                result.StringData.Add(BoolToStringConverter.BTS((bool)LevelCheck));
            }
            return result;
        }
        /// <summary>
        /// ?? IntData[0]-number of stages
        /// </summary>
        /// <returns></returns>
        public override DataClass GetPowerData()
        {
            DataClass result = new DataClass();
            result.StringData.Add(BoolToStringConverter.BTS((bool)ByPass));
            result.IntData.Add(SelectedNumOfStages);
            return result;
        }

        #endregion
    }
}
