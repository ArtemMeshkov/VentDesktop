using System.Collections.Generic;
using System.ComponentModel;
using BMC.ViewModel;


namespace BMC.Model
{
    public class Sbor : BaseViewModel
    {
        public static int DIstat { get; set; } = 0;
        public static int DOstat { get; set; } = 0;
        public static int AIstat { get; set; } = 0;
        public static int AOstat { get; set; } = 0;
        public List<int> FullResult { get; set; }
        public List<int> GetAllControlData()
        {
            FullResult = new List<int> { 0, 0, 0, 0 };
            List<int> result1 = new List<int> { 0, 0, 0, 0 };
            List<int> result2 = new List<int> { 0, 0, 0, 0 };
            List<int> result3 = new List<int> { 0, 0, 0, 0 };
            List<int> result4 = new List<int> { 0, 0, 0, 0 };
            List<int> result5 = new List<int> { 0, 0, 0, 0 };
            List<int> result6 = new List<int> { 0, 0, 0, 0 };
            List<int> result7 = new List<int> { 0, 0, 0, 0 };
            PageChangeVentTypes k = PageChangeVentTypes.GetInstance();
            SettingModel SettingModelInstance = new SettingModel();
            Cooler CoolerModelInstance = new Cooler();
            Heater HeaterModelInstance = new Heater();
            Filters FilterModelInstance = new Filters();
            VentSettings VentModelInstance = new VentSettings();
            Gates GatesModelInstance = new Gates();
            HeatExchange HExModelInstance = new HeatExchange();
            Humid HumidModelInstance = new Humid();
            Recirc RecircModelInstance = new Recirc();
            List<int> result = SettingModelInstance.GetPins(k.SettingVM);
            DataClass SelectionData = k.FullVM.GetControlData();
            List<int> result8 = VentModelInstance.GetPins(k.VentVM);
            if (SelectionData.StringData[1] == "Да")
                result1 = CoolerModelInstance.GetPins(k.CoolerVM);
            if (SelectionData.StringData[0] == "Да")
                result2 = HeaterModelInstance.GetPins(k.HeaterVM);
            if (SelectionData.StringData[6] == "Да")
                result3 = FilterModelInstance.GetPins(k.FilterVM);
            if (SelectionData.StringData[5] == "Да")
                result4 = GatesModelInstance.GetPins(k.GatesVM);
            if (SelectionData.StringData[3] == "Да")
                result5 = HExModelInstance.GetPins(k.HEVM);
            if (SelectionData.StringData[2] == "Да")
                result6 = HumidModelInstance.GetPins(k.HumidVM);
            if (SelectionData.StringData[4] == "Да")
                result7 = RecircModelInstance.GetPins(k.RecircVM);

            for (int i = 0; i <= 3; i++)
                FullResult[i] += result[i] + result1[i] + result2[i] + result3[i] + result4[i] + result5[i] + result6[i] + result7[i]+result8[i];
            List<int> kappa = new List<int> { (FullResult[0] + AOstat), (FullResult[1] + DOstat), (FullResult[2] + AIstat), (FullResult[3] + DIstat) };
            FullResult[0] = kappa[3];
            FullResult[1] = kappa[1];
            FullResult[2] = kappa[2];
            FullResult[3] = kappa[0];
            return FullResult; //Param DI, DO, AI, АO
        }

        public List<string> GetControlParts(int DI,int DO,int AI,int AO)
        {
            GetAllControlData();
            ControlChoosingMathModel mathModel = new ControlChoosingMathModel();
            List<string> newResult = mathModel.GetMathModel(DI, DO, AI, AO);
            return newResult;
        }

        public List<PowerObject> GetAllPowerData()
        {
            return null;
        }
        public Sbor()
        {
            FullResult= new List<int> { 0, 0, 0, 0 };
        }
    }
}
