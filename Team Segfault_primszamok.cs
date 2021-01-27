//Visual Studio 2017
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TeamSegfaulf_primszamok
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            List<int> lista = new List<int>();
            List<int> primek = new List<int>();
            bool igaz = true;
            int negyzetosszeg=0;
            for (int i = 0; i < 100; i++)
            {
                int szam = rnd.Next(2, 501);
                if (lista.Contains(szam))
                {
                    i--;
                }
                else
                {
                    lista.Add(szam);
                }
            }
            for (int i = 0; i < lista.Count; i++)
            {
                igaz = false;
                for (int j = 2; j < 501; j++)
                {                  
                    if (lista[i] % j != 0)
                    {
                        igaz = true;
                    }
                    else
                    {
                        if (lista[i]!=j)
                        {
                            igaz = false;
                            j = 501;
                        }
                    }
                }
                if (igaz)
                {
                    primek.Add(lista[i]);
                }              
            }
            primek.Sort();
            foreach (var item in primek)
            {
                negyzetosszeg += item * item;
                Console.WriteLine(item);
            }
            Console.WriteLine("prímszámok négyzetösszege: {0}",negyzetosszeg);



            Console.ReadKey();
        }
    }
}
