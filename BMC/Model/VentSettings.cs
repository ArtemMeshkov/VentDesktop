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
        public int VentPower { get; set; }
        public DoubleCollection DrivePower { get; set; }
        public string PCH { get; set; }
        public string PCHType { get; set; }

        public static List<int> GetVentPower()
        {
            List<int> result = new List<int> { 380, 220 };
            return result;
        }
        public static List<string> GetPCHTypes()
        {
            List<string> result = new List<string> { "Подача питания", "Внешние контакты", "MODBUS RTU" };
            return result;
        }
        public static DoubleCollection GetDrivePower()
        {
            DoubleCollection result = new DoubleCollection { 0.18, 0.25, 0.37, 0.55, 0.75, 1.1, 1.5, 2.2, 3.0, 4.0, 5.5, 7.5, 11.0, 15.0, 18.0, 22.0, 30.0 };
            return result;
        }
        public List<int> GetPins(VentSettingViewModel p)
        {
            int fDO = 0; int fDI = 0; int fAI = 0; int fAO = 0; int eDI = 0; int eDO = 0; int eAI = 0; int eAO = 0;
            DataClass result = p.GetControlData();
            if (result.StringData[0] == "Да" && result.StringData[1] == "Да")
            {
                // Do += 2; - для пч?
                fDO += 1;
                eDO += 1;
                if (result.StringData[2] == "Да")//forced
                {
                    if (result.StringData[4] == "Подача питания")
                        fDO += 1;
                    else if (result.StringData[4] == "Внешние контакты")
                    {
                        fDO += 1;
                        fDI += 1;
                    }
                    else if (result.StringData[4] == "MODBUS RTU") { }
                    // fDI -= 1;
                }
                if (result.StringData[5] == "Да")//exhausted
                {
                    if (result.StringData[7] == "Подача питания")
                        eDO += 1;
                    else if (result.StringData[7] == "Внешние контакты")
                    {
                        eDO += 1;
                        eDI += 1;
                    }

                    else if (result.StringData[7] == "MODBUS RTU") { }//                    eDI -= 1;
                }
                if (result.StringData[3] == "Да")
                {
                    fDI *= 2;
                    fDO *= 2;
                }
                if (result.StringData[6] == "Да")
                {
                    eDI *= 2;
                    eDO *= 2;
                }
                DO += fDO;
                DO += eDO;
                DI += eDI;
                DI += fDI;
            }
            else if (result.StringData[0] == "Да" && result.StringData[1] == "Нет")
            {
                // DO += 1;  -  для пч?
                fDO += 1;
                if (result.StringData[2] == "Да")//forced
                {
                    if (result.StringData[4] == "Подача питания")
                        fDO += 1;
                    else if (result.StringData[4] == "Внешние контакты")
                    {
                        fDO += 1;
                        fDI += 1;
                    }
                    else if (result.StringData[4] == "MODBUS RTU") { }
                    // fDI -= 1;

                }
                if (result.StringData[3] == "Да")
                {
                    fDI *= 2;
                    fDO *= 2;
                }
                DO += fDO;
                // DO += eDO;
                // DI += eDI;
                DI += fDI;
            }
            else if (result.StringData[0] == "Нет" && result.StringData[1] == "Да")
            {
                eDO += 1;
                if (result.StringData[2] == "Да")//exhausted
                {
                    if (result.StringData[4] == "Подача питания")
                        eDO += 1;
                    else if (result.StringData[4] == "Внешние контакты")
                    {
                        eDO += 1;
                        eDI += 1;
                    }
                    else if (result.StringData[4] == "MODBUS RTU") { }//                    eDI -= 1;
                }
                if (result.StringData[3] == "Да")
                {
                    eDI *= 2;
                    eDO *= 2;
                }
                //DO += fDO;
                DO += eDO;
                DI += eDI;
                // DI += fDI;
            }
            else { }
            List<int> newResult = new List<int> { AO, DO, AI, DI };
            return newResult;

        }


        public List<PowerObject> GetPowerParts(VentSettingViewModel p)
        {
            DataClass result = p.GetPowerData();
            List<PowerObject> PowerResult = new List<PowerObject>();
            if (result.StringData[0] == "Да" && result.StringData[1] == "Да")
            {
                if (result.StringData[3] == "Да")                                   // Начало работы с ПЧ/прямой пуск
                    PowerResult.AddRange(GetPCH(result.IntData[0], result.StringData[2]));
                else
                    PowerResult.AddRange(GetAuto(result.IntData[0], result.StringData[2]));
                if (result.StringData[7]=="Да")
                    PowerResult.AddRange(GetPCH(result.IntData[1], result.StringData[6]));
                else
                    PowerResult.AddRange(GetAuto(result.IntData[1], result.StringData[6]));
                if(result.StringData[4]=="Да")
                {
                    PowerResult[0].Number *= 2;
                    PowerResult[1].Number *= 2;
                    //Тут надо сделать в 2 раза умножить пч/автоматы
                }
                if (result.StringData[8] == "Да")
                {
                    PowerResult[2].Number *= 2;
                    PowerResult[3].Number *= 2;
                    //Тут надо сделать в 2 раза умножить пч/автоматы
                }
            }
            else if (result.StringData[0] == "Да" && result.StringData[1] == "Нет")
            {
                if (result.StringData[3] == "Да")                                   // Начало работы с ПЧ/прямой пуск
                    PowerResult.AddRange(GetPCH(result.IntData[0], result.StringData[2]));
                else
                    PowerResult.AddRange(GetAuto(result.IntData[0], result.StringData[2]));
                if (result.StringData[4] == "Да")
                {
                    PowerResult[0].Number *= 2;
                    PowerResult[1].Number *= 2;
                    //Тут надо сделать в 2 раза умножить пч/автоматы
                }
            }
            else if (result.StringData[0] == "Нет" && result.StringData[1] == "Да")
            {
                if (result.StringData[3] == "Да")                                   // Начало работы с ПЧ/прямой пуск
                    PowerResult.AddRange(GetPCH(result.IntData[0], result.StringData[2]));
                else
                    PowerResult.AddRange(GetAuto(result.IntData[0], result.StringData[2]));
                if (result.StringData[4] == "Да")
                {
                    PowerResult[0].Number *= 2;
                    PowerResult[1].Number *= 2;
                    //Тут надо сделать в 2 раза умножить пч/автоматы
                }
            }

            return PowerResult;
           // return PowerResult;
        }

        #region DataBase
        /// <summary>
        /// ЭТО ТЕСТОВЫЙ ВАРИАНТ
        /// </summary>
        /// <param name="VentPower"></param>
        /// <param name="DrivePower"></param>
        /// <returns></returns>
        /// 
        public static List<PowerObject> GetPCH(int VentPower, string DrivePower)
        {
            List<PowerObject> result = new List<PowerObject>();
            double d = Convert.ToDouble(DrivePower);
            using(var db = new VentContext())
            {
                var atv = db.ATVs212.Where(p=>p.elecpower==VentPower&&p.pumppower>=d).ToList();
                foreach(ATV212 p in atv)
                {
                    result.Add(new PowerObject(p.pch, 1));
                }
            }
            return result;
        }
        private static List<PowerObject> GetAuto(int VentPower, string DrivePower)
        {
            return new List<PowerObject> { };
        }
        #endregion
    } 
}
