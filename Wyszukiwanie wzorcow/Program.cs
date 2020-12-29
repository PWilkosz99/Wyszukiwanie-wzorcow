using System;
using System.Collections.Generic;
using System.IO;

namespace Wyszukiwanie_wzorcow
{
    class Program
    {
        static void Main(string[] args)
        {
            //Z1.Wypisz();

            //Z2.Wypisz();

            //Z3.Wypisz();

            //Z4.Wypisz();

            //Z5.Wypisz();
        }
    }


    class Z1
    {
        public static List<string> Find()
        {
            string path = @"Job-case1.inp";
            StreamReader sr = File.OpenText(path);

            List<string> list = new List<string>();
            string s;
            int h;
            while ((s = sr.ReadLine()) != null)
            {
                if (( h = BM.BoyerMooreHorspoolSearch("Part, name=", s) ) > 0)
                {
                    s = s.Remove(0, h + 11);
                    list.Add(s);
                }
            }
            sr.Close();
            return list;
        }

        public static void Wypisz()
        {
            List<string> list = Find();
            foreach(string s in list)
            {
                Console.WriteLine(s);
            }
        }
    }
    class Z2
    {
        public static List<string> Find()
        {
            string path = @"Job-case1.inp";
            StreamReader sr = File.OpenText(path);

            List<string> list = new List<string>();
            string s;

            while ((s = sr.ReadLine()) != null)
            {
                if (((BM.BoyerMooreHorspoolSearch("*Elset, elset=", s) > -1) || (BM.BoyerMooreHorspoolSearch("*Nset, nset=", s) > -1)) == true)
                {
                    s = s.Remove(0, s.LastIndexOf("=") + 1);
                    list.Add(s);
                }
            }
            sr.Close();
            return list;
        }

        public static void Wypisz()
        {
            List<string> list = Find();
            foreach (string s in list)
            {
                Console.WriteLine(s);
            }
        }
    }

    class Z3
    {
        public static List<string> Find()
        {
            string path = @"Job-case1.inp";
            StreamReader sr = File.OpenText(path);

            List<string> list = new List<string>();
            string s;
            int h;
            while ((s = sr.ReadLine()) != null)
            {
                if ((h = BM.BoyerMooreHorspoolSearch("*Element, type=", s)) > -1)
                {
                    s = s.Remove(0, h + 15);
                    list.Add(s);
                }
            }
            sr.Close();
            return list;
        }

        public static void Wypisz()
        {
            List<string> list = Find();
            foreach (string s in list)
            {
                Console.WriteLine(s);
            }
        }
    }
    class Z4
    {
        public struct Materialy
        {
            public string Nazwa;
            public double Conductivity;
            public double Density;
            public double Specific_Heat;
        }

        public static Materialy[] Find()
        {
            string path = @"Job-case1.inp";
            StreamReader sr = File.OpenText(path);

            List<string> list = new List<string>();
            string s;
            int i=0;
            int j=0;
            bool inc=false;
            int h;
            Materialy[] m = new Materialy[5];
            while ((s = sr.ReadLine()) != null)
            {
                if ((h = BM.BoyerMooreHorspoolSearch("*Material", s)) > -1)
                {
                    s = s.Remove(0, h + 16);
                    m[i].Nazwa = s;
                    inc = true;
                }
                if (j == 2)
                {
                    s = s.TrimEnd(',');
                    s = s.Replace('.', ',');
                    //Console.WriteLine(s);
                    m[i].Conductivity= Double.Parse(s);
                }
                if (j == 4)
                {
                    s = s.TrimEnd(',');
                    s = s.Replace('.', ',');
                    m[i].Density = Double.Parse(s);
                }
                if (j == 6)
                {
                    s = s.TrimEnd(',');
                    s = s.Replace('.', ',');
                    m[i].Specific_Heat = Double.Parse(s);
                    inc = false;
                    j = 0;
                    i++;
                }
                if(inc){
                    j++;
                }
            }
            sr.Close();
            return m;
        }

        public static void Wypisz()
        {
            Materialy[] tab = Find();
            foreach (Materialy s in tab)
            {
                Console.WriteLine(s.Nazwa);
                Console.WriteLine(s.Conductivity);
                Console.WriteLine(s.Density);
                Console.WriteLine(s.Specific_Heat);
            }
        }
    }
    class Z5
    {
        public static bool Find()
        {
            string path = @"Job-case1.inp";
            StreamReader sr = File.OpenText(path);

            string s;
            bool[] c = new bool[4];
            bool status = false;
            bool corr=false;
            while ((s = sr.ReadLine()) != null)
            {
                if ((BM.BoyerMooreHorspoolSearch("** STEP:", s)) > -1)
                {
                    status = true;
                }
                if ((BM.BoyerMooreHorspoolSearch("** Interaction:", s)) > -1)
                {
                    c[0] = true;
                    continue;
                }
                if ((BM.BoyerMooreHorspoolSearch("** OUTPUT REQUESTS", s)) > -1)
                {
                    c[1] = true;
                    continue;
                }
                if ((BM.BoyerMooreHorspoolSearch("** FIELD OUTPUT:", s)) > -1)
                {
                    c[2] = true;
                    continue;
                }
                if ((BM.BoyerMooreHorspoolSearch("** HISTORY OUTPUT:", s)) > -1)
                {
                    c[3] = true;
                    continue;
                }
                if ((BM.BoyerMooreHorspoolSearch("*End Step", s)) > -1)
                {
                    if((status == true && c[0] == true && c[1] == true && c[2] == true && c[3] == true)||
                        status == true && c[0] == false && c[1] == false && c[2] == false && c[3] == false)
                    {
                        corr = true;
                        break;
                    }
                    status = false;
                    c[0] = false;
                    c[1] = false;
                    c[2] = false;
                    c[3] = false;
                }
            }
            sr.Close();
            return corr;
        }

        public static void Wypisz()
        {
            bool c = Find();
            if (c)
            {
                Console.WriteLine("Step został zdefiniowany poprawny");
            }
            else
            {
                Console.WriteLine("Stop został zdefiniwany błednie");
            }
        }


    }

    class BM
    {
        public static int BoyerMooreHorspoolSearch(string ptrn, string txt)
        {
            char[] pattern = ptrn.ToCharArray();
            char[] text = txt.ToCharArray();

            int[] shift = new int[256];

            for (int k = 0; k < 256; k++)
            {
                shift[k] = pattern.Length;
            }

            for (int k = 0; k < pattern.Length - 1; k++)
            {
                shift[pattern[k]] = pattern.Length - 1 - k;
            }

            int i = 0, j = 0;

            while ((i + pattern.Length) <= text.Length)
            {
                j = pattern.Length - 1;

                while (text[i + j] == pattern[j])
                {
                    j -= 1;
                    if (j < 0)
                        return i;
                }

                i = i + shift[text[i + pattern.Length - 1]];
            }
            return -1;
        }
    }
}
