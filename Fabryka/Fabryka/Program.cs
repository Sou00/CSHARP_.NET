using System;
using System.IO;
using System.Reflection;

namespace Fabryka
{
    class Program
    {
        static void Main(string[] args)
        {
            //Z nieznanego mi powodu proba wywołania programu z konsoli komenda konczy sie
            //errorem ze nie istnieje taki typ w assembly (zlecenieKanapka albo zleceniePiwo).
            //Zmienna Type t bedzie miec wartosc null i program dalej nie zadziala.
            //Probowalem szukac rozwiazan w internecie jednak nie znalazlem nic pomocnego.
            //Zostawiam plik test.bat jednak jedyne co on zrobi to odpali program z cmd nastepnie trzeba
            //podać ręcznie argumenty wejściowe i zakończy on program (bo t jest nullem).
            //Kiedy jednak odpalam program normalnie to w assembly jest widoczny ten typ i wszystko dziala poprawnie.
            //Argumenty wejsciowe to odpowiednio sciezka do classlibrary a potem tytul zadania.
            //W komenatrzach podalem moje sciezki.
            Console.WriteLine("Podaj sciezke");
            var path = Console.ReadLine();
            Console.WriteLine("Podaj tytul");
            var title = Console.ReadLine();
            if (path != null)
            {
                FileInfo f = new FileInfo(path);
                //"C:\Users\Sou\RiderProjects\Fabryka\SandwichProcessor\bin\Debug\net5.0\SandwichProcessor.dll"
                //"C:\Users\Sou\RiderProjects\Fabryka\BeerProcessor\bin\Debug\net5.0\BeerProcessor.dll"
                Assembly assembly = Assembly.LoadFrom(f.FullName);  
            
                Type t;
                if(path.Contains("SandwichProcessor"))
                    t = assembly.GetType("SandwichProcessor.zlecenieKanapka");
                else
                {
                    t = assembly.GetType("BeerProcessor.zleceniePiwo");
                }
                if (t != null)
                {
                    MethodInfo method = t.GetMethod("Process"); 
                    PropertyInfo propertyInfo = t.GetProperty("tytul");
                    object o = Activator.CreateInstance(t);
                    propertyInfo.SetValue(o,title);
                    method.Invoke(o,null); 
                }
            }

            Console.WriteLine("Koniec");
            Console.ReadLine();
        }
    }
}