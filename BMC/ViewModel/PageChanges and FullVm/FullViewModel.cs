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
                PageChangeVentTypes status = PageChangeVentTypes.GetStatus();
                if (status != null)
                {
                    PageChangeVentTypes k = PageChangeVentTypes.GetInstance();
                    if (_selectedValueHeater == "Нет")
                    {
                        HeaterViewModel.SetToNull(k.HeaterVM);
                    }
                    else
                    {
                        HeaterViewModel.SetToStandart(k.HeaterVM);
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
                PageChangeVentTypes status = PageChangeVentTypes.GetStatus();
                if (status != null)
                {
                    PageChangeVentTypes k = PageChangeVentTypes.GetInstance();
                    if (_selectedValueCooler == "Нет")
                        CoolerViewModel.SetToNull(k.CoolerVM);
                    else
                        CoolerViewModel.SetToStandart(k.CoolerVM);
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
                PageChangeVentTypes status = PageChangeVentTypes.GetStatus();
                if (status != null)
                {
                    PageChangeVentTypes k = PageChangeVentTypes.GetInstance();
                    if (_selectedValueHumid == "Нет")
                        HumidViewModel.SetToNull(k.HumidVM);
                    else
                        HumidViewModel.SetToStandart(k.HumidVM);
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
                PageChangeVentTypes status = PageChangeVentTypes.GetStatus();
                if (status != null)
                {
                    PageChangeVentTypes k = PageChangeVentTypes.GetInstance();
                    if (_selectedValueHEx == "Нет")
                        HeatExchangeViewModel.SetToNull(k.HEVM);
                    else
                        HeatExchangeViewModel.SetToStandart(k.HEVM);
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
                PageChangeVentTypes status = PageChangeVentTypes.GetStatus();
                if (status != null)
                {
                    PageChangeVentTypes k = PageChangeVentTypes.GetInstance();
                    if (_selectedValueRecirc == "Нет")
                        RecircViewModel.SetToNull(k.RecircVM);
                    else
                        RecircViewModel.SetToStandart(k.RecircVM);
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
                PageChangeVentTypes status = PageChangeVentTypes.GetStatus();
                if (status != null)
                {
                    PageChangeVentTypes k = PageChangeVentTypes.GetInstance();
                    if (_selectedValueGates == "Нет")
                        GatesViewModel.SetToNull(k.GatesVM);
                    else
                        GatesViewModel.SetToStandart(k.GatesVM);
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
                PageChangeVentTypes status = PageChangeVentTypes.GetStatus();
                if (status != null)
                {
                    PageChangeVentTypes k = PageChangeVentTypes.GetInstance();
                    if (_selectedValueFilter == "Нет")
                        FilterViewModel.SetToNull(k.FilterVM);
                    else
                        FilterViewModel.SetToStandart(k.FilterVM);
                }
            }
        }

        public bool InvisibleForForcedAndExhausted
        {
            get
            {
                _invisForForcedAndExhausted = (PageChangeVentTypes.GetInstance().ForcedIsChecked|| PageChangeVentTypes.GetInstance().ExhaustedIsChecked);
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
                _invisForExhausted = PageChangeVentTypes.GetInstance().ExhaustedIsChecked;
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
                _invisForForced = PageChangeVentTypes.GetInstance().ForcedIsChecked;
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
            PageChangeVentTypes k = PageChangeVentTypes.GetInstance();
            k.TakenData = p;
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
