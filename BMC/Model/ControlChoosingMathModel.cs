using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.Model
{
    class ControlChoosingMathModel
    {
        #region Properties

        private int Dif { get; set; }
        private int Extra { get; set; }
        private int DI16 { get; set; }
        private int UI16 { get; set; }
        private int AO8 { get; set; }
        private int DO12 { get; set; }
        private int AO { get; set; }
        private int DO { get; set; }
        private int AI { get; set; }
        private int DI { get; set; }
        private int UI { get; set; }
        private int U1 { get; set; }
        private int U2 { get; set; }
        private int AItest { get; set; }
        private int DItest { get; set; }
        private int Extra2 { get; set; }

        #endregion

        #region Methods

        public static void Count4(int input, out int result)
        {
            if (input <= 0)
                result = 0;
            else if (input % 4 == 0)
            {
                result = input / 4;
            }
            else
            {
                result = input / 4 + 1;
            }
        }
        public static void Count8(int input, out int result)
        {

            if (input <= 0)
                result = 0;
            else if (input % 8 == 0)
            {
                result = input / 8;
            }
            else
            {
                result = input / 8 + 1;
            }

        }
        public static void Count2(int input, out int result)
        {

            if (input <= 0)
                result = 0;
            else if (input % 2 == 0)
            {
                result = input / 2;
            }
            else
            {
                result = input / 2 + 1;
            }

        }
        public static void Count16(int input, out int result)
        {

            if (input <= 0)
                result = 0;
            else if (input % 16 == 0)
            {
                result = input / 16;
            }
            else
            {
                result = input / 16 + 1;
            }
        }
        public static void Count12(int input, out int result)
        {

            if (input <= 0)
                result = 0;
            else if (input % 12 == 0)
            {
                result = input / 12;
            }
            else
            {
                result = input / 12 + 1;
            }
        }
        #endregion

        public List<string> GetMathModel(int DI,int DO,int AI,int AO)
        {
            List<string> resultString = new List<string>();
            ControlChoosingMathModel p = this;
            p.DI = DI;
            p.DO = DO;
            p.AI = AI;
            p.AO = AO;
            Count8(p.DI, out int param1);
            p.DItest = param1 * 8;
            Count8(p.AI, out int param2);
            p.AItest = param2 * 8;
            Count4(p.AO, out int u2);
            p.U2 = u2;
            Count4(p.DO, out int u1);
            p.U1 = u1;
            ReMath:
            p.UI = p.U1 * 8 + p.U2 * 8;

            if ((p.UI <= p.AI) && (p.AI != 0))
            {
                int c = p.AI - p.UI;
                if (c > 8)
                {
                    Count16(c, out int UI16);
                    p.UI16 = UI16;
                }
                else if ((0 <= c) && (c <= 8))
                {
                    Count8(c, out int param3);
                    p.U1 = param3;
                }

                int DIOst = p.DI - (p.UI16 * 16 + p.U1 * 8 - c);

                Count16(DIOst, out int DI16);
                p.DI16 = DI16;
            }
            else
            {
                p.Dif = p.UI - p.AI;

                if (p.Dif <= p.DItest)
                {
                    int d = p.DI - p.Dif;
                    Count16(d, out int DI16);
                    p.DI16 = DI16;

                }
                else if (p.Dif <= p.AItest)
                {
                    int d = p.AI - p.Dif;
                    if (d > 8)
                    {
                        Count16(d, out int AI16);
                        p.UI16 = AI16;
                    }
                    else if ((0 <= d) && (d <= 8))
                    {
                        Count8(d, out int param4);
                        p.U1 = param4;
                    }
                }
                else
                {
                    int pls = p.Dif - p.DI;
                    Count2(pls, out int Extra);
                    p.Extra = Extra;
                    if (p.Extra <= p.AO)
                    {
                        Count8(p.Extra, out int u3);
                        p.AO8 = u3;
                        int dif = p.AO - p.AO8 * 8;
                        Count4(dif, out int param5);
                        p.U2 = param5;
                        goto ReMath;
                    }

                    else if (p.Extra - p.AO <= p.DO)
                    {
                        int k = p.DO / 12;
                        Count4(k, out int d);
                        p.U1 = d;
                        Count12(p.DO - d * 4, out int a);   /*если что-то сломалось, тут предется думать о занулении DO*/
                        p.DO12 = a;
                        goto ReMath;
                    }
                    else if (p.Extra2 == 1)
                    {
                        p.U1 = 0;
                        int left = p.DO / 4;
                        if (left != 0)
                        {
                            p.U1 = 1;
                        }
                        Count12(p.DO - left, out int u3);
                        p.DO12 = u3;
                        goto ReMath;
                    }
                    else
                    {
                        p.U2 = 0;
                        Count8(p.AO, out int param6);
                        p.AO8 = param6;
                        p.AO = 0;
                        p.UI = p.U1 * 8;
                        p.Extra2++;

                        goto ReMath;
                    }

                }

            }
            resultString.Add("Ваша конфигурация с заданными параметрами:\nСервер автоматизации AS-P");
            if(p.U1!=0)
                resultString.Add($"UI8/DO4 {p.U1} штук");
            if (p.U2 != 0)
                resultString.Add($"UI8/AO4 {p.U2} штук");
            if (p.DO12 != 0)
                resultString.Add($"DO12 {p.DO12} штук");
            if (p.UI16 != 0)
                resultString.Add($"UI16 {p.UI16} штук ");
            if (p.AO8!=0)
                resultString.Add($"AO8 {p.AO8} штук");
            if (p.DI16 != 0)
                resultString.Add($"DI16 {p.DI16} штук");
            return resultString;

        }

    }

}

