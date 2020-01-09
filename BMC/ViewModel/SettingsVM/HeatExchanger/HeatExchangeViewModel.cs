using BMC.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace BMC.ViewModel
{
    public class HeatExchangeViewModel : BaseViewModel
    {
        #region Private variables
        private List<BaseViewModel> _pageViewModels;
        private string _selectionItemChanged;
        private bool _visibleGrid;
        #endregion

        #region Properties / Commands

        public List<string> RecupList { get; set; }
        public bool? IsTempSensor { get; set; }
        public bool? IsPressureSensor { get; set; }
        public RotorViewModel RotorVM{get;set;}
        public PlateViewModel PlateVM { get; set; }
        public PlateWithBypassViewModel PlateBypassVM { get; set; }
        public GlycolViewModel GlycolVM { get; set; }
        public string SelectedValueHeaterExchange
        {
            get
            {
                return _selectionItemChanged;
            }
            set
            {
                _selectionItemChanged = value;
                if (_selectionItemChanged.Equals(RecupList[0]))
                    ChangeViewModel(PageViewHeatExchangeModels[1]);
                else if (_selectionItemChanged.Equals(RecupList[1]))
                    ChangeViewModel(PageViewHeatExchangeModels[2]);
                else if (_selectionItemChanged.Equals(RecupList[2]))
                    ChangeViewModel(PageViewHeatExchangeModels[3]);
                else ChangeViewModel(PageViewHeatExchangeModels[4]);

            }
        }

        public List<BaseViewModel> PageViewHeatExchangeModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<BaseViewModel>();

                return _pageViewModels;
            }
        }

        public BaseViewModel CurrentRecupViewPage
        {
            get; set;
        }


        #endregion

        #region Methods 

        public void ChangeViewModel(BaseViewModel viewModel)
        {
            if (!PageViewHeatExchangeModels.Contains(viewModel))
                PageViewHeatExchangeModels.Add(viewModel);

            CurrentRecupViewPage = PageViewHeatExchangeModels
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


        public static void SetToNull(HeatExchangeViewModel Current)
        {
            Current.VisibleGrid = false;
        }
        public static void SetToStandart(HeatExchangeViewModel Current)
        {
            Current.VisibleGrid = true;
        }

        #endregion

        #region Constructor
        public HeatExchangeViewModel()
        {
            RecupList = HeatExchange.GetTypes();
            RotorVM = new RotorViewModel();
            PlateVM = new PlateViewModel();
            PlateBypassVM = new PlateWithBypassViewModel();
            GlycolVM = new GlycolViewModel();
            PageViewHeatExchangeModels.Add(new EmptyVM());
            PageViewHeatExchangeModels.Add(RotorVM);
            PageViewHeatExchangeModels.Add(PlateVM);
            PageViewHeatExchangeModels.Add(PlateBypassVM);
            PageViewHeatExchangeModels.Add(GlycolVM);
            CurrentRecupViewPage = PageViewHeatExchangeModels[0];
            SelectedValueHeaterExchange = RecupList[0];
            IsPressureSensor = false;
            IsTempSensor = false;
            
        }
        #endregion

        #region Data Methods
        /// <summary>
        /// StringData[0]-Type of exchanger,StringData[1]-Temp. sensor,StringData[2]-Press. sensor
        /// After - IntData and StringData of chosen exchanger
        /// </summary>
        /// <returns></returns>
        public override DataClass GetControlData()
        {
            DataClass result = new DataClass();
            result.StringData.Add(SelectedValueHeaterExchange);
            result.StringData.Add(BoolToStringConverter.BTS((bool)IsTempSensor));
            result.StringData.Add(BoolToStringConverter.BTS((bool)IsPressureSensor));
            if (SelectedValueHeaterExchange==RecupList[0])
            {
                DataClass RotorResult = RotorVM.GetControlData();
                if (RotorResult != null)
                {
                    result.IntData.AddRange(RotorResult.IntData);
                    result.StringData.AddRange(RotorResult.StringData);
                }
            }
            else if(SelectedValueHeaterExchange==RecupList[2])
                {
                DataClass PlateBypassResult = PlateBypassVM.GetControlData();
                if (PlateBypassResult != null)
                {
                    result.IntData.AddRange(PlateBypassResult.IntData);
                    result.StringData.AddRange(PlateBypassResult.StringData);
                }
                }
            else if(SelectedValueHeaterExchange==RecupList[3])
                {
                DataClass GlycolResult = GlycolVM.GetControlData();
                if (GlycolResult != null)
                {
                    result.IntData.AddRange(GlycolResult.IntData);
                    result.StringData.AddRange(GlycolResult.StringData);
                }
                }
            return result;
        }
        /// <summary>
        /// IntData and StringData of chosen  exchanger
        /// </summary>
        /// <returns></returns>
        public override DataClass GetPowerData()
        {
            DataClass result = new DataClass();
            if (SelectedValueHeaterExchange == RecupList[0])
            {
                DataClass RotorResult = RotorVM.GetPowerData();
                if (RotorResult != null)
                {
                    result.IntData.AddRange(RotorResult.IntData);
                    result.StringData.AddRange(RotorResult.StringData);
                }
            }
            else if (SelectedValueHeaterExchange == RecupList[2])
            {
                DataClass PlateBypassResult = PlateBypassVM.GetPowerData();
                if (PlateBypassResult != null)
                {
                    result.IntData.AddRange(PlateBypassResult.IntData);
                    result.StringData.AddRange(PlateBypassResult.StringData);
                }
            }
            else if (SelectedValueHeaterExchange == RecupList[3])
            {
                DataClass GlycolResult = GlycolVM.GetPowerData();
                if (GlycolResult != null)
                {
                    result.IntData.AddRange(GlycolResult.IntData);
                    result.StringData.AddRange(GlycolResult.StringData);
                }
            }
            return result;
        }
    }
        #endregion
}

