using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.Model
{
    class ControlChoosingMathModel
    {
        #region Property
        public int Dif { get; set; }
        public int Extra { get; set; }

        public int DI16 { get; set; }
        public int UI16 { get; set; }
        public int AO8 { get; set; }
        public int DO12 { get; set; }

        public int AO { get; set; }
        public int DO { get; set; }
        public int AI { get; set; }
        public int DI { get; set; }
        public int UI { get; set; }

        public int u1 { get; set; }
        public int u2 { get; set; }
        public int AItest { get; set; }
        public int DItest { get; set; }
        public int Spike { get; set; }
        public int Power { get; set; }
        #endregion
        #region Methods
        public static void Schet4(int Vvod, out int result)
        {
            if (Vvod <= 0)
                result = 0;
            else if (Vvod % 4 == 0)
            {
                result = Vvod / 4;
            }
            else
            {
                result = Vvod / 4 + 1;
            }
        }
        public static void Schet8(int Vvod, out int result)
        {

            if (Vvod <= 0)
                result = 0;
            else if (Vvod % 8 == 0)
            {
                result = Vvod / 8;
            }
            else
            {
                result = Vvod / 8 + 1;
            }

        }
        public static void Schet2(int Vvod, out int result)
        {

            if (Vvod <= 0)
                result = 0;
            else if (Vvod % 2 == 0)
            {
                result = Vvod / 2;
            }
            else
            {
                result = Vvod / 2 + 1;
            }

        }
        public static void Schet16(int Vvod, out int result)
        {

            if (Vvod <= 0)
                result = 0;
            else if (Vvod % 16 == 0)
            {
                result = Vvod / 16;
            }
            else
            {
                result = Vvod / 16 + 1;
            }
        }
        public static void Schet12(int Vvod, out int result)
        {

            if (Vvod <= 0)
                result = 0;
            else if (Vvod % 12 == 0)
            {
                result = Vvod / 12;
            }
            else
            {
                result = Vvod / 12 + 1;
            }
        }
        #endregion

        public List<string> GetMathModel(int DI,int DO,int AI,int AO)
        {
            List<string> resultstring = new List<string>();
            ControlChoosingMathModel p = this;
            p.DI = DI;
            p.DO = DO;
            p.AI = AI;
            p.AO = AO;
            Schet8(p.DI, out int lul);
            p.DItest = lul * 8;
            Schet8(p.AI, out int lul2);
            p.AItest = lul2 * 8;
            Schet4(p.AO, out int u2);
            p.u2 = u2;
            Schet4(p.DO, out int u1);
            p.u1 = u1;
            ReMath:
            p.UI = p.u1 * 8 + p.u2 * 8;

            if ((p.UI <= p.AI) && (p.AI != 0))
            {
                int c = p.AI - p.UI;
                if (c > 8)
                {
                    Schet16(c, out int UI16);
                    p.UI16 = UI16;
                }
                else if ((0 <= c) && (c <= 8))
                {
                    Schet8(c, out int mem);
                    p.u1 = mem;
                }

                int DIOst = p.DI - (p.UI16 * 16 + p.u1 * 8 - c);

                Schet16(DIOst, out int DI16);
                p.DI16 = DI16;
            }
            else
            {
                p.Dif = p.UI - p.AI;

                if (p.Dif <= p.DItest)
                {
                    int d = p.DI - p.Dif;
                    Schet16(d, out int DI16);
                    p.DI16 = DI16;

                }
                else if (p.Dif <= p.AItest)
                {
                    int d = p.AI - p.Dif;
                    if (d > 8)
                    {
                        Schet16(d, out int AI16);
                        p.UI16 = AI16;
                    }
                    else if ((0 <= d) && (d <= 8))
                    {
                        Schet8(d, out int mem);
                        p.u1 = mem;
                    }
                }
                else
                {
                    int pls = p.Dif - p.DI;
                    Schet2(pls, out int Extra);
                    p.Extra = Extra;
                    if (p.Extra <= p.AO)
                    {
                        Schet8(p.Extra, out int u3);
                        p.AO8 = u3;
                        int dif = p.AO - p.AO8 * 8;
                        Schet4(dif, out int xd);
                        p.u2 = xd;
                        goto ReMath;
                    }

                    else if (p.Extra - p.AO <= p.DO)
                    {
                        int k = p.DO / 12;
                        Schet4(k, out int d);
                        p.u1 = d;
                        Schet12(p.DO - d * 4, out int a);   /*если что-то сломалось, тут предется думать о занулении ДО*/
                        p.DO12 = a;


                        goto ReMath;
                    }
                    else if (p.Spike == 1)
                    {
                        p.u1 = 0;
                        int ost = p.DO / 4;
                        if (ost != 0)
                        {
                            p.u1 = 1;
                        }
                        Schet12(p.DO - ost, out int u3);
                        p.DO12 = u3;
                        goto ReMath;
                    }
                    else
                    {
                        p.u2 = 0;
                        Schet8(p.AO, out int lamao);
                        p.AO8 = lamao;
                        p.AO = 0;
                        p.UI = p.u1 * 8;
                        p.Spike++;

                        goto ReMath;
                    }

                }

            }
            resultstring.Add("Ваша конфигурация с заданными параметрами:\nСервер автоматизации AS-P");
            if(p.u1!=0)
                resultstring.Add($"UI8/DO4 {p.u1} штук");
            if (p.u2 != 0)
                resultstring.Add($"UI8/AO4 {p.u2} штук");
            if (p.DO12 != 0)
                resultstring.Add($"DO12 {p.DO12} штук");
            if (p.UI16 != 0)
                resultstring.Add($"UI16 {p.UI16} штук ");
            if (p.AO8!=0)
                resultstring.Add($"AO8 {p.AO8} штук");
            if (p.DI16 != 0)
                resultstring.Add($"DI16 {p.DI16} штук");
            return resultstring;

        }

    }

}

