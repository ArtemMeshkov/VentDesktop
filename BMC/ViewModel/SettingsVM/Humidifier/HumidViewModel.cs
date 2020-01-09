
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using BMC.Model;

namespace BMC.ViewModel
{
    public class HumidViewModel : BaseViewModel
    {
        #region Private variables
        private ICommand _changePageCommand;
        private List<BaseViewModel> _pageViewModels;
        private string _selectionItemChanged;
        private bool _visibleGrid;
        #endregion

        #region Properties / Commands
        public List<string> HumidList { get; set; }
        public SteamHumid SteamHumidVM { get; set; }
        public HoneyCombs HoneyCombsVM { get; set; }
        public ICommand ChangePageHeaterCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new DelegateCommand(
                        p =>
                        {
                            BaseViewModel model = p as BaseViewModel;
                            if (model != null)
                            {
                                ChangeViewModel(model);
                            }
                        },
                        p => p is BaseViewModel);
                }

                return _changePageCommand;
            }
        }

        public List<BaseViewModel> PageViewHumidModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<BaseViewModel>();

                return _pageViewModels;
            }
        }

        public BaseViewModel CurrentPageHumidViewModel
        {
            get; set;
        }

        public string SelectedValueHumidVar
        {
            get
            {
                return _selectionItemChanged;
            }
            set
            {
                _selectionItemChanged = value;
                if (_selectionItemChanged.Contains(HumidList[0]))
                    ChangeViewModel(PageViewHumidModels[1]);
                else
                    ChangeViewModel(PageViewHumidModels[2]);

            }
        }

        #endregion

        #region Methods 

        public void ChangeViewModel(BaseViewModel viewModel)
        {
            if (!PageViewHumidModels.Contains(viewModel))
                PageViewHumidModels.Add(viewModel);

            CurrentPageHumidViewModel = PageViewHumidModels
                .FirstOrDefault(vm => vm == viewModel);
        }

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


        public static void SetToNull(HumidViewModel Current)
        {
            Current.VisibleGrid = false;
        }
        public static void SetToStandart(HumidViewModel Current)
        {
            Current.VisibleGrid = true;
        }

        #endregion

        #region Constructor
        public HumidViewModel()
        {
            SteamHumidVM = new SteamHumid();
            HoneyCombsVM = new HoneyCombs();
            HumidList = Humid.GetTypes();
            PageViewHumidModels.Add(new EmptyVM());
            PageViewHumidModels.Add(SteamHumidVM);
            PageViewHumidModels.Add(HoneyCombsVM);
            CurrentPageHumidViewModel = PageViewHumidModels[0];
            SelectedValueHumidVar = HumidList[0];
        }
        #endregion

        #region Data Methods
        /// <summary>
        /// If honeycombs chosen - all after StringData[0] are its properties and all IntData
        /// If not - only StringData[0] type of humidifier
        /// </summary>
        /// <returns></returns>
        public override DataClass GetControlData()
        {
            DataClass result = new DataClass();
            result.StringData.Add(SelectedValueHumidVar);
            if (SelectedValueHumidVar == HumidList[1])
            {
                DataClass HoneyCombsResult = HoneyCombsVM.GetControlData();
                result.IntData.AddRange(HoneyCombsResult.IntData);
                result.StringData.AddRange(HoneyCombsResult.StringData);
            }
            return result;

        }
        /// <summary>
        /// If honeycombs chosen - all StringData are its properties and all IntData
        /// </summary>
        /// <returns></returns>
        public override DataClass GetPowerData()
        {
            DataClass result = new DataClass();
            if (SelectedValueHumidVar == HumidList[1])
            {
                DataClass HoneyCombsResult = HoneyCombsVM.GetPowerData();
                result.IntData.AddRange(HoneyCombsResult.IntData);
                result.StringData.AddRange(HoneyCombsResult.StringData);
            }
            return result;
        }
        #endregion
    }
}