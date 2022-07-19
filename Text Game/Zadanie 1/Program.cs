using System;
using System.Collections.Generic;
using System.Linq;

namespace Zadanie_1
{
    class Program
    {
        enum EHeroClass
        {
            Rycerz,
            Paladyn,
            Mag,
            Druid
        }
        class Hero
        {
            private string heroName;
            private EHeroClass heroClass;

            public Hero(string heroName, EHeroClass heroClass)
            {
                this.heroClass = heroClass;
                this.heroName = heroName;
            }

            public string getName()
            {
                return heroName;
            }
            
            public void TalkTo(NPC npc, DialogParser parser)
            {
                NpcDialogPart npcDialog = npc.StartTalking();
                string npcName = npc.getName();
                List<string> output = new List<string>();
                Console.Clear();
                while (npcDialog != null)
                {
                    output.Add(npcName + ": " + parser.ParseDialog(npcDialog));
                    Console.WriteLine(npcName + ": " + parser.ParseDialog(npcDialog));
                    
                    if (npcDialog.getDialogList() != null)
                    {
                        List<HeroDialogPart> dialogList = npcDialog.getDialogList();
                        int it = 1;
                        List<string> temp = new List<string>();
                        foreach (var dial in dialogList)
                        {
                            Console.WriteLine("[" + it + "] "+ parser.ParseDialog(dial));
                            temp.Add(it.ToString());
                            it++;
                        }

                        var input = Console.ReadLine();
                        while (!temp.Contains(input))
                        {
                            Console.Clear();
                            Console.WriteLine("Wybierz poprawna opcje");
                            Console.WriteLine(npcName + ": " + parser.ParseDialog(npcDialog));
                            foreach (var dial in dialogList)
                            {
                                Console.WriteLine("[" + it + "] "+ parser.ParseDialog(dial));
                                temp.Add(it.ToString());
                                it++;
                            }
                            input = Console.ReadLine();
                        }
                        Console.Clear();
                        
                        HeroDialogPart heroDialog = dialogList[input[0] - 49];
                        output.Add(heroName + ": " + parser.ParseDialog(heroDialog));
                        foreach (var s in output)
                        {
                            Console.WriteLine(s);
                        }
                        npcDialog = heroDialog.getNpcDialog();
                    }
                    else
                    {
                        npcDialog = null;
                    }
                }
                Console.WriteLine("Koniec");
                Console.ReadKey();
                Console.Clear();
                
            }

        }

        class NPC
        {
            private string name;
            private NpcDialogPart firstMessage;

            public NPC(string name, NpcDialogPart dialog)
            {
                this.name = name;
                firstMessage = dialog;
            }

            public string getName()
            {
                return name;
            }
            public NpcDialogPart StartTalking()
            {
                return firstMessage;
            }
        }

        interface IDialogPart
        {
            string getDialog();
        }
        class NpcDialogPart:IDialogPart
        {
            private string sentence;
            private List<HeroDialogPart> dialogList;

            public NpcDialogPart(string s, List<HeroDialogPart> heroDialog)
            {
                sentence = s;
                dialogList = heroDialog;
            }

            public string getDialog()
            {
                return sentence;
            }

            public List<HeroDialogPart> getDialogList()
            {
                return dialogList;
            }


        }

        class HeroDialogPart:IDialogPart
        {
            private string sentence;
            private NpcDialogPart npcResponse;
            
            public HeroDialogPart(string s, NpcDialogPart npcDialog)
            {
                sentence = s;
                npcResponse = npcDialog;
            }
            public string getDialog()
            {
                return sentence;
            }

            public NpcDialogPart getNpcDialog()
            {
                return npcResponse;
            }
        }

        class Location
        {
            private string name;
            private List<NPC> npcList;

            public Location(string name, List<NPC> npc)
            {
                this.name = name;
                npcList = npc;
            }

            public void addNpc(NPC npc)
            {
                npcList.Add(npc);
            }
            public void removeNpc(NPC npc)
            {
                npcList.Remove(npc);
            }
            public string getName()
            {
                return name;
            }
            public List<NPC> getList()
            {
                return npcList;
            }
        }

