
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
        #endregion

        #region Properties / Commands
        public List<string> HumidList { get; set; }

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


        #endregion

        #region Constructor
        public HumidViewModel()
        {
            HumidList = Humid.GetTypes();
            PageViewHumidModels.Add(new EmptyVM());
            PageViewHumidModels.Add(new SteamHumid());
            PageViewHumidModels.Add(new HoneyCombs());
            CurrentPageHumidViewModel = PageViewHumidModels[0];
            SelectedValueHumidVar = HumidList[0];
        }
        #endregion

        #region Data Methods
        #endregion
    }

}