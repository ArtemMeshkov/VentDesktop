//using DevExpress.Mvvm;
using BMC.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace BMC.ViewModel
{
    public class HeaterViewModel : BaseViewModel
    {
        #region Private variables        
        private string _selectionItemChanged;
        private List<BaseViewModel> _pageViewModels;
        private string _selectionExtraItemChanged;
        private List<BaseViewModel> _pageViewExtraModels;
        #endregion

        #region Properties / Commands
        public static List<string> HeaterList { get; set; }
        public static List<string> ExtraHeaterList { get; set; }
        public bool? ExtraHeater { get; set; }

        public List<BaseViewModel> PageViewHeaterModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<BaseViewModel>();

                return _pageViewModels;
            }
        }
        public List<BaseViewModel> PageViewExtraHeaterModels
        {
            get
            {
                if (_pageViewExtraModels == null)
                    _pageViewExtraModels = new List<BaseViewModel>();

                return _pageViewExtraModels;
            }
        }
        public BaseViewModel CurrentPageHeaterViewModel
        {
            get; set;
        }
        public BaseViewModel CurrentPageExtraHeaterViewModel
        {
            get; set;
        }
        public string SelectedValueHeaterVar
        {
            get
            {
                return _selectionItemChanged;
            }
            set
            {
                _selectionItemChanged = value;
                if(_selectionItemChanged!=null)
                if (_selectionItemChanged.Contains(HeaterList[0]))
                    ChangeViewModel(PageViewHeaterModels[1]);
                else
                    ChangeViewModel(PageViewHeaterModels[2]);

            }
        }
        public string SelectedExtraHeater
        {
            get
            {
                return _selectionExtraItemChanged;
            }
            set
            {
                _selectionExtraItemChanged = value;
                if (_selectionExtraItemChanged != null)
                    if (_selectionExtraItemChanged.Contains(ExtraHeaterList[0]))
                        ChangeExtraViewModel(PageViewExtraHeaterModels[1]);
                    else
                        ChangeExtraViewModel(PageViewExtraHeaterModels[2]);

            }
        }
        public bool VisibleGrid
        {get;set;
        }

        public LiquidViewModel LiquidVM { get; set; }
        public ElectricalViewModel ElectricalVM { get; set; }

        public LiquidViewModel ExtraLiquid { get; set; }
        public ElectricalViewModel ExtraElectrical { get; set; }
        #endregion

        #region ChangeView/Methods 

        public void ChangeViewModel(BaseViewModel viewModel)
        {
            if (!PageViewHeaterModels.Contains(viewModel))
                PageViewHeaterModels.Add(viewModel);

            CurrentPageHeaterViewModel = PageViewHeaterModels
                .FirstOrDefault(vm => vm == viewModel);
        }
        public void ChangeExtraViewModel(BaseViewModel viewModel)
        {
            if (!PageViewExtraHeaterModels.Contains(viewModel))
                PageViewExtraHeaterModels.Add(viewModel);

            CurrentPageExtraHeaterViewModel = PageViewExtraHeaterModels
                .FirstOrDefault(vm => vm == viewModel);
        }
        public static void SetToNull(HeaterViewModel current)
        {
            current.VisibleGrid = false;
        }
        public static void SetToStandart(HeaterViewModel current)
        {
            current.VisibleGrid = true;
        }     
        
        #endregion

        #region Contstructor
        
        public HeaterViewModel()
        {
            LiquidVM = new LiquidViewModel();
            ElectricalVM = new ElectricalViewModel();
            ExtraLiquid = new LiquidViewModel();
            ExtraElectrical = new ElectricalViewModel();
            HeaterList =Heater.GetHeater();
            ExtraHeaterList = Heater.GetHeater();
            PageViewHeaterModels.Add(new EmptyVM());
            PageViewHeaterModels.Add(LiquidVM);
            PageViewHeaterModels.Add(ElectricalVM);
            PageViewExtraHeaterModels.Add(new EmptyVM());
            PageViewExtraHeaterModels.Add(ExtraLiquid);
            PageViewExtraHeaterModels.Add(ExtraElectrical);
            CurrentPageExtraHeaterViewModel = PageViewExtraHeaterModels[0];
            CurrentPageHeaterViewModel = PageViewHeaterModels[0];
            SelectedValueHeaterVar = HeaterList[0];
            ExtraHeater = false;
        }
        #endregion

        #region Data Methods
        /// <summary>
        /// StringData[0] - Type of heater, after - electrical or liquid properties
        /// First heater, then same the second;
        /// </summary>
        /// <returns></returns>
        public override DataClass GetControlData()
        {
            var heaterResult = new DataClass();
            heaterResult.StringData.Add(SelectedValueHeaterVar);
            if (SelectedValueHeaterVar == HeaterList[1])
            {
                DataClass electricControl = ElectricalVM.GetControlData();
                heaterResult.StringData.AddRange(electricControl.StringData);
                heaterResult.IntData.AddRange(electricControl.IntData);
            }
            else if (SelectedValueHeaterVar==HeaterList[0])
            {
                DataClass liquidControl = LiquidVM.GetControlData();
                heaterResult.StringData.AddRange(liquidControl.StringData);
            }
            if (ExtraHeater == true)
            {
                heaterResult.StringData.Add(SelectedExtraHeater);
                if (SelectedExtraHeater == ExtraHeaterList[1])
                {
                    DataClass electricControl = ExtraElectrical.GetControlData();
                    heaterResult.StringData.AddRange(electricControl.StringData);
                    heaterResult.IntData.AddRange(electricControl.IntData);
                }
                else if (SelectedExtraHeater == ExtraHeaterList[0])
                {
                    DataClass liquidControl = ExtraLiquid.GetControlData();
                    heaterResult.StringData.AddRange(liquidControl.StringData);
                }
            }
            return heaterResult;
        }
        /// <summary>
        /// Transfer of electrical/liquid properties, look into it
        /// First heater, then same the second;
        /// </summary>
        /// <returns></returns>
        public override DataClass GetPowerData()
        {
            var heaterResult = new DataClass();
            if (SelectedValueHeaterVar == HeaterList[1])
            {
                DataClass electricPower = ElectricalVM.GetPowerData();
                heaterResult.StringData.AddRange(electricPower.StringData);
                heaterResult.IntData.AddRange(electricPower.IntData);
            }
            else if (SelectedValueHeaterVar==HeaterList[0])
            {
                DataClass liquidPower = LiquidVM.GetPowerData();
                heaterResult.IntData.AddRange(liquidPower.IntData);
            }
            if (ExtraHeater == true)
            {
                if (SelectedExtraHeater == ExtraHeaterList[1])
                {
                    DataClass electricPower = ExtraElectrical.GetPowerData();
                    heaterResult.StringData.AddRange(electricPower.StringData);
                    heaterResult.IntData.AddRange(electricPower.IntData);
                }
                else if (SelectedExtraHeater == ExtraHeaterList[0])
                {
                    DataClass liquidPower = ExtraLiquid.GetPowerData();
                    heaterResult.IntData.AddRange(liquidPower.IntData);
                }
            }
            return heaterResult;
        }
        #endregion
    }

}
