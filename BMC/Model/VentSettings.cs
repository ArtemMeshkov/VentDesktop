using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BMC.ViewModel;
using Npgsql;

namespace BMC.Model
{
    public class VentSettings : Pins
    {
        #region Properties

        //private int VentPower { get; set; }
        //private DoubleCollection DrivePower { get; set; }
        //private string PCH { get; set; }
        //private string PCHType { get; set; }

        #endregion

        #region Get methods
        public static List<int> GetVentPower()
        {
            var result = new List<int> { 380, 220 };
            return result;
        }
        public static List<string> GetPCHTypes()
        {
            var result = new List<string> { "Подача питания", "Внешние контакты", "MODBUS RTU" };
            return result;
        }
        public static DoubleCollection GetDrivePower()
        {
            var result = new DoubleCollection { 0.18, 0.25, 0.37, 0.55, 0.75, 1.1, 1.5, 2.2, 3.0, 4.0, 5.5, 7.5, 11.0, 15.0, 18.0, 22.0, 30.0 };
            return result;
        }
        public List<int> GetPins(VentSettingViewModel ventSettingsVM)
        {
            int fDO = 0;
            int fDI = 0;
            int fAI = 0; 
            int fAO = 0;
            int eDI = 0; 
            int eDO = 0;
            int eAI = 0;
            int eAO = 0;
            DataClass ventControlData = ventSettingsVM.GetControlData();
            if (ventControlData.StringData[0] == "Да" && ventControlData.StringData[1] == "Да")
            {
                // Do += 2; - для пч?
                fDO += 1;
                eDO += 1;
                if (ventControlData.StringData[2] == "Да")//forced
                {
                    if (ventControlData.StringData[4] == "Подача питания")
                        fDO += 1;
                    else if (ventControlData.StringData[4] == "Внешние контакты")
                    {
                        fDO += 1;
                        fDI += 1;
                    }
                    else if (ventControlData.StringData[4] == "MODBUS RTU") { }
                    // fDI -= 1;
                }
                if (ventControlData.StringData[5] == "Да")//exhausted
                {
                    if (ventControlData.StringData[7] == "Подача питания")
                        eDO += 1;
                    else if (ventControlData.StringData[7] == "Внешние контакты")
                    {
                        eDO += 1;
                        eDI += 1;
                    }

                    else if (ventControlData.StringData[7] == "MODBUS RTU") { }//  eDI -= 1;
                }
                if (ventControlData.StringData[3] == "Да")
                {
                    fDI *= 2;
                    fDO *= 2;
                }
                if (ventControlData.StringData[6] == "Да")
                {
                    eDI *= 2;
                    eDO *= 2;
                }
                DO += fDO;
                DO += eDO;
                DI += eDI;
                DI += fDI;
            }
            else if (ventControlData.StringData[0] == "Да" && ventControlData.StringData[1] == "Нет")
            {
                // DO += 1;  -  для пч?
                fDO += 1;
                if (ventControlData.StringData[2] == "Да")//forced
                {
                    if (ventControlData.StringData[4] == "Подача питания")
                        fDO += 1;
                    else if (ventControlData.StringData[4] == "Внешние контакты")
                    {
                        fDO += 1;
                        fDI += 1;
                    }
                    else if (ventControlData.StringData[4] == "MODBUS RTU") { }
                }
                if (ventControlData.StringData[3] == "Да")
                {
                    fDI *= 2;
                    fDO *= 2;
                }
                DO += fDO;
                DI += fDI;
            }
            else if (ventControlData.StringData[0] == "Нет" && ventControlData.StringData[1] == "Да")
            {
                eDO += 1;
                if (ventControlData.StringData[2] == "Да")//exhausted
                {
                    if (ventControlData.StringData[4] == "Подача питания")
                        eDO += 1;
                    else if (ventControlData.StringData[4] == "Внешние контакты")
                    {
                        eDO += 1;
                        eDI += 1;
                    }
                    else if (ventControlData.StringData[4] == "MODBUS RTU") { }//                    eDI -= 1;
                }
                if (ventControlData.StringData[3] == "Да")
                {
                    eDI *= 2;
                    eDO *= 2;
                }
                DO += eDO;
                DI += eDI;
            }
            else { }
            var newResult = new List<int> { AO, DO, AI, DI };
            return newResult;
        }

