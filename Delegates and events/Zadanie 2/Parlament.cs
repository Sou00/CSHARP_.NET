using System;
using System.Collections.Generic;

namespace Zadanie_2
{
    public class MyEventArgs
    {
        public int positive=0;
        public int negative=0;
        public string topic = "";
    }
    public class Parlament
    {
        public EventHandler VoteEnd;
        public  EventHandler VoteStart;
        private List<Parlamentarist> parList = new List<Parlamentarist>();
        private MyEventArgs result = new MyEventArgs();

        public Parlament(int size)
        {
            for (int i = 0; i < size; i++)
            {
                parList.Add(new Parlamentarist(this));
            }
        }

        public void startVote(string topic)
        {
            result.topic = topic;
            onVoteStart(topic);
            
            string input = Console.ReadLine();
            while(input != "KONIEC")
            {
                if (input.Substring(0,4) == "GLOS")
                {
                    int number = int.Parse(input.Substring(5));
                    if (number <= parList.Count)
                    {
                        //VoteEnd += parList[number-1].Vote;
                        parList[number - 1].Voting += randomVote;
                    }
                    else
                    {
                        Console.WriteLine("Nie ma takiego parlamentarzysty");
                    }
                    
                }
                else
                {
                    Console.WriteLine("Niepoprawne polecenie");
                }

                input = Console.ReadLine();
            }
            onVoteEnd();
        }

        protected virtual void onVoteStart(string topic)
        {
            VoteStart?.Invoke(this,EventArgs.Empty);
        }

        protected virtual void onVoteEnd()
        {
            VoteEnd?.Invoke(this,EventArgs.Empty);
            print();
        }
        
        public void randomVote(object sender, EventArgs e)
        {
            Random r1 = new Random();
            int r = r1.Next() % 2;
            if (r == 1)
            {
                result.positive++;
            }
            else
            {
                result.negative++;
            }
        }

        public void print()
        {
            Console.WriteLine("Głosowanie nad: "+ result.topic);
            Console.WriteLine("Głosy za: "+result.positive);
            Console.WriteLine("Głosy przeciw: "+result.negative);
        }
    }
}