using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BMC.Model
{
    public class Rotor
    {
        #region Properties
        public int RecupPower { get; set; }
        public DoubleCollection Power220 { get; set; }
        public DoubleCollection Power380 { get; set; }
        public string SpeedRegulator { get; set; }
        public string SpeedSignal { get; set; }
        #endregion

        #region Get Methods
        public static List<int> GetPower()
        {
            var result=new List<int>{220,380};
            return result;
        }
        public static DoubleCollection GetPower220()
        {
            var result = new  DoubleCollection { 0.18, 0.25, 0.55, 0.75, 1.1, 1.5, 2.2 };
            return result;
        }
        public static DoubleCollection GetPower380()
        {
            var result = new DoubleCollection { 0.18, 0.25, 0.55, 0.75, 1.1, 1.5};
            return result;
        }
        public static List<string> GetSpeedRegulator()
        {
            var result = new List<string> { "Частотный преобразователь", "Встроенный регулятор" };
            return result;
        }
        public static List<string> GetSignal()
        {
            var result = new List<string> { "0-10 В", "MODBUS RTU" };
            return result;
        }
        #endregion
    }
}
