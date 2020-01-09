using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.Model
{
    public class Shield:BaseViewModel
    {
        public List<int> ControlList { get; set; }
        public List<PowerObject> PowerList { get; set; }
        public string Name { get; set; }
        public int Number { get; set; } = 1;

        public Shield()
        {
            ControlList = new List<int>();
            PowerList = new List<PowerObject>();
        }

        public List<int> GetControlInfo()
        {
            Sbor ControlSbor = new Sbor();
            ControlList = ControlSbor.GetAllControlData();
            return ControlList;
        }

        public List<PowerObject> GetPowerInfo()
        {
            Sbor PowerSbor = new Sbor();
            PowerList = PowerSbor.GetAllPowerData();
            return PowerList;
        }
    }
}
