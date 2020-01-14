using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.Model
{
    public class HoneyCombsModel
    {
        #region Properties
        public string WaterType { get; set; }
        public int NumOfStages { get; set; }
        public string ByPass { get; set; }
        public string InWater { get; set; }
        public string OutWater { get; set; }
        public string LevelCheck { get; set; }
        #endregion

        #region
        public static List<string> GetTypes()
        {
            var result = new List<string> { "Оборотная", "Прямая" };
            return result;

        }
        public static List<int> GetNumOfStages()
        {
            var result = new List<int> { 1, 2, 3, 4, 5 };
            return result;
        }
        #endregion
    }
}
