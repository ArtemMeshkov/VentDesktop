using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.Model
{
    public class Shield:BaseViewModel
    {
        #region Properties
        private List<int> ControlList { get; set; }
        private List<PowerObject> PowerList { get; set; }
        public string Name { get; set; }
        public int Number { get; set; } = 1;
        #endregion

        #region Constructor
        public Shield()
        {
            ControlList = new List<int>();
            PowerList = new List<PowerObject>();
        }
        #endregion

        #region Get Methods
        public List<int> GetControlInfo()
        {
            AllPartsData controlSbor = new AllPartsData();
            ControlList = controlSbor.GetAllControlData();
            return ControlList;
        }

        public List<PowerObject> GetPowerInfo()
        {
            AllPartsData powerSbor = new AllPartsData();
            PowerList = powerSbor.GetAllPowerData();
            return PowerList;
        }
        #endregion
    }
}
