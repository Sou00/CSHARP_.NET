using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Crawler
{
    static class Globals
    {
        public static bool isCrawling = true; // zmienna ktora jest true dopoki ktorys z crawlerow nie znajdzie sciezki,
                                              // wtedy ustawiana jest na false i reszta konczy prace
        public static List<string> beenTo = new List<string>(); // list odwiedzonych stron zapobiega petli
        public static List<int> results = new List<int>(); //wyniki 

    }
 class Crawler
            {
                private string _url; 
                private int _count; //glebokosc 
                private List<string> _road; //zawiera posrednie linki
                public Crawler(string url, int count, List<string> road)
                {
                    _url = url;
                    _count = count;
                    _road = road;
                }
        
                public Task StartCrawling(bool toCrawl)
                {
                    Task task = Task.Run(() =>
                    {
                        if (toCrawl)
                        {
                            WebClient web = new WebClient();
                            System.IO.Stream stream = web.OpenRead(_url);
                            using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                            {
                                String text = reader.ReadToEnd();
                                var htmlDocument = new HtmlDocument();
                                htmlDocument.LoadHtml(text);
                                
                                //Dodawanie url do drogi
                                //UpdateRoad(htmlDocument);
                                
                                //Szuka elementow <a href> czyli hiperlinkow w elementach <p>
                                var links = htmlDocument.DocumentNode.SelectNodes("//p/a[@href]"); 
                                if(links != null)
                                {
                                    List<String> urlList = new List<string>();

                                    foreach (var link in links)
                                    {
                                        //Wyciaga wartosc hiperlinka z elementu
                                        string hrefValue = link.GetAttributeValue("href", string.Empty);
                                        //Sprawdza czy prowadzi na wikipedie
                                        if (hrefValue.StartsWith("/wiki"))
                                        {
                                            //Sprawdza czy hiperlinki nie dubluja sie na stronie lub czy juz nie odwiedzilismy tego hiperlinku
                                            if (!urlList.Contains(hrefValue) && !Globals.beenTo.Contains(hrefValue))
                                            {
                                                urlList.Add(hrefValue);
                                            }
                                        }
                                    }

                                    if (urlList.Contains("/wiki/Japan"))
                                    {
                                        //Wypisywanie drogi
                                        //PrintRoad();
                                        Globals.results.Add(_count + 1);
                                        Globals.isCrawling = false;
                                    }
                                    else
                                    {
                                        List<Task> taskList = new List<Task>();
                                        taskList.AsParallel();
                                        //Dla kazdego hiperlinka nowy crawler
                                        foreach (var url in urlList)
                                        {
                                            if (!Globals.isCrawling)
                                                break;
                                            List<string> newRoad = new List<string>(_road);
                                            taskList.Add(
                                                new Crawler("https://en.wikipedia.org" + url, _count + 1, newRoad)
                                                    .StartCrawling(Globals.isCrawling));
                                            Globals.beenTo.Add(url);
                                        }
                                    }
                                }
                            }
                        }
                    });
                    return task;
                }

                void UpdateRoad(HtmlDocument htmlDocument)
                {
                    if(_url == "http://en.wikipedia.org/wiki/Special:Random"){
                        var originalUrl = htmlDocument.DocumentNode
                            .Descendants("link").First(x =>
                                x.GetAttributeValue("rel", "").Equals("canonical"))
                            .GetAttributeValue("href", "");
                        _road.Add(originalUrl);
                    }
                    else
                    {
                        _road.Add(_url);
                    }
                }

                void PrintRoad()
                {
                    Console.WriteLine("Success! Found in depth: " + (_count + 1));
                    Console.WriteLine("Road: ");
                    foreach (var path in _road)
                    {
                        Console.WriteLine(path);
                    }
                }
            }
    
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("The crawling device has been activated");
            for (int i = 0; i < 100; i++)
            {
                Crawler crawlie = new Crawler("http://en.wikipedia.org/wiki/Special:Random", 0, new List<string>());
                Globals.isCrawling = true;
                crawlie.StartCrawling(Globals.isCrawling);
                while (Globals.isCrawling)
                {
                
                }
            }
            Console.WriteLine("Average depth for 100 trials: " + Globals.results.Average()); //Puscilem 3 razy, wyniki: 70, 104, 88
        }
        
    }
}