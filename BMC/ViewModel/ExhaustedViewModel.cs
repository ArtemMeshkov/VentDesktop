using BMC.Model;
using System.Collections.Generic;

namespace BMC.ViewModel
{
    public class ExhaustedViewModel : PageChangeSettings
    {
        public List<Danet> DaNetGateExhausted { get; private set; }
        public List<Danet> DaNetFilterExhausted { get; private set; }
        public bool EnableChangedGate { get; set; }
        private Danet selectedValueGate;
        private Danet selectedValueFilter;
        public Danet SelectedValueFilter
        {
            get { return selectedValueFilter; }

            set
            {
                selectedValueFilter = value;
                EnableChangedFilter = selectedValueFilter.Value;
            }
        }
        public Danet SelectedValueGate
        {
            get { return selectedValueGate; }

            set
            {
                selectedValueGate = value;
                EnableChangedGate = selectedValueGate.Value;
            }
        }
        public bool EnableChangedFilter { get; set; }
        public ExhaustedViewModel()
        {
            DaNetGateExhausted = new List<Danet>(Danet.GetDanets());
            DaNetFilterExhausted = new List<Danet>(Danet.GetDanets());
            SelectedValueGate = DaNetGateExhausted[1];
            SelectedValueFilter = DaNetFilterExhausted[1];
        }
    }
}
