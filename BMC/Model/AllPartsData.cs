using System.Collections.Generic;
using System.ComponentModel;
using BMC.ViewModel;


namespace BMC.Model
{
    public class AllPartsData : BaseViewModel
    {
        #region Properties
        public static int DIstat { get; set; } = 0;
        public static int DOstat { get; set; } = 0;
        public static int AIstat { get; set; } = 0;
        public static int AOstat { get; set; } = 0;
        public List<int> FullResult { get; set; }
        #endregion

        #region Get Methods
        public List<int> GetAllControlData()
        {
            FullResult = new List<int> { 0, 0, 0, 0 };
            var result1 = new List<int> { 0, 0, 0, 0 };
            var result2 = new List<int> { 0, 0, 0, 0 };
            var result3 = new List<int> { 0, 0, 0, 0 };
            var result4 = new List<int> { 0, 0, 0, 0 };
            var result5 = new List<int> { 0, 0, 0, 0 };
            var result6 = new List<int> { 0, 0, 0, 0 };
            var result7 = new List<int> { 0, 0, 0, 0 };
            MainWindowVM instanceMainWindow = MainWindowVM.GetInstance();
            var settingModelInstance = new SettingModel();
            var coolerModelInstance = new Cooler();
            var heaterModelInstance = new Heater();
            var filterModelInstance = new Filters();
            var ventModelInstance = new VentSettings();
            var gatesModelInstance = new Gates();
            var heatExchangeModelInstance = new HeatExchange();
            var humidModelInstance = new Humid();
            var recircModelInstance = new Recirc();
            List<int> result = settingModelInstance.GetPins(instanceMainWindow.SettingVM);
            DataClass selectedParts = instanceMainWindow.FullVM.GetControlData();

            List<int> result8 = ventModelInstance.GetPins(instanceMainWindow.VentVM);
            if (selectedParts.StringData[1] == "Да")
                result1 = coolerModelInstance.GetPins(instanceMainWindow.CoolerVM);
            if (selectedParts.StringData[0] == "Да")
                result2 = heaterModelInstance.GetPins(instanceMainWindow.HeaterVM);
            if (selectedParts.StringData[6] == "Да")
                result3 = filterModelInstance.GetPins(instanceMainWindow.FilterVM);
            if (selectedParts.StringData[5] == "Да")
                result4 = gatesModelInstance.GetPins(instanceMainWindow.GatesVM);
            if (selectedParts.StringData[3] == "Да")
                result5 = heatExchangeModelInstance.GetPins(instanceMainWindow.HeatExchangeVM);
            if (selectedParts.StringData[2] == "Да")
                result6 = humidModelInstance.GetPins(instanceMainWindow.HumidVM);
            if (selectedParts.StringData[4] == "Да")
                result7 = recircModelInstance.GetPins(instanceMainWindow.RecircVM);

            for (int i = 0; i <= 3; i++)
                FullResult[i] += result[i] + result1[i] + result2[i] + result3[i] + result4[i] + result5[i] + result6[i] + result7[i]+result8[i];
            var swappingList = new List<int> { (FullResult[0] + AOstat), (FullResult[1] + DOstat), (FullResult[2] + AIstat), (FullResult[3] + DIstat) };
            FullResult[0] = swappingList[3];
            FullResult[1] = swappingList[1];
            FullResult[2] = swappingList[2];
            FullResult[3] = swappingList[0];
            return FullResult; //Param DI, DO, AI, АO
        }

        public List<string> GetControlParts(int DI,int DO,int AI,int AO)
        {
            GetAllControlData();
            var mathModel = new ControlChoosingMathModel();
            List<string> newResult = mathModel.GetMathModel(DI, DO, AI, AO);
            return newResult;
        }

        public List<PowerObject> GetAllPowerData()
        {
            return null;
        }
        #endregion

        #region Constructor
        public AllPartsData()
        {
            FullResult= new List<int> { 0, 0, 0, 0 };
        }
        #endregion
    }
}
