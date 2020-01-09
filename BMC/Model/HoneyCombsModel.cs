using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.Model
{
    public class HoneyCombsModel
    {
        public string WaterType { get; set; }
        public int NumOfStages { get; set; }
        public string ByPass { get; set; }
        public string InWater { get; set; }
        public string OutWater { get; set; }
        public string LevelCheck { get; set; }
        public static List<string> GetTypes()
        {
            var result = new List<string> { "Оборотная", "Прямая" };
            return result;

        }
        public static List<int> GetNumOfStages()
        {
            var result = new List<int>();
            result.Add(1);
            result.Add(2);
            result.Add(3);
            result.Add(4);
            result.Add(5);
            return result;
        }
    }
}
