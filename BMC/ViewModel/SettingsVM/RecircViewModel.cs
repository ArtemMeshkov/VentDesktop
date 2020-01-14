using System.Collections.Generic;
using BMC.Model;

namespace BMC.ViewModel
{
    public class RecircViewModel : BaseViewModel
    {
        #region Properties

        public bool? WithoutExh { get; set; }
        public bool? WithoutVent { get; set; }
        public List<int> Power { get; set; }
        public int SelectedPower { get; set; }
        public bool VisibleGrid { get; set; }

        #endregion

        #region Metods

        public static void SetToNull(RecircViewModel current)
        {
            current.VisibleGrid = false;
        }
        public static void SetToStandart(RecircViewModel current)
        {
            current.VisibleGrid = true;
        }

        #endregion

        #region Constructor
        public RecircViewModel()
        {
            Power = Recirc.Power();
            WithoutVent = false;
            WithoutExh = false;
            SelectedPower = Power[0];
        }
        #endregion

        #region Data Methods
        /// <summary>
        /// StringData[0] - exhausted vent is disabled/enabled
        /// </summary>
        /// <returns></returns>
        public override DataClass GetControlData()
        {
            DataClass result = new DataClass();
            result.StringData.Add(BoolToStringConverter.BTS((bool)WithoutVent));
            return result;
        }
        /// <summary>
        /// IntData[0] - power of flaps
        /// </summary>
        /// <returns></returns>
        public override DataClass GetPowerData()
        {
            DataClass result = new DataClass();
            result.IntData.Add(SelectedPower);
            return result;
        }
        #endregion
    }
}