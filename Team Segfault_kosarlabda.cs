// MonoDevelop 7.8.4 (build 2) [Megegyezik a Visual Studio 201x-el működési szempontból, nincs szükség speciális szoftverre]
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Team_Segfault_kosarlabda
{
    class MainClass
    {
        public struct Match
        {
            public int Turn;
            public string HomeTeam;
            public int HomeScore;
            public int GuestScore;
            public string GuestTeam;
        }

        public static List<Match> Matches = new List<Match>();
        public static string FileNameForRead = "eredmenyek_20210109.txt";

        public static void Main(string[] args)
        {
            ReadIn();
            Console.WriteLine();
            Console.WriteLine("2. feladat - Mérkőzések száma.\n2020/2021-es bajnoki évadban megrendezett mérkőzések száma: {0}\n", Matches.Count());
            Task03();
            Console.WriteLine();
            Task04();
            Console.WriteLine();
            Task05();
            Console.WriteLine();
            Task06();
            Console.WriteLine();
            Task07();
            Console.WriteLine();
            Task06(true);
        }

        public static void ReadIn()
        {
            Console.WriteLine("1. feladat - Fájl beolvasása.");
            string[] all_lines = File.ReadAllLines(FileNameForRead);
            foreach (var item in all_lines)
            {
                string[] seperated = item.Split(' ');
                Match specimen;
                specimen.Turn = Convert.ToInt32(seperated[0]);
                specimen.HomeTeam = seperated[1];
                specimen.HomeScore = Convert.ToInt32(seperated[2]);
                specimen.GuestScore = Convert.ToInt32(seperated[3]);
                specimen.GuestTeam = seperated[4];
                Matches.Add(specimen);
            }
            Console.WriteLine("Fájl beolvasva.");
        }

        public static void Task03()
        {
            Console.WriteLine("3. feladat - A bekért csapat eddig megrendezett és elmaradt mérkőzéseinek száma.");
            Console.Write("Kérem a csapat nevét: ");
            string user_input = Console.ReadLine();
            int all_matches = Matches.Where(x => x.HomeTeam == user_input || x.GuestTeam == user_input).Count();
            int matches_in_queue = Matches.Where(x => (x.HomeTeam == user_input || x.GuestTeam == user_input) && x.GuestScore == 0 && x.HomeScore == 0).Count();
            Console.WriteLine("A {0} csapat {1} mérkőzést játszott le a {2} fordulóból és {3} elmaradt csatája van még.", user_input, all_matches - matches_in_queue, all_matches, matches_in_queue);
            Console.WriteLine("A {0} csapatának elhalasztott mérkőzéseinek párosításai:", user_input);
            foreach (var item in Matches.Where(x => (x.HomeTeam == user_input || x.GuestTeam == user_input) && x.GuestScore == 0 && x.HomeScore == 0).ToList())
            {
                Console.WriteLine("{0}-{1}", item.HomeTeam, item.GuestTeam);
            }
        }

        public static void Task04()
        {
            Console.WriteLine("4. feladat - A meghatározott forduló adatai.");
            Console.Write("Adja meg, melyik forduló adatait szeretné látni? ");
            int user_input = Convert.ToInt32(Console.ReadLine());
            string[] headers = new string[]{ "Forduló", "Hazai csapat", "Vendég csapat", "Dobott pont", "Kapott pont" };
            Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-12} {4,-12}", headers[0], headers[1], headers[2], headers[3], headers[4]);
            int all_score = 0;
            foreach (var item in Matches.Where(x=>x.Turn == user_input && (x.HomeScore != 0 && x.GuestScore != 0)).ToList())
            {
                Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-12} {4,-12}", item.Turn, item.HomeTeam, item.GuestTeam, item.HomeScore, item.GuestScore);
                all_score += item.GuestScore;
                all_score += item.HomeScore;
            }
            Console.WriteLine("Elmaradt mérkőzések:");
            foreach (var item in Matches.Where(x => x.Turn == user_input && x.HomeScore == 0 && x.GuestScore==0).ToList())
            {
                Console.WriteLine("{0}-{1}", item.HomeTeam, item.GuestTeam);
            }
            Console.WriteLine("Összesen dobott pontok száma: {0}", all_score);
            double output_raw = (double)all_score / Matches.Where(x => x.Turn == user_input && (x.HomeScore != 0 && x.GuestScore != 0)).Count();
            Console.WriteLine("Mérkőzésenként dobott pontok átlaga: {0}", Math.Round(output_raw,2));
        }

        public static void Task05()
        {
            Console.WriteLine("5. feladat - A legtöbb pontot dobó és a legkevesebbet kapó csapat pontokkal együtt.");
            Dictionary<string, int> most_give = new Dictionary<string, int>();
            foreach (var item in Matches.GroupBy(x=>x.HomeTeam))
            {
                most_give.Add(item.Key, Matches.Where(x => x.HomeTeam == item.Key).Sum(x => x.HomeScore));
            }
            Console.WriteLine("A legtöbb pontot dobó csapat és pontszáma: {0} {1}", most_give.Select(x => x.Key).Max(), most_give.Select(x => x.Value).Max());

            Dictionary<string, int> least_get = new Dictionary<string, int>();
            foreach (var item in Matches.GroupBy(x => x.GuestTeam))
            {
                least_get.Add(item.Key, Matches.Where(x => x.GuestTeam == item.Key).Sum(x => x.GuestScore));
            }
            Console.WriteLine("A legkevesebb pontot kapó csapat és pontszáma: {0} {1}", least_get.Select(x => x.Key).Min(), least_get.Select(x => x.Value).Min());
        }

        public struct Tabella
        {
            public string TeamName;
            public int MatchesNum;
            public int Wins;
            public int Loses;
            public int GivePoints;
            public int GetPoints;
            public int ActualPoints;
            public int PotentialPoints;
            public double Rate;
        }

        public static void Task06(bool writeout = false)
        {
            if (writeout)
            {
                Console.WriteLine("8. feladat - Tabella kiírása");
            }
            else
            {
                Console.WriteLine("6. feladat - Tabella 2021.01.09.");
            }
            List<Tabella> tabella_data = new List<Tabella>();
            List<string> teams = new List<string>();
            foreach (var item in Matches)
            {
                if (!teams.Contains(item.GuestTeam))
                {
                    teams.Add(item.GuestTeam);
                }
                if (!teams.Contains(item.HomeTeam))
                {
                    teams.Add(item.HomeTeam);
                }
            }

            foreach (var item in teams)
            {
               
                Tabella specimen;
                specimen.TeamName = item;
                specimen.MatchesNum = 0;
                specimen.Wins = 0;
                specimen.Loses = 0;
                specimen.GetPoints = 0;
                specimen.GivePoints = 0;
                foreach (var item2 in Matches.Where(x=>x.GuestTeam == item))
                {
                    if ((item2.HomeScore != 0 && item2.GuestScore != 0))
                    {
                        specimen.MatchesNum++;
                        if (item2.GuestScore > item2.HomeScore)
                        {
                            specimen.Wins++;
                        }
                        else
                        {
                            specimen.Loses++;
                        }
                    }
                }
                foreach (var item2 in Matches.Where(x => x.HomeTeam == item))
                {
                    if ((item2.HomeScore != 0 && item2.GuestScore != 0))
                    {
                        specimen.MatchesNum++;
                        if (item2.HomeScore > item2.GuestScore)
                        {
                            specimen.Wins++;
                        }
                        else
                        {
                            specimen.Loses++;
                        }
                    }
                }
                foreach (var item2 in Matches.Where(x => x.GuestTeam == item))
                {
                    if ((item2.HomeScore != 0 && item2.GuestScore != 0))
                    {
                        specimen.GetPoints += item2.HomeScore;
                        specimen.GivePoints += item2.GuestScore;
                    }
                }
                foreach (var item2 in Matches.Where(x => x.HomeTeam == item))
                {
                    if ((item2.HomeScore != 0 && item2.GuestScore != 0))
                    {
                        if ((item2.HomeScore != 0 && item2.GuestScore != 0))
                        {
                            specimen.GetPoints += item2.GuestScore;
                            specimen.GivePoints += item2.HomeScore;
                        }
                    }
                }

                specimen.ActualPoints = (specimen.Wins * 2) + (specimen.Loses);
                specimen.PotentialPoints = specimen.MatchesNum * 2;
                specimen.Rate = (double)specimen.ActualPoints / (double)specimen.PotentialPoints;
                tabella_data.Add(specimen);
            }
            string[] headers = new string[] { "Helyezés", "Csapatnév", "Mérkőzés", "Győzelem", "Vereség", "Dpont", "Kpont", "Arány" };
            int place = 1;
            double previous = 0;
            if (writeout)
            {
                StreamWriter sw = new StreamWriter("tabella.txt", false);
                sw.WriteLine("{0,-10} {1,-15} {2,-15} {3,-12} {4,-12} {5,-12} {6,-12} {7,-12}", headers[0], headers[1], headers[2], headers[3], headers[4], headers[5], headers[6], headers[7]);
                foreach (var item in tabella_data.OrderByDescending(x => x.Rate))
                {
                    if (previous == item.Rate)
                    {
                        sw.WriteLine("{0,-10} {1,-15} {2,-15} {3,-12} {4,-12} {5,-12} {6,-12} {7,-12}", "", item.TeamName, item.MatchesNum, item.Wins, item.Loses, item.GivePoints, item.GetPoints, Math.Round(item.Rate, 3));
                        place++;
                    }
                    else
                    {
                        sw.WriteLine("{0,-10} {1,-15} {2,-15} {3,-12} {4,-12} {5,-12} {6,-12} {7,-12}", place, item.TeamName, item.MatchesNum, item.Wins, item.Loses, item.GivePoints, item.GetPoints, Math.Round(item.Rate, 3));
                        previous = item.Rate;
                        place++;
                    }
                }
                sw.Close();
                Console.WriteLine("Fájl létrejött.");
            }
            else
            {
                Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-12} {4,-12} {5,-12} {6,-12} {7,-12}", headers[0], headers[1], headers[2], headers[3], headers[4], headers[5], headers[6], headers[7]);
                foreach (var item in tabella_data.OrderByDescending(x => x.Rate))
                {
                    if (previous == item.Rate)
                    {
                        Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-12} {4,-12} {5,-12} {6,-12} {7,-12}", "", item.TeamName, item.MatchesNum, item.Wins, item.Loses, item.GivePoints, item.GetPoints, Math.Round(item.Rate, 3));
                        place++;
                    }
                    else
                    {
                        Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-12} {4,-12} {5,-12} {6,-12} {7,-12}", place, item.TeamName, item.MatchesNum, item.Wins, item.Loses, item.GivePoints, item.GetPoints, Math.Round(item.Rate, 3));
                        previous = item.Rate;
                        place++;
                    }
                }

            }
        }

        public static void Task07()
        {
            Console.WriteLine("7. feladat - A legnagyobb kosárkülönbség és a mérkőzés adatai");
            Match heighest_diff_item = new Match();
            int highest_difference = 0;
            foreach (var item in Matches)
            {
                if(Math.Abs(item.HomeScore - item.GuestScore) > highest_difference)
                {
                    highest_difference = Math.Abs(item.HomeScore - item.GuestScore);
                    heighest_diff_item = item;
                }
            }
            Console.WriteLine("A legnagyobb különbség: {0} pont", highest_difference);
            Console.WriteLine("{0}. forduló {1} {2}:{3} {4}", heighest_diff_item.Turn, heighest_diff_item.HomeTeam, heighest_diff_item.HomeScore, heighest_diff_item.GuestScore, heighest_diff_item.GuestTeam);
        }
    }
}
