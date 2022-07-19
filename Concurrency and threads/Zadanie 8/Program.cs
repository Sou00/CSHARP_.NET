using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadanie_8
{
     
    class Program
    {
        public static Task PingTask(string a)
        {
            Task task = Task.Run(() =>
            {
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();

                
                options.DontFragment = true;
                
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;
                PingReply reply = pingSender.Send(a, timeout, buffer, options);
                //Console.WriteLine(reply.Status == IPStatus.Success);

            });
            return task;
        }

        static void PingMethod(List<string> list )
        {
            foreach (var s in list)
            {
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                
                options.DontFragment = true;
                
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;
                PingReply reply = pingSender.Send(s, timeout, buffer, options);
               // Console.WriteLine(reply.Status == IPStatus.Success);

            }
            
        }
        
        public static Task SyncPingTask(string a)
        {
            return new Task(() =>
            {
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();

                options.DontFragment = true;
                
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;
                PingReply reply = pingSender.Send(a, timeout, buffer, options);
                //Console.WriteLine(reply.Status == IPStatus.Success);

            });
        }
        
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();

            var lines = File.ReadAllLines("ping.txt");

            //Sekwencyjnie rownolegle
            var size = lines.Length;
            
            List<String> list1 = new List<string>();
            List<String> list2 = new List<string>();
            List<String> list3 = new List<string>();
            List<String> list4 = new List<string>();

            for (int i = 0; i < size; i++)
            {
                if (i < size / 4)
                    list1.Add(lines[i].Split(';')[1]);
                else if(i<size/2)
                    list2.Add(lines[i].Split(';')[1]);
                else if(i<size*3/4)
                    list3.Add(lines[i].Split(';')[1]);
                else
                {
                    list4.Add(lines[i].Split(';')[1]);
                }
            }


            var t1 = new Thread(()=>PingMethod(list1));
            var t2 = new Thread(()=>PingMethod(list2));
            var t3 = new Thread(()=>PingMethod(list3));
            var t4 = new Thread(()=>PingMethod(list4));
            sw.Start();

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();
            
            sw.Stop();
            Console.WriteLine("Sekwencyjnie rownolegle: " + sw.ElapsedMilliseconds + "ms");

            sw.Reset();
            
            //AsParallel
            
            List<Task> tasks = new List<Task>();
            foreach (var s in lines)
            {
                tasks.Add(PingTask(s.Split(';')[1]));
            }
            
            
            sw.Start();
            tasks.AsParallel().WithDegreeOfParallelism(4);
            Task t = Task.WhenAll(tasks);

            t.Wait();
            sw.Stop();
            Console.WriteLine("AsParallel: " + sw.ElapsedMilliseconds + "ms");
            
            sw.Reset();
            
            //RunSynchronously
            List<string> list = new List<string>();
            foreach (var s in lines)
            {
                list.Add(s.Split(';')[1]);
            }

            sw.Start();
            foreach (var s in list)
            {
                SyncPingTask(s).RunSynchronously();
            }
            sw.Stop();
            Console.WriteLine("RunSynchronously: " + sw.ElapsedMilliseconds + "ms");
            sw.Reset();
        }
    }
}