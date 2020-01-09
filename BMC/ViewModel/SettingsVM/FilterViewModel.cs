
using BMC.Model;
using System.Collections.Generic;

namespace BMC.ViewModel
{
    public class FilterViewModel : BaseViewModel,ICommonData
    {
        private bool _visibleGrid;
        #region Properties
        public List<int> FNumber { get; set; }
        public List<int> ExhNumber { get; set; }
        public bool? IsForcedSelected { get; set; }
        public bool? IsExhaustedSelected { get; set; }
        public int SelectedFNumber { get; set; }
        public int SelectedExhNumber { get; set; }
        public bool VisibleGrid
        {
            get
            {
                return _visibleGrid;
            }
            set
            {
                _visibleGrid = value;
            }
        }
        #endregion

        #region Metods




        public static void SetToNull(FilterViewModel Current)
        {
            Current.VisibleGrid = false;
        }
        public static void SetToStandart(FilterViewModel Current)
        {
            Current.VisibleGrid = true;
        }

        #endregion

        #region Constructor
        public FilterViewModel()
        {
            FNumber = Filters.GetNumber();
            ExhNumber = Filters.GetNumber();
            IsExhaustedSelected = false;
            IsForcedSelected = false;
            SelectedFNumber = FNumber[0];
            SelectedExhNumber = ExhNumber[0];
        }
        #endregion

        #region Data Methods
        /// <summary>
        /// StringData[0],[1] - first exhausted,second forced - DISABLED LIST
        /// Same IntData[0],[1]. Equals 0 if its not chosen
        /// </summary>
        /// <returns></returns>
        /// 
        public override DataClass GetControlData()
        {
            var result = new DataClass();
            //result.StringData.Add(BoolToStringConverter.BTS((bool)IsExhaustedSelected)); 
           // result.StringData.Add(BoolToStringConverter.BTS((bool)IsForcedSelected));
            if (IsExhaustedSelected == true)
                result.IntData.Add(SelectedExhNumber);
            else
                result.IntData.Add(0);
            if (IsForcedSelected == true)
                result.IntData.Add(SelectedFNumber);
            else
                result.IntData.Add(0);
            return result;
        }
        
        #endregion
    }
}