        public List<PowerObject> GetPowerParts(VentSettingViewModel ventSettingVM)
        {
            var ventPowerParts = new List<PowerObject>();
            DataClass ventPowerData = ventSettingVM.GetPowerData();            
            if (ventPowerData.StringData[0] == "Да" && ventPowerData.StringData[1] == "Да")
            {
                if (ventPowerData.StringData[3] == "Да")                                   // Начало работы с ПЧ/прямой пуск
                    ventPowerParts.AddRange(GetPCH(ventPowerData.IntData[0], ventPowerData.StringData[2]));
                else
                    ventPowerParts.AddRange(GetAuto(ventPowerData.IntData[0], ventPowerData.StringData[2]));
                if (ventPowerData.StringData[7]=="Да")
                    ventPowerParts.AddRange(GetPCH(ventPowerData.IntData[1], ventPowerData.StringData[6]));
                else
                    ventPowerParts.AddRange(GetAuto(ventPowerData.IntData[1], ventPowerData.StringData[6]));
                if(ventPowerData.StringData[4]=="Да")
                {
                    ventPowerParts[0].Number *= 2;
                    ventPowerParts[1].Number *= 2;
                    //Тут надо сделать в 2 раза умножить пч/автоматы
                }
                if (ventPowerData.StringData[8] == "Да")
                {
                    ventPowerParts[2].Number *= 2;
                    ventPowerParts[3].Number *= 2;
                    //Тут надо сделать в 2 раза умножить пч/автоматы
                }
            }
            else if (ventPowerData.StringData[0] == "Да" && ventPowerData.StringData[1] == "Нет")
            {
                if (ventPowerData.StringData[3] == "Да")                                   // Начало работы с ПЧ/прямой пуск
                    ventPowerParts.AddRange(GetPCH(ventPowerData.IntData[0], ventPowerData.StringData[2]));
                else
                    ventPowerParts.AddRange(GetAuto(ventPowerData.IntData[0], ventPowerData.StringData[2]));
                if (ventPowerData.StringData[4] == "Да")
                {
                    ventPowerParts[0].Number *= 2;
                    ventPowerParts[1].Number *= 2;
                    //Тут надо сделать в 2 раза умножить пч/автоматы
                }
            }
            else if (ventPowerData.StringData[0] == "Нет" && ventPowerData.StringData[1] == "Да")
            {
                if (ventPowerData.StringData[3] == "Да")                                   // Начало работы с ПЧ/прямой пуск
                    ventPowerParts.AddRange(GetPCH(ventPowerData.IntData[0], ventPowerData.StringData[2]));
                else
                    ventPowerParts.AddRange(GetAuto(ventPowerData.IntData[0], ventPowerData.StringData[2]));
                if (ventPowerData.StringData[4] == "Да")
                {
                    ventPowerParts[0].Number *= 2;
                    ventPowerParts[1].Number *= 2;
                    //Тут надо сделать в 2 раза умножить пч/автоматы
                }
            }

            return ventPowerParts;
        }

        #endregion

        #region DataBase methods

        /// <summary>
        /// ЭТО ТЕСТОВЫЙ ВАРИАНТ
        /// </summary>
        /// <param name="ventPower"></param>
        /// <param name="drivePower"></param>
        /// <returns></returns>
        /// 
        public static List<PowerObject> GetPCH(int ventPower, string drivePower)
        {
            var result = new List<PowerObject>();
            double pumpPower = Convert.ToDouble(drivePower);
            using(var db = new VentContext())
            {
                var atv = db.ATVs212.Where(p=>p.ElecPower==ventPower&&p.PumpPower>=pumpPower).ToList();
                foreach(var p in atv)
                {
                    result.Add(new PowerObject(p.PCH, 1));
                }
            }
            return result;
        }
        private static List<PowerObject> GetAuto(int ventPower, string drivePower)
        {
            return new List<PowerObject> { };
        }

        #endregion
    } 
}
