using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BMC.Model;

namespace BMC.ViewModel
{
    public class VentSettingViewModel : BaseViewModel
    {
        #region Private members

        private List<BaseViewModel> _pageViewModels;

        #endregion

        #region Properties
        public VentViewModel ForcedVM { get; set; }
        public VentViewModel ExhaustedVM { get; set; }
        public List<BaseViewModel> PageViewVentModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<BaseViewModel>();

                return _pageViewModels;
            }
        }

        public BaseViewModel CurrentPageForcedViewModel
        {
            get; set;
        }
        public BaseViewModel CurrentPageExhaustedViewModel
        {
            get; set;
        }

        #endregion

        #region Constructor
        public VentSettingViewModel()
        {
            ForcedVM = new VentViewModel();
            ExhaustedVM = new VentViewModel();
            PageViewVentModels.Add(ForcedVM);
            PageViewVentModels.Add(ExhaustedVM);
            CurrentPageForcedViewModel = PageViewVentModels[0];
            CurrentPageExhaustedViewModel = PageViewVentModels[1];
        }
        #endregion

        #region Data methods
        /// <summary>
        /// StringData[0] - exhausted type, StringData[1] - forced type; after - vent properties
        /// </summary>
        /// <returns></returns>
        public override DataClass GetControlData()
        {
            DataClass result = new DataClass();
            MainWindowVM instance = MainWindowVM.GetInstance();
            if (instance.ExhaustedIsChecked){
                result.StringData.Add("Да");
                result.StringData.Add("Нет");
                DataClass exhaustedControl = ExhaustedVM.GetControlData();
                result.StringData.AddRange(exhaustedControl.StringData);
                result.IntData.AddRange(exhaustedControl.IntData);
            }
            else if (instance.ForcedIsChecked)
            {
                result.StringData.Add("Нет");
                result.StringData.Add("Да");
                DataClass forcedControl = ForcedVM.GetControlData();
                result.StringData.AddRange(forcedControl.StringData);
                result.IntData.AddRange(forcedControl.IntData);
            }
            else if (instance.FullIsChecked)
            {
                result.StringData.Add("Да");
                result.StringData.Add("Да");
                DataClass exhaustedControl = ExhaustedVM.GetControlData();
                result.StringData.AddRange(exhaustedControl.StringData);
                result.IntData.AddRange(exhaustedControl.IntData);
                DataClass forcedControl = ForcedVM.GetControlData();
                result.StringData.AddRange(forcedControl.StringData);
                result.IntData.AddRange(forcedControl.IntData);
            }
            else
            {
                result.StringData.Add("Нет");
                result.StringData.Add("Нет");
            }
            return result;
        }
        /// <summary>
        /// StringData[0]-exh status,StringData[1]- forced status
        /// First exhausted then forced, so summ if both enabled
        /// IntData[0] - VentPower(220/380), StringData[2]- DrivePower, StringData[3] - PCHCheck;
        /// StringData[4] - ReserveCheck, StringData[5] - TypeOfPchControl
        /// </summary>
        /// <returns></returns>
        public override DataClass GetPowerData()
        {
            DataClass result = new DataClass();
            MainWindowVM instance = MainWindowVM.GetInstance();
            if (instance.ExhaustedIsChecked)
            {
                result.StringData.Add("Да");
                result.StringData.Add("Нет");
                DataClass exhaustedPower = ExhaustedVM.GetPowerData();
                result.StringData.AddRange(exhaustedPower.StringData);
                result.IntData.AddRange(exhaustedPower.IntData);
            }
            else if (instance.ForcedIsChecked)
            {
                result.StringData.Add("Нет");
                result.StringData.Add("Да");
                DataClass forcedPower = ForcedVM.GetPowerData();
                result.StringData.AddRange(forcedPower.StringData);
                result.IntData.AddRange(forcedPower.IntData);
            }
            else if (instance.FullIsChecked)
            {
                result.StringData.Add("Да");
                result.StringData.Add("Да");
                DataClass exhaustedPower = ExhaustedVM.GetPowerData();
                result.StringData.AddRange(exhaustedPower.StringData);
                result.IntData.AddRange(exhaustedPower.IntData);
                DataClass forcedPower = ForcedVM.GetPowerData();
                result.StringData.AddRange(forcedPower.StringData);
                result.IntData.AddRange(forcedPower.IntData);
            }
            else
            {
                result.StringData.Add("Нет");
                result.StringData.Add("Нет");
            }
            return result;
        }
        #endregion
    }
}
