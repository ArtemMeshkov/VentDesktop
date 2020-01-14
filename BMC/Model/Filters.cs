using System.Collections.Generic;
using BMC.ViewModel;
namespace BMC.Model
{
    public class Filters:Pins
    {
        #region Properties
        public int NumberofExhaustedFilters { get; set; }
        public string ExhaustedChosen { get; set; }
        public string ForcedChosen { get; set; }
        public int NumberOfForcedFilters { get; set; }
        #endregion

        #region Get Methods
        public static List<int> GetNumber ()
        {
            var result = new List<int> { 1, 2, 3 };
            return result;
        }
        /// <summary>
        ///  List[0]=AO,List[1]=DO,List[2]=AI,List[3]=DI
        /// </summary>
        /// <param name="filterVM"></param>
        /// <returns></returns>
        public List<int> GetPins(FilterViewModel filterVM)
        {
            DataClass filterControlData = filterVM.GetControlData();
            DI += filterControlData.IntData[0];
            DI += filterControlData.IntData[1];
            List<int> result =new List<int>{ AO,DO,AI,DI};
            return result;
        }
        #endregion
    }
}
