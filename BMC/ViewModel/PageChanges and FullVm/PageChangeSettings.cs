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
        
        #region Constructor
        public PageChangeSettings()
        {
            HeaterVM = new HeaterViewModel();
            CoolerVM = new CoolerViewModel();
            HumidVM = new HumidViewModel();
            HEVM = new HeatExchangeViewModel();
            RecircVM = new RecircViewModel();
            GatesVM = new GatesViewModel();
            FilterVM = new FilterViewModel();
            SettingVM = new SettingViewModel();
            VentVM = new VentSettingViewModel();
            PageViewSettingsModels.Add(new EmptyVM());
            PageViewSettingsModels.Add(HeaterVM);
            PageViewSettingsModels.Add(CoolerVM);
            PageViewSettingsModels.Add(HumidVM);
            PageViewSettingsModels.Add(HEVM);
            PageViewSettingsModels.Add(RecircVM);
            PageViewSettingsModels.Add(GatesVM);
            PageViewSettingsModels.Add(FilterVM);
            PageViewSettingsModels.Add(SettingVM);
            PageViewSettingsModels.Add(VentVM);


            // Set starting page
            CurrentPageViewSettingsModel = PageViewSettingsModels[0];
        }
        #endregion

        #region Private variables
        private ICommand _changePageCommand;
        private BaseViewModel _currentPageViewSettingsModel;
       
        private List<BaseViewModel> _pageViewSettingsModel;
        private ICommand _getAllControlData;
        private DelegateCommand _getAnotherOne;
        #endregion

        #region Properties / Commands
        public int TakenData { get; set; }
        
        public ICommand ChangePageSettingsCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new DelegateCommand(
                        p => ChangeViewSettingsModel((BaseViewModel)p),
                        p => p is BaseViewModel);
                }

                return _changePageCommand;
            }
        }
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
        public HeatExchangeViewModel HEVM { get; set; }
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
        /*public ICommand GetAnotherOne
        {
            get
            {
                if (_getAnotherOne == null)
                {
                    _getAnotherOne = new DelegateCommand(
                        p => GetInfo());         ////////test
                }

                return _getAnotherOne;
            }
        }*/
       /* public ICommand GetAllControlData
        {
            get
            {
                if (_getAllControlData == null)
                {
                    _getAllControlData = new DelegateCommand(
                        p =>GetInfo());         ////////test
                }

                return _getAllControlData;
            }
        }*/
        #endregion

        #region Methods

       
        private void ChangeViewSettingsModel(BaseViewModel viewModel)
        {
            if (!PageViewSettingsModels.Contains(viewModel))
                PageViewSettingsModels.Add(viewModel);
           
            CurrentPageViewSettingsModel = PageViewSettingsModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        /*private void GetInfo()
        {
            Sbor FullSystem = new Sbor();
            var result = FullSystem.GetControlParts();
            ControlInfo = result;
            ControlInfoPins = FullSystem.GetAllControlData();

        }*/



        #endregion

       

    }

}

