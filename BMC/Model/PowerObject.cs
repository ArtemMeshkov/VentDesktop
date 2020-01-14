using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.Model
{
    public class PowerObject
    {
        #region Properties
        public string Name { get; set; }
        public int Number { get; set; }
        #endregion

        #region Constructor
        public PowerObject(string name,int number)
        {
            this.Name = name;
            this.Number = number;
        }
        #endregion
    }
}