        static Hero HeroCreation()
        {
            Console.Clear();
            Console.WriteLine("Proszę podać nazwę bohatera");
            string heroName = Console.ReadLine();
            heroName = heroName.Trim();
            bool result = heroName.All(Char.IsLetter);
            while (heroName.Length < 2 || !result)
            {
                Console.Clear();
                Console.WriteLine("Niepoprawna nazwa, proszę spróbować ponownie");
                heroName = Console.ReadLine();
                heroName = heroName.Trim();
                result = heroName.All(Char.IsLetter);
            }
            Console.Clear();
            Console.WriteLine("Witaj "+ heroName + ", wybierz klasę bohatera");
            Console.WriteLine("1. Rycerz");
            Console.WriteLine("2. Paladyn");
            Console.WriteLine("3. Mag");
            Console.WriteLine("4. Druid");
            var input = Console.ReadLine();
            while (input != "1" && input != "2" && input != "3" && input != "4")
            {
                Console.Clear();
                Console.WriteLine("Wybierz poprawna opcje");
                input = Console.ReadLine();
            }
            EHeroClass heroClass = EHeroClass.Rycerz;
            if (input == "1")
            {
                heroClass = EHeroClass.Rycerz;
            }

            if (input == "2")
            {
                heroClass = EHeroClass.Paladyn;
            }

            if (input == "3")
            {
                heroClass = EHeroClass.Mag;
            }

            if (input == "4")
            {
                heroClass = EHeroClass.Druid;
            }
            
            Console.Clear();
            Console.WriteLine(heroClass + " " + heroName + " wyrusza na przygode");
            return new Hero(heroName, heroClass);
        }
        
        static string ChooseCity()
        {
            string[] cityList =  {"Thais", "Venore", "Carlin", "Ab'dendriel","Kazordoon"};
            Console.WriteLine("Wybierz miasto do ktorego chcesz sie udac");
            Console.WriteLine("1. Thais");
            Console.WriteLine("2. Venore");
            Console.WriteLine("3. Carlin");
            Console.WriteLine("4. Ab'dendriel");
            Console.WriteLine("5. Kazordoon");
            var input = Console.ReadLine();
            while (input != "1" && input != "2" && input != "3" && input != "4" && input != "5")
            {
                Console.Clear();
                Console.WriteLine("Wybierz poprawna opcje");
                input = Console.ReadLine();
            }
            Console.Clear();
            string city = cityList[input[0] - 49];
            return city;
        }
        
        static void ShowLocation(Location location)
        {
            Console.WriteLine("Znajdujesz sie w "+ location.getName() +". Co chcesz robic?");
            int it = 1;
            foreach (var npc in location.getList())
            {
                Console.WriteLine("["+it+"] Porozmawiaj z "+ npc.getName());
                it++;
            }
            Console.WriteLine("[x] Zamknij program");
        }

        static NpcDialogPart Dialog1()
        {
            NpcDialogPart t1 = new NpcDialogPart("Dziekuje.", null);
            List<HeroDialogPart> hList1 = new List<HeroDialogPart>();
            HeroDialogPart h1 = new HeroDialogPart("OK, moze byc 100 sztuk zlota.", t1);
            HeroDialogPart h2 = new HeroDialogPart("W takim razie radz sobie sam.", null);
            hList1.Add(h1);
            hList1.Add(h2);
            NpcDialogPart t2 = new NpcDialogPart("Niestety nie mam wiecej. Jestem bardzo biedny.", hList1);
            HeroDialogPart h3 = new HeroDialogPart("Dam znac jak bede gotowy.", null);
            HeroDialogPart h4 = new HeroDialogPart("100 sztuk zlota to za malo!", t2);
            List<HeroDialogPart> hList2 = new List<HeroDialogPart>();
            hList2.Add(h3);
            hList2.Add(h4);
            NpcDialogPart t3 = new NpcDialogPart("Dziekuje! W nagrode otrzymasz ode mnie 100 sztuk zlota.", hList2);
            HeroDialogPart h5 = new HeroDialogPart("Tak, chetnie pomoge.", t3);
            HeroDialogPart h6 = new HeroDialogPart("Nie, nie pomoge, zegnaj.", null);
            List<HeroDialogPart> hList3 = new List<HeroDialogPart>();
            hList3.Add(h5);
            hList3.Add(h6);
            NpcDialogPart t4 = new NpcDialogPart("Witaj, czy mozesz mi pomoc dostac sie do innego miasta?", hList3);
            return t4;
        }

