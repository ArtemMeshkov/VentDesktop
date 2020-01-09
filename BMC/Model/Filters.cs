using System.Collections.Generic;
using BMC.ViewModel;
namespace BMC.Model
{
    public class Filters:Pins
    {
        public int NumberofExhaustedFilters { get; set; }
        public string ExhaustedChosen { get; set; }
        public string ForcedChosen { get; set; }
        public int NumberOfForcedFilters { get; set; }
        public static List<int> GetNumber ()
        {
            var result = new List<int> { 1, 2, 3 };
            return result;
        }
        /// <summary>
        ///  List[0]=AO,List[1]=DO,List[2]=AI,List[3]=DI
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<int> GetPins(FilterViewModel p)
        {
            DataClass FilterResult = p.GetControlData();
            DI += FilterResult.IntData[0];
            DI += FilterResult.IntData[1];
            List<int> result =new List<int>{ AO,DO,AI,DI};
            return result;
        }
    }
}
