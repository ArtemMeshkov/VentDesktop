using BMC.Model;
using System.Collections.Generic;

namespace BMC.ViewModel
{
    public class PlateWithBypassViewModel : BaseViewModel
    {
        #region Properties
        public List<int> Power{get;set;}
        public int SelectedPower { get; set; }
        #endregion

        #region Constructor
        public PlateWithBypassViewModel()
        {
            Power = PlateWithBypass.GetPower();
            SelectedPower = Power[0];
        }
        #endregion

        #region Data Methods
        /// <summary>
        /// IntData[0] - Power of drive 
        /// </summary>
        /// <returns></returns>
        
        public override DataClass GetPowerData()
        {
            var result = new DataClass();
            result.IntData.Add(SelectedPower);
            return result;
        }
        #endregion

    }
}