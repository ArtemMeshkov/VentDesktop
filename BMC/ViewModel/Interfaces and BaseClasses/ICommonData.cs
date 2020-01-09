using BMC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC
{
    public interface ICommonData
    {
        DataClass GetControlData();
        DataClass GetPowerData();
    }
}
