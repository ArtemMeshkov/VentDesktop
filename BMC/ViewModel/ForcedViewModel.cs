//using DevExpress.Mvvm;
using BMC.Model;
using System.Collections.Generic;
//using Prism.Mvvm;

namespace BMC.ViewModel
{
    public class ForcedViewModel : PageChangeSettings
    {
        public List<Danet> DaNetHeaterForced { get; private set; }
        public List<Danet> DaNetCoolerForced { get; private set; }
        public List<Danet> DaNetHumidForced { get; private set; }
        public List<Danet> DaNetGateForced { get; private set; }
        public List<Danet> DaNetFilterForced { get; private set; }
        private Danet selectedValueHeater;
        private Danet selectedValueCooler;
        private Danet selectedValueHumid;
        private Danet selectedValueGate;
        private Danet selectedValueFilter;
        public Danet SelectedValueHeater
        {
            get { return selectedValueHeater; }

            set
            {
                selectedValueHeater = value;
                EnableChangedHeat = selectedValueHeater.Value;
            }
        }
        public bool EnableChangedHeat { get; set; }
        public Danet SelectedValueCooler
        {
            get { return selectedValueCooler; }

            set
            {
                selectedValueCooler = value;
                EnableChangedCooler = selectedValueCooler.Value;
            }
        }
        public bool EnableChangedCooler { get; set; }
        public Danet SelectedValueHumid
        {
            get { return selectedValueHumid; }

            set
            {
                selectedValueHumid = value;
                EnableChangedHumid = selectedValueHumid.Value;
            }
        }
        public bool EnableChangedHumid { get; set; }
        public Danet SelectedValueGate
        {
            get { return selectedValueGate; }

            set
            {
                selectedValueGate = value;
                EnableChangedGate = selectedValueGate.Value;
            }
        }
        public bool EnableChangedGate { get; set; }
        public Danet SelectedValueFilter
        {
            get { return selectedValueFilter; }

            set
            {
                selectedValueFilter = value;
                EnableChangedFilter = selectedValueFilter.Value;
            }
        }
        public bool EnableChangedFilter { get; set; }
        public ForcedViewModel()
        {
            DaNetHeaterForced = new List<Danet>(Danet.GetDanets());
            DaNetCoolerForced = new List<Danet>(Danet.GetDanets());
            DaNetHumidForced = new List<Danet>(Danet.GetDanets());
            DaNetGateForced = new List<Danet>(Danet.GetDanets());
            DaNetFilterForced = new List<Danet>(Danet.GetDanets());
            SelectedValueHeater = DaNetHeaterForced[1];
            SelectedValueCooler = DaNetCoolerForced[1];
            SelectedValueHumid = DaNetHumidForced[1];
            SelectedValueGate = DaNetGateForced[1];
            SelectedValueFilter = DaNetFilterForced[1];

        }
    }
}
