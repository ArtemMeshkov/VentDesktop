using BMC.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BMC.ViewModel
{

    public class MainWindowVM: PageChangeSettings
    {
        #region Private variables
        private ICommand _changePageCommand;
        private bool _fullEnable;
        private bool _exhaustedEnable;
        private bool _forcedEnable;
        private List<BaseViewModel> _pageViewModels;
        #endregion

        #region Private window member

        /// <summary>
        /// The window this view model controls
        /// </summary>
        private Window mWindow;

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        private int mOuterMarginSize = 0;

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        private int mWindowRadius = 0;

        /// <summary>
        /// The last known dock position
        /// </summary>   

        #endregion

        #region Properties and commands
        public bool FullEnable
        {
            get
            {
                _fullEnable = (!ForcedIsChecked && !ExhaustedIsChecked);
                return _fullEnable;
            }
            set
            {
                _fullEnable = value;
            }
        }
        public bool ExhaustedEnable
        {
            get
            {
                _exhaustedEnable = (!ForcedIsChecked && !FullIsChecked);
                return _exhaustedEnable;
            }
            set
            {
                _exhaustedEnable = value;
            }
        }
        public bool ForcedEnable
        {
            get
            {
                _forcedEnable = (!FullIsChecked && !ExhaustedIsChecked);
                return _forcedEnable;
            }
            set
            {
                _forcedEnable = value;
            }
        }
                
        public List<BaseViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<BaseViewModel>();

                return _pageViewModels;
            }
            set { }
        }

        public BaseViewModel CurrentPageViewModel
        {
            get; set;
        }
        public string SelectedItems { get; set; }
        public bool FullIsChecked { get; set; }
        public bool ForcedIsChecked { get; set; }
        public bool ExhaustedIsChecked { get; set; }
        private static MainWindowVM instance { get; set; }
        public FullViewModel FullVM { get; set; }
        public ObservableCollection<Shield> ShieldList { get; set; }
        public Shield SelectedShield { get; set; }
        public string SystemName { get; set; }
        public ICommand ChangePageCommand
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
        #endregion
                
        #region Public properties for window
        
        /// <summary>
        /// The smallest width the window can go to
        /// </summary>
        public double WindowMinimumWidth { get; set; } = 400;

        /// <summary>
        /// The smallest height the window can go to
        /// </summary>
        public double WindowMinimumHeight { get; set; } = 400;


        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public int ResizeBorder { get; set; } = 6;

        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public int TitleHeight { get; set; } = 42;
        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }

        #endregion

        #region Commands for window

        /// <summary>
        /// The command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// The command to maximize the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// The command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to show the system menu of the window
        /// </summary>
        public ICommand MenuCommand { get; set; }

        #endregion

        #region Constructor
        public MainWindowVM(Window window)
        {
            FullVM = new FullViewModel();
            PageViewModels.Add(new EmptyVM());
            PageViewModels.Add(FullVM);
            CurrentPageViewModel = PageViewModels[0];
            ShieldList = new ObservableCollection<Shield>();
            mWindow = window;

            // Create commands
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));

            // Fix window resize issue
        

            // Listen out for dock changes
        
        }
        #endregion
               
        #region Methods 
        public static MainWindowVM GetInstance(Window window=null)
        {
            if (instance == null)
                instance = new MainWindowVM(window);
            return instance;
        }
        public static MainWindowVM ResetInstance(Window window)
        {
            
            instance = new MainWindowVM(window);
            return instance;
        }
       
        public void DeleteShield(int i)
        {
            ShieldList.RemoveAt(i);
        }
        public static MainWindowVM GetStatus()
        {
            return instance;
        }

        public void ChangeViewModel(BaseViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }
        #endregion
                              
        #region Private Helpers for window        
        /// <summary>
        /// Gets the current mouse position on the screen
        /// </summary>
        /// <returns></returns>
        private Point GetMousePosition()
        {
            // Position of the mouse relative to the window
            var position = Mouse.GetPosition(mWindow);

            // Add the window position so its a "ToScreen"
            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }

        /// <summary>
        /// If the window resizes to a special position (docked or maximized)
        /// this will update all required property change events to set the borders and radius values
        /// </summary>   
       
        #endregion

    }

}
