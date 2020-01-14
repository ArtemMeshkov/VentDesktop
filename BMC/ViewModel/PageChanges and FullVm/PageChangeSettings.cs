using BMC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BMC.ViewModel
{
    public class PageChangeSettings : BaseViewModel
    {
        #region Private variables
        private BaseViewModel _currentPageViewSettingsModel;

        private List<BaseViewModel> _pageViewSettingsModel;
        #endregion

        #region Properties
        public int TakenData { get; set; }
        public List<string> ControlInfo { get; set; }
        public List<int> ControlInfoPins { get; private set; }
        public List<BaseViewModel> PageViewSettingsModels
        {
            get
            {
                if (_pageViewSettingsModel == null)
                    _pageViewSettingsModel = new List<BaseViewModel>();

                return _pageViewSettingsModel;
            }
        }
        public HeaterViewModel HeaterVM { get; set; }
        public CoolerViewModel CoolerVM { get; set; }
        public HumidViewModel HumidVM { get; set; }
        public HeatExchangeViewModel HeatExchangeVM { get; set; }
        public RecircViewModel RecircVM { get; set; }
        public GatesViewModel GatesVM { get; set; }
        public FilterViewModel FilterVM { get; set; }
        public SettingViewModel SettingVM { get; set; }
        public VentSettingViewModel VentVM { get; set; }
        public BaseViewModel CurrentPageViewSettingsModel
        {
            get
            {

                _currentPageViewSettingsModel = PageViewSettingsModels[TakenData];
                return _currentPageViewSettingsModel;
            }
            set
            {
                _currentPageViewSettingsModel = value;
            }
        }
        #endregion

        #region Constructor
        public PageChangeSettings()
        {
            HeaterVM = new HeaterViewModel();
            CoolerVM = new CoolerViewModel();
            HumidVM = new HumidViewModel();
            HeatExchangeVM = new HeatExchangeViewModel();
            RecircVM = new RecircViewModel();
            GatesVM = new GatesViewModel();
            FilterVM = new FilterViewModel();
            SettingVM = new SettingViewModel();
            VentVM = new VentSettingViewModel();
            PageViewSettingsModels.Add(new EmptyVM());
            PageViewSettingsModels.Add(HeaterVM);
            PageViewSettingsModels.Add(CoolerVM);
            PageViewSettingsModels.Add(HumidVM);
            PageViewSettingsModels.Add(HeatExchangeVM);
            PageViewSettingsModels.Add(RecircVM);
            PageViewSettingsModels.Add(GatesVM);
            PageViewSettingsModels.Add(FilterVM);
            PageViewSettingsModels.Add(SettingVM);
            PageViewSettingsModels.Add(VentVM);            
            // Set starting page
            CurrentPageViewSettingsModel = PageViewSettingsModels[0];
        }
        #endregion

    }

}

