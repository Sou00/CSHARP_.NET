using System;
using System.Linq;
using System.Text.RegularExpressions;


namespace Zadanie_5
{
    public class MergeString
    {
        public string Merge(string a, string b)
        {
            if (a == null || b == null)
            {
                return null;
            }

            return a + b;
        }
    }

    public class MergeStringWithEx
    {
        public string Merge(string a, string b)
        {
            if (a == null || b == null)
            {
                throw new ArgumentNullException();
            }

            return a + b;
        }
    }

    public interface IAnagramChecker
    {
        bool IsAnagram(string w1, string w2);
    }

    public class AnagramChecker : IAnagramChecker
    {
        public bool IsAnagram(string w1, string w2)
        {
            if (w1 == null || w2 == null || w1 == "" || w2 == "")
            {
                return false;
            }

            Regex reg = new Regex(@"[^a-zA-Z0-9]");
            w1 = reg.Replace(w1, "");
            w2 = reg.Replace(w2, "");

            w1 = w1.ToLower();
            w2 = w2.ToLower();

            w1 = String.Concat(w1.OrderBy(x => x));
            w2 = String.Concat(w2.OrderBy(x => x));
            
            if (w1 == w2)
            {
                return true;
            }

            return false;
        }
    }

    public interface IDiscountFromPeselComputer
    {
        bool HasDiscount(string pesel);
    }

    public class DiscountFromPeselComputer : IDiscountFromPeselComputer
    {
        public bool HasDiscount(string pesel)
        {
            if (pesel == null)
                throw new InvalidPeselException("Pesel cannot be null");
            if (pesel == "")
                throw new InvalidPeselException("Pesel cannot be empty");
            if (pesel.Length != 11)
                throw new InvalidPeselException("Wrong pesel length");
            Regex reg = new Regex(@"[^0-9]");
            if (reg.IsMatch(pesel))
                throw new InvalidPeselException("Incorrect signs in the pesel");

            int year = Int32.Parse(pesel.Substring(0, 2));
            int month = Int32.Parse(pesel.Substring(2, 2));
            if (year > 3 && month > 20)
                return true;
            if (year < 56 && month is < 13 or > 80)
                return true;
            return false;

        }
    }

    public class InvalidPeselException : Exception
    {
        private string message { get; }
        public InvalidPeselException(string message)
        {
            this.message = message;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}