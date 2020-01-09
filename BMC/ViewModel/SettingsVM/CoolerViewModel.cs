﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using BMC.Model;

namespace BMC.ViewModel
{
    public class CoolerViewModel:BaseViewModel
    {
        private ICommand _changePageCommand;
        private List<BaseViewModel> _pageViewModels;
        private bool? _extraHeaterChecked;
        #region Properties
        public List<int> NumOfStages { get; set; }
        public List<int> PumpPower { get; set; }
        public List<string> CoolerTypes { get; set; }
        public string SelectedType { get; set; }
        public int SelectedNumOfStages { get; set; }
        public int SelectedPumpPower { get; set; }
        public HeaterViewModel ExtraHeater { get; set; }
        public bool? DryerChecked { get; set; } = false;
        public bool? ExtraHeaterChecked
        {
            get
            {
                return _extraHeaterChecked;
            }
            set
            {
                _extraHeaterChecked = value;
                if (_extraHeaterChecked != null)
                    if (_extraHeaterChecked == true)
                    {
                        HeaterViewModel.SetToStandart(ExtraHeater);
                        ChangeViewModel(PageViewExtraHeaterModels[1]);
                    }
                    else
                        ChangeViewModel(PageViewExtraHeaterModels[0]);
            }
        }
        
        public ICommand ChangePageExtraHeaterCommand
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

        public List<BaseViewModel> PageViewExtraHeaterModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<BaseViewModel>();

                return _pageViewModels;
            }
        }

        public BaseViewModel CurrentExtraHeaterViewModel
        {
            get; set;
        }

        public bool VisibleGrid { get; set; }
        #endregion

        #region Methods
        public void ChangeViewModel(BaseViewModel viewModel)
        {
            if (!PageViewExtraHeaterModels.Contains(viewModel))
                PageViewExtraHeaterModels.Add(viewModel);

            CurrentExtraHeaterViewModel = PageViewExtraHeaterModels
                .FirstOrDefault(vm => vm == viewModel);
        }
        public static void SetToNull(CoolerViewModel Current)
        {
            Current.VisibleGrid = false;
        }
        public static void SetToStandart(CoolerViewModel Current)
        {
            Current.VisibleGrid = true;
        }

        #endregion

        #region Constructor
        public CoolerViewModel()
        {
            ExtraHeater = new HeaterViewModel();
            PageViewExtraHeaterModels.Add(new EmptyVM());
            PageViewExtraHeaterModels.Add(ExtraHeater);
            CoolerTypes = Cooler.GetTypes();
            PumpPower = Cooler.GetPumpPower();
            NumOfStages = Cooler.GetNumOfStages();
            SelectedType = CoolerTypes[0];
            SelectedNumOfStages = NumOfStages[0];
            SelectedPumpPower = PumpPower[0];
            ExtraHeaterChecked = false;
        }
        #endregion

        #region Data Methods
        /// <summary>
        /// StringData[0] - selected type,StringData[1] - dryer mode enabled(+2AI), StringData[2]- extra heater enabled
        /// IntData[0]- number of stages if freon chosen , =0 if not
        /// Another IntData and StringData are for ExtraHeater properties
        /// </summary>
        /// <returns></returns>
        public override DataClass GetControlData()
        {
            DataClass result = new DataClass();
            result.StringData.Add(SelectedType);
            result.StringData.Add(BoolToStringConverter.BTS((bool)DryerChecked));
            result.StringData.Add(BoolToStringConverter.BTS((bool)ExtraHeaterChecked));
            if (SelectedType == CoolerTypes[1])
                result.IntData.Add(SelectedNumOfStages);//Only for freon
            else
                result.IntData.Add(0);
            if (ExtraHeaterChecked == true)
            {
                DataClass HeaterResult = ExtraHeater.GetControlData();
                result.StringData.AddRange(HeaterResult.StringData);
                result.IntData.AddRange(HeaterResult.IntData);
            }

            return result;
        }
        /// <summary>
        /// IntData[0] - if water is chosen, pump power, otherwise number of stages
        /// Another IntData and StringData are for ExtraHeater properties
        /// </summary>
        /// <returns></returns>
        public override DataClass GetPowerData()
        {
            DataClass result = new DataClass();
            //result.StringData.Add(BoolToStringConverter.BTS((bool)ExtraHeaterChecked));
            if (SelectedType == CoolerTypes[0])
                result.IntData.Add(SelectedPumpPower);
            else
                result.IntData.Add(SelectedNumOfStages);
            if (ExtraHeaterChecked == true)
            {
                DataClass HeaterResult = ExtraHeater.GetPowerData();
                result.StringData.AddRange(HeaterResult.StringData);
                result.IntData.AddRange(HeaterResult.IntData);
            }
            return result;
        }
        #endregion
    }
}