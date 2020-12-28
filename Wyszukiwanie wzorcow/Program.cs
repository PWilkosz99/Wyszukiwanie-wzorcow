using System;
using System.Collections.Generic;
using System.IO;

namespace Wyszukiwanie_wzorcow
{
    class Program
    {
        static void Main(string[] args)
        {
            Z1.Wypisz();

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

            while ((s = sr.ReadLine()) != null)
            {
                if (s.Contains("Part, name=") == true)
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
                if ((s.Contains("*Elset, elset=") || s.Contains("*Nset, nset=")) == true)
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

            while ((s = sr.ReadLine()) != null)
            {
                if (s.Contains("*Element, type=") == true)
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
            Materialy[] m = new Materialy[5];
            while ((s = sr.ReadLine()) != null)
            {
                if (s.Contains("*Material") == true)
                {
                    s = s.Remove(0, s.LastIndexOf("=") + 1);
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
                if (s.Contains("** STEP:") == true)
                {
                    status = true;
                }
                if (s.Contains("** Interaction:") == true)
                {
                    c[0] = true;
                    continue;
                }
                if (s.Contains("** OUTPUT REQUESTS") == true)
                {
                    c[1] = true;
                    continue;
                }
                if (s.Contains("** FIELD OUTPUT:") == true)
                {
                    c[2] = true;
                    continue;
                }
                if (s.Contains("** HISTORY OUTPUT:") == true)
                {
                    c[3] = true;
                    continue;
                }
                if(s.Contains("*End Step") == true)
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
}
