using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.Model
{
    public class Glycol
    {
        public int ValvePower { get; set; }
        public int DrivePower { get; set; }
        public string VentStatus { get; set; }
        public static List<int> GetValvePower()
        {
            var result = new List<int> { 220, 380 };
            return result;
        }
        public static List<int> GetDrivePower()
        {
            var result = new List<int> { 24, 220 };
            return result;
        }
    }
}
