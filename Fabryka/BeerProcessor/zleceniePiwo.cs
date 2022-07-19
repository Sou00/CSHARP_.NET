using System;
using System.Threading;
using Common;

namespace BeerProcessor
{
    public class zleceniePiwo : Izlecenie
    {
        public string tytul { get; set; }
        
        public void Process()
        {
            Console.WriteLine(tytul);
            Console.WriteLine("Wytwarzanie piwa");
            Thread.Sleep(2000);
            Console.WriteLine("Transport piwa do sklepu");
            Thread.Sleep(2000);
            Console.WriteLine("Kupno i przyniesienie piwa do domu");
            Thread.Sleep(2000);
            Console.WriteLine("Wyciagniecie schlodzonego piwa z lodowki");
            Thread.Sleep(2000);
            Console.WriteLine("Otwarcie butelki i ostrozne przelanie do kufla");
            Thread.Sleep(2000);
        }
    }
}