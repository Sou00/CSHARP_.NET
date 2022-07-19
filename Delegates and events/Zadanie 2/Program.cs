using System;

namespace Zadanie_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Parlament p = new Parlament(5);
            string topic = Console.ReadLine();
            if (topic.Substring(0, 8) == "POCZATEK")
            {
                p.startVote(topic.Substring(9));
            }

            Console.ReadKey();
        }
    }
}