using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.Model
{
    public class PowerObject
    {
        public string Name { get; set; }
        public int Number { get; set; }

        public PowerObject(string name,int number)
        {
            this.Name = name;
            this.Number = number;
        }
    }
}
