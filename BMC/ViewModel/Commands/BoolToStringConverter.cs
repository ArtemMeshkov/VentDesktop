using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.ViewModel
{
    class BoolToStringConverter
    {
        public static string BTS(bool value)
        {
            if (value == true)
                return "Да";
            else
                return "Нет";
            
        }
    }
}
