using System.Collections.Generic;
using BMC.Model;

namespace BMC.ViewModel
{
    public class RecircViewModel : BaseViewModel
    {
        private bool _visibleGrid;
        #region Properties
        public bool? WithoutExh { get; set; }
        public bool? WithoutVent { get; set; }
        public List<int> Power { get; set; }
        public int SelectedPower { get; set; }
        #endregion

        #region Metods

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


        public static void SetToNull(RecircViewModel Current)
        {
            Current.VisibleGrid = false;
        }
        public static void SetToStandart(RecircViewModel Current)
        {
            Current.VisibleGrid = true;
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