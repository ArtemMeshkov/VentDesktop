using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.Model
{
    public class DataClass
    {
        public List<int> IntData { get; set; }
        public List<string> StringData { get; set; }
        public DataClass()
        {
            IntData = new List<int>();
            StringData = new List<string>();
        }
    }
}
