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
        private List<BaseViewModel> _pageViewModels;
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
        public VentSettingViewModel()
        {
            ForcedVM = new VentViewModel();
            ExhaustedVM = new VentViewModel();
            PageViewVentModels.Add(ForcedVM);
            PageViewVentModels.Add(ExhaustedVM);
            CurrentPageForcedViewModel = PageViewVentModels[0];
            CurrentPageExhaustedViewModel = PageViewVentModels[1];
        }
        /// <summary>
        /// StringData[0] - exhausted type, StringData[1] - forced type; after - vent properties
        /// </summary>
        /// <returns></returns>
        public override DataClass GetControlData()
        {
            DataClass result = new DataClass();
            PageChangeVentTypes k = PageChangeVentTypes.GetInstance();
            if (k.ExhaustedIsChecked){
                result.StringData.Add("Да");
                result.StringData.Add("Нет");
                DataClass forced = ExhaustedVM.GetControlData();
                result.StringData.AddRange(forced.StringData);
                result.IntData.AddRange(forced.IntData);
            }
            else if (k.ForcedIsChecked)
            {
                result.StringData.Add("Нет");
                result.StringData.Add("Да");
                DataClass exhausted = ForcedVM.GetControlData();
                result.StringData.AddRange(exhausted.StringData);
                result.IntData.AddRange(exhausted.IntData);
            }
            else if (k.FullIsChecked)
            {
                result.StringData.Add("Да");
                result.StringData.Add("Да");
                DataClass forced = ExhaustedVM.GetControlData();
                result.StringData.AddRange(forced.StringData);
                result.IntData.AddRange(forced.IntData);
                DataClass exhausted = ForcedVM.GetControlData();
                result.StringData.AddRange(exhausted.StringData);
                result.IntData.AddRange(exhausted.IntData);
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
            PageChangeVentTypes k = PageChangeVentTypes.GetInstance();
            if (k.ExhaustedIsChecked)
            {
                result.StringData.Add("Да");
                result.StringData.Add("Нет");
                DataClass forced = ExhaustedVM.GetPowerData();
                result.StringData.AddRange(forced.StringData);
                result.IntData.AddRange(forced.IntData);
            }
            else if (k.ForcedIsChecked)
            {
                result.StringData.Add("Нет");
                result.StringData.Add("Да");
                DataClass exhausted = ForcedVM.GetControlData();
                result.StringData.AddRange(exhausted.StringData);
                result.IntData.AddRange(exhausted.IntData);
            }
            else if (k.FullIsChecked)
            {
                result.StringData.Add("Да");
                result.StringData.Add("Да");
                DataClass forced = ExhaustedVM.GetPowerData();
                result.StringData.AddRange(forced.StringData);
                result.IntData.AddRange(forced.IntData);
                DataClass exhausted = ForcedVM.GetPowerData();
                result.StringData.AddRange(exhausted.StringData);
                result.IntData.AddRange(exhausted.IntData);
            }
            else
            {
                result.StringData.Add("Нет");
                result.StringData.Add("Нет");
            }
            return result;
        }
    }
}
