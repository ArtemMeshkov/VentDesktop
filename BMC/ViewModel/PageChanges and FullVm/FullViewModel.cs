using BMC.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace BMC.ViewModel
{
    public class FullViewModel:BaseViewModel
    {
        #region Private variables

        private bool _invisForForced;
        private bool _invisForExhausted;
        private bool _invisForForcedAndExhausted;
        private ICommand _transferData;
        private List<int> _num;
        private string _selectedValueHeater;
        private string _selectedValueCooler;
        private string _selectedValueHumid;
        private string _selectedValueHEx;
        private string _selectedValueRecirc;
        private string _selectedValueGates;
        private string _selectedValueFilter;
        #endregion

        #region Properties/Command to TransferData
        public List<string> DaNetHeaterFull { get; private set; }
        public List<string> DaNetCoolerFull { get; private set; }
        public List<string> DaNetHumidFull { get; private set; }
        public List<string> DaNetHeatExchangeFull { get; private set; }
        public List<string> DaNetRecircFull { get; private set; }
        public List<string> DaNetGateFull { get; private set; }
        public List<string> DaNetFilterFull { get; private set; }

        public string SelectedValueHeater
        {
            get
            { return _selectedValueHeater; }
            set
            {
                _selectedValueHeater = value;
                MainWindowVM status = MainWindowVM.GetStatus();
                if (status != null)
                {
                    MainWindowVM instance = MainWindowVM.GetInstance();
                    if (_selectedValueHeater == "Нет")
                    {
                        HeaterViewModel.SetToNull(instance.HeaterVM);
                    }
                    else
                    {
                        HeaterViewModel.SetToStandart(instance.HeaterVM);
                    }
                }
            }
        }
        public string SelectedValueCooler
        {
            get
            {
                return _selectedValueCooler;
            }
            set
            {
                _selectedValueCooler = value;
                MainWindowVM status = MainWindowVM.GetStatus();
                if (status != null)
                {
                    MainWindowVM instance = MainWindowVM.GetInstance();
                    if (_selectedValueCooler == "Нет")
                        CoolerViewModel.SetToNull(instance.CoolerVM);
                    else
                        CoolerViewModel.SetToStandart(instance.CoolerVM);
                }
            }
        }
        public string SelectedValueHumid
        {
            get
            {
                return _selectedValueHumid;
            }
            set
            {
                _selectedValueHumid = value;
                MainWindowVM status = MainWindowVM.GetStatus();
                if (status != null)
                {
                    MainWindowVM instance = MainWindowVM.GetInstance();
                    if (_selectedValueHumid == "Нет")
                        HumidViewModel.SetToNull(instance.HumidVM);
                    else
                        HumidViewModel.SetToStandart(instance.HumidVM);
                }
            }
        }
        public string SelectedValueHeatExchange
        {
            get
            {
                return _selectedValueHEx;
            }
            set
            {
                _selectedValueHEx = value;
                MainWindowVM status = MainWindowVM.GetStatus();
                if (status != null)
                {
                    MainWindowVM instance = MainWindowVM.GetInstance();
                    if (_selectedValueHEx == "Нет")
                        HeatExchangeViewModel.SetToNull(instance.HeatExchangeVM);
                    else
                        HeatExchangeViewModel.SetToStandart(instance.HeatExchangeVM);
                }
            }
        }
        public string SelectedValueRecirc
        {
            get
            {
                return _selectedValueRecirc;
            }
            set
            {
                _selectedValueRecirc = value;
                MainWindowVM status = MainWindowVM.GetStatus();
                if (status != null)
                {
                    MainWindowVM instance = MainWindowVM.GetInstance();
                    if (_selectedValueRecirc == "Нет")
                        RecircViewModel.SetToNull(instance.RecircVM);
                    else
                        RecircViewModel.SetToStandart(instance.RecircVM);
                }
            }
        }
        public string SelectedValueGate
        {
            get
            {
                return _selectedValueGates;
            }
            set
            {
                _selectedValueGates = value;
                MainWindowVM status = MainWindowVM.GetStatus();
                if (status != null)
                {
                    MainWindowVM instance = MainWindowVM.GetInstance();
                    if (_selectedValueGates == "Нет")
                        GatesViewModel.SetToNull(instance.GatesVM);
                    else
                        GatesViewModel.SetToStandart(instance.GatesVM);
                }
            }
        }
       
        public string SelectedValueFilter
        {
            get
            {
                return _selectedValueFilter;
            }
            set
            {
                _selectedValueFilter = value;
                MainWindowVM status = MainWindowVM.GetStatus();
                if (status != null)
                {
                    MainWindowVM instance = MainWindowVM.GetInstance();
                    if (_selectedValueFilter == "Нет")
                        FilterViewModel.SetToNull(instance.FilterVM);
                    else
                        FilterViewModel.SetToStandart(instance.FilterVM);
                }
            }
        }

        public bool InvisibleForForcedAndExhausted
        {
            get
            {
                _invisForForcedAndExhausted = (MainWindowVM.GetInstance().ForcedIsChecked|| MainWindowVM.GetInstance().ExhaustedIsChecked);
                return _invisForForcedAndExhausted;
            }
            set
            {
                _invisForForcedAndExhausted = value;                 
            }
        }
        public bool InvisibleForExhausted
        {
            get
            {
                _invisForExhausted = MainWindowVM.GetInstance().ExhaustedIsChecked;
                return _invisForExhausted;
            }
            set
            {
                _invisForExhausted = value;
            }
        }
        public bool InvisibleForForced
        {
            get
            {
                _invisForForced = MainWindowVM.GetInstance().ForcedIsChecked;
                return _invisForForced;
            }
            set
            {
                _invisForForced = value;
            }
        }
        public List<int> Num
        {
            get
            {
                if (_num == null)
                    _num = new List<int>();
                return _num;

            }
            set
            { _num = value; }
        }
        public ICommand TransferData
        {
            get
            {
                if (_transferData == null)
                {
                    _transferData = new DelegateCommand(
                        p =>TransferIntoMain((int)p),p=>p is int);
                }

                return _transferData;
            }
        }

        #endregion

        #region Methods
        public void TransferIntoMain(int p)
        {
            MainWindowVM instance = MainWindowVM.GetInstance();
            instance.TakenData = p;
        }
                       
        private static List<string> GetVariants()
        {
            var result = new List<string> { "Да", "Нет" };
            return result;
        }
        #endregion

        #region Constructor
        public FullViewModel()
        {
            DaNetHeaterFull = GetVariants();
            DaNetCoolerFull = GetVariants();
            DaNetHumidFull = GetVariants();
            DaNetHeatExchangeFull = GetVariants();
            DaNetRecircFull = GetVariants();
            DaNetGateFull = GetVariants();
            DaNetFilterFull = GetVariants();
            SelectedValueHeater = DaNetHeaterFull[1];
            SelectedValueCooler = DaNetCoolerFull[1];
            SelectedValueHumid = DaNetHumidFull[1];
            SelectedValueHeatExchange = DaNetHeatExchangeFull[1];
            SelectedValueRecirc = DaNetRecircFull[1];
            SelectedValueGate = DaNetGateFull[1];
            SelectedValueFilter = DaNetFilterFull[1];
            Num.Add(1);
            Num.Add(2);
            Num.Add(3);
            Num.Add(4);
            Num.Add(5);
            Num.Add(6);
            Num.Add(7);
            Num.Add(8);
            Num.Add(9);
        }
        #endregion

        #region Data Methods
        /// <summary>
        /// This method gives us the information about our main parts of a system
        /// </summary>
        /// <returns></returns>
        public override DataClass GetControlData()
        {
            DataClass result = new DataClass();
            result.StringData.Add(SelectedValueHeater);
            result.StringData.Add(SelectedValueCooler);
            result.StringData.Add(SelectedValueHumid);
            result.StringData.Add(SelectedValueHeatExchange);
            result.StringData.Add(SelectedValueRecirc);
            result.StringData.Add(SelectedValueGate);
            result.StringData.Add(SelectedValueFilter);
            return result;
        }
        #endregion
    }
}
