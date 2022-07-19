using System;
using System.Collections.Generic;
using System.Linq;



namespace Zadanie_4
{
    
    
    class Program
    {
        //Zadanie 1
        public static void Solution1()
        {
            int N = Convert.ToInt32(Console.ReadLine());
            var numbers = Enumerable.Range(1, N)
                .Where(num=>num != 5 && num != 9 && (num % 2 != 0 || num % 7 == 0))
                .Select(x=>x*x);

            var q =
                from num in Enumerable.Range(1,N)
                where num != 5 && num != 9 && (num % 2 != 0 || num % 7 == 0)
                select num * num;
            
            Console.WriteLine();
            
            Console.WriteLine("Sum: " + q.Sum());
            Console.WriteLine("Count: " + q.Count());
            Console.WriteLine("First: " + q.First());
            Console.WriteLine("Last: " + q.Last());
            Console.WriteLine("Third: " + q.ElementAt(2));
            
            Console.WriteLine();
            
            Console.WriteLine("Sum: " + numbers.Sum());
            Console.WriteLine("Count: " + numbers.Count());
            Console.WriteLine("First: " + numbers.First());
            Console.WriteLine("Last: " + numbers.Last());
            Console.WriteLine("Third: " + numbers.ElementAt(2));
            
        }

        //Zadanie 2
        public static void Solution2()
        {
            int N = Convert.ToInt32(Console.ReadLine());
            int M = Convert.ToInt32(Console.ReadLine());
            
            Random _rand = new Random();
            
            var randomRow = Enumerable.Range(0, M)
                .Select(r => _rand.Next(10));
            var matrix = Enumerable.Range(0, N)
                .Select(r => randomRow);

            var sum = matrix
                .SelectMany(x => x)
                .Sum();
            
            Console.WriteLine("Sum: " + sum);

        }
        
        //Zadanie 3
        public static void Solution3()
        {
            string input = Console.ReadLine();
            List<String> cityList = new List<string>();
            while (input != "X")
            {
                if(input != "")
                    cityList.Add(input);
                input = Console.ReadLine();
            }

            if (cityList.Count != 0)
            {
                var list = cityList
                    .OrderBy(x => x)
                    .GroupBy(x => x[0])
                    .Select(x => x).ToDictionary(t => t.Key);
                
                input = Console.ReadLine();
                while (input != "exit")
                {
                    if (input != "" && list.ContainsKey(input[0]) && input.Length == 1)
                    {
                        foreach (var s in list[input[0]])
                        {
                            Console.Write(s + " ");
                        }

                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("PUSTE");
                    }

                    input = Console.ReadLine();
                }
            }
        }

        static void Main(string[] args)
        {
            //Solution1();
            //Solution2();
            //Solution3();
        }
    }
}