        static NpcDialogPart Dialog2()
        {
            NpcDialogPart t1 = new NpcDialogPart("WOW! Milo poznac!", null);
            List<HeroDialogPart> hList1 = new List<HeroDialogPart>();
            HeroDialogPart h1 = new HeroDialogPart("Tak, jestem #HERONAME#", t1);
            HeroDialogPart h2 = new HeroDialogPart("Nie.", null);
            hList1.Add(h1);
            hList1.Add(h2);
            NpcDialogPart t2 = new NpcDialogPart("Hej czy to Ty jestes tym slynnym #HERONAME# - pogromca smokow?", hList1);
            return t2;
        }

        static NpcDialogPart Dialog3()
        {
            NpcDialogPart t2 = new NpcDialogPart("Prosze bardzo. Zapraszam ponownie", null);
            HeroDialogPart h3 = new HeroDialogPart("Biore miecz!", t2);
            HeroDialogPart h4 = new HeroDialogPart("Biore mosiezna tarcze!", t2);
            HeroDialogPart h5 = new HeroDialogPart("Biore cwiekowany pancerz!", t2);
            HeroDialogPart h6 = new HeroDialogPart("Rozmyslilem sie.", null);

            List<HeroDialogPart> hList2 = new List<HeroDialogPart>();
            hList2.Add(h3);
            hList2.Add(h4);
            hList2.Add(h5);
            hList2.Add(h6);
            NpcDialogPart t3 = new NpcDialogPart("Oto one: \nMiecz - 85 sztuk zlota\nMosiezna tarcza - 65 sztuk zlota\nCwiekowany pancerz - 90 sztuk zlota", hList2);
            HeroDialogPart h7 = new HeroDialogPart("Tak.", t3);
            HeroDialogPart h8 = new HeroDialogPart("Nie.", null);
            List<HeroDialogPart> hList3 = new List<HeroDialogPart>();
            hList3.Add(h7);
            hList3.Add(h8);
            NpcDialogPart t4 = new NpcDialogPart("Witaj, czy chcialbys obejrzec moje towary?", hList3);
            return t4;
        }

        class DialogParser
        {
            private Hero hero;
            public DialogParser(Hero hero)
            {
                this.hero = hero;
            }

            public string ParseDialog(IDialogPart dialog)
            {
                string s = dialog.getDialog();
                string[] words = s.Split(' ');
                s = "";

                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i] == "#HERONAME#")
                    {
                        words[i] = hero.getName();
                    }

                    s += words[i] + " ";
                }
                
                return s;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Witaj w grze Tibia");
            Console.WriteLine("[1] Zacznij nową grę");
            Console.WriteLine("[X] Zamknij program");
            var input=Console.ReadLine();
            while (input != "x" && input != "1")
            {
                Console.Clear();
                Console.WriteLine("Wybierz poprawna opcje");
                Console.WriteLine("Witaj w grze Tibia");
                Console.WriteLine("[1] Zacznij nową grę");
                Console.WriteLine("[X] Zamknij program");
                input = Console.ReadLine();
            }
            if (input == "1")
            {
                Hero hero = HeroCreation();
                string city = ChooseCity();
                
                List<NPC> npcList = new List<NPC>();
                NPC tom = new NPC("Tom",Dialog1());
                npcList.Add(tom);
                NPC baxter = new NPC("Baxter", Dialog2());
                npcList.Add(baxter);
                NPC sam = new NPC("Sam", Dialog3());
                npcList.Add(sam);
                Location location = new Location(city, npcList);
                ShowLocation(location);
                input = Console.ReadLine();
                DialogParser parser = new DialogParser(hero);
                bool chatted = false;
                while (input != "x")
                {
                    if (input == "1")
                    {
                        hero.TalkTo(npcList[0],parser);
                        chatted = true;
                    }
                    else if (input == "2")
                    {
                        hero.TalkTo(npcList[1],parser);
                        chatted = true;
                    }
                    else if (input == "3")
                    {
                        hero.TalkTo(npcList[2],parser);
                        chatted = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Wybierz poprawna opcje");
                        ShowLocation(location);
                        input = Console.ReadLine();
                        chatted = false;
                    }

                    if (chatted)
                    {
                        ShowLocation(location);
                        input = Console.ReadLine();
                    }
                }
            }

            Console.ReadKey();
        }
    }
}