/// Visual Studio 2019

using System;
using System.Collections.Generic;

namespace CsokiFeladat
{
    class Program
    {
        class Nap
        {
            public int nap_nev;
            public int csoki_tipus;
            public int csoki_szam;

            public Nap(int nap_n, int csoki_t, int csoki_sz)
            {
                nap_nev = nap_n;
                csoki_tipus = csoki_t;
                csoki_szam = csoki_sz;
            }
        }

        static string[] nap_nevek = { "hétfő", "kedd", "szerda", "csütörtök", "péntek", "szombat", "vasárnap" };
        static string[] csokik = { "kerek", "szögletes", "hosszú", "rövid", "gömbölyű", "lapos" };
        static int[] arak = { 100, 50, 30, 80, 70, 90 };

        static List<Nap[]> Esetek()
        {
            List<Nap[]> esetek = new List<Nap[]>();
            for (int eset = 0; eset < 7; eset++)
            {
                esetek.Add(new Nap[24]);
                for (int i = 0; i < 24; i++)
                    esetek[eset][i] = new Nap(i + eset - ((i + eset) / 7 * 7), i - (i / csokik.Length * csokik.Length), nap_nevek[i + eset - ((i + eset) / 7 * 7)].Length + i / 7 + 1);
            }
            return esetek;
        }

        static void Main()
        {
            var esetek = Esetek();
            const int enap = 1;    // kedd

            List<int[]> listak = new List<int[]>();
            for (int i = 0; i < esetek.Count; i++)
                listak.Add(new int[csokik.Length]);

            for (int eset = 0; eset < esetek.Count; eset++)
                for (int i = 0; i < listak[eset].Length; i++)
                    listak[eset][esetek[eset][i].csoki_tipus] += esetek[eset][i].csoki_szam;

            int[] eset_arak = new int[esetek.Count];
            for (int i = 0; i < eset_arak.Length; i++)
                eset_arak[i] = 0;
            for (int eset = 0; eset < esetek.Count; eset++)
                for (int i = 0; i < listak[eset].Length; i++)
                    eset_arak[eset] += listak[eset][i] * arak[i];

            Console.WriteLine("a) Picurnak {0} forintot kellett fizetnie az adventi naptárért.", eset_arak[enap]);

            Console.WriteLine("\nb)");
            for (int i = 0; i < esetek[enap].Length; i++)
                Console.WriteLine("december {0}. {1}\t{2}\t{3} darab", i + 1, nap_nevek[esetek[enap][i].nap_nev], csokik[esetek[enap][i].csoki_tipus], esetek[enap][i].csoki_szam);

            Console.WriteLine("\nc)");
            for (int i = 0; i < listak[enap].Length; i++)
                Console.WriteLine("{0}:\t{1} darab", csokik[i], listak[enap][i]);

            Console.WriteLine("\nd) December 1. lehet:");
            int min = int.MaxValue;
            for (int i = 0; i < eset_arak.Length; i++)
                if (eset_arak[i] < min)
                    min = eset_arak[i];

            for (int i = 0; i < eset_arak.Length; i++)
                if (eset_arak[i] == min)
                    Console.WriteLine("{0} ({1} forint)", nap_nevek[i], eset_arak[i]);

            Console.ReadKey();
        }
    }
}
