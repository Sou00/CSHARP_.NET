using System;
using System.IO;
using System.Threading;
using Common;

namespace SandwichProcessor
{
    public class zlecenieKanapka : Izlecenie
    {
        public string tytul { get; set; }
        
        public void Process()
        {
            Console.WriteLine(tytul);
            Console.WriteLine("Piecznie chleba");
            Thread.Sleep(1000); 
            Console.WriteLine("Krojenie chleba");
            Thread.Sleep(1000);
            Console.WriteLine("Smarowanie maslem");
            Thread.Sleep(1000);
            Console.WriteLine("Nakladanie dodatkow");
            Thread.Sleep(1000);
        }
    }
}