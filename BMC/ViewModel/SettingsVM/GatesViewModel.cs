using System.Collections.Generic;
using BMC.Model;

namespace BMC.ViewModel
{
    public class GatesViewModel : BaseViewModel
    {               
        #region Properties

        public bool? SpringReturn { get; set; }
        public List<string> ControlType { get; set; }
        public bool? IsSelectedForced { get; set; }
        public bool? IsSelectedExhausted { get; set; }
        public int SelectedFPower { get; set; }
        public int SelectedExhPower { get; set; }
        public List<int> FPower { get; set; }
        public List<int> ExhPower { get; set; }
        public string SelectedControl { get; set; }

        #endregion

        #region Metods

        public bool VisibleGrid { get; set; }


        public static void SetToNull(GatesViewModel current)
        {
            current.VisibleGrid = false;
        }
        public static void SetToStandart(GatesViewModel current)
        {
            current.VisibleGrid = true;
        }

        private static List<string> GetControl()
        {
            List<string> result = new List<string> { "Двухпозиционное", "0-10В" };
            return result;
        }
        #endregion

        #region Constructor
        public GatesViewModel()
        {
            ControlType = GetControl();
            FPower = Gates.GetPower();
            ExhPower = Gates.GetPower();
            IsSelectedExhausted = false;
            IsSelectedForced = false;
            SpringReturn = false;
            SelectedExhPower = ExhPower[0];
            SelectedFPower = FPower[0];
            SelectedControl = ControlType[0];

        }
        #endregion

        #region Data Methods
        /// <summary>
        /// StringData[0] - exhausted,StringData[1] - forced;
        /// </summary>
        /// <returns></returns>
        public override DataClass GetControlData()
        {
            DataClass result = new DataClass();
            result.StringData.Add(BoolToStringConverter.BTS((bool)IsSelectedExhausted));
            result.StringData.Add(BoolToStringConverter.BTS((bool)IsSelectedForced));
            return result;
        }
        /// <summary>
        /// IntData[0] - exhausted power, IntData[1] - forced power
        /// If not chosen = 0
        /// </summary>
        /// <returns></returns>
        public override DataClass GetPowerData()
        {
            DataClass result = new DataClass();
            result.StringData.Add(SelectedControl);
            result.StringData.Add(BoolToStringConverter.BTS((bool)SpringReturn));
            if (IsSelectedExhausted == true)
                result.IntData.Add(SelectedExhPower);
            else
                result.IntData.Add(0);
            if (IsSelectedForced == true)
                result.IntData.Add(SelectedFPower);
            else
                result.IntData.Add(0);
            return result;
        }
        #endregion
    }
}