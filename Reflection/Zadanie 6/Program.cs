using System;
using System.Linq;
using System.Reflection;

namespace Zadanie_6
{
    public class Customer
    {
        private string _name;
        protected int _age;
        public bool isPreferred;
        public Customer(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("Customer name!");
            _name = name;
        }
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public string Address { get; set; }
        public int SomeValue { get; set; }
        public int ImportantCalculation()
        {
            return 1000;
        }
        public void ImportantVoidMethod()
        {
        }
        public enum SomeEnumeration
        {
            ValueOne = 1
            , ValueTwo = 2
        }
        public class SomeNestedClass
        {
            private string _someString;
        }
    }

    
    class Program
    {
        
        static void Main(string[] args)
        {
            Type type = typeof(Customer);
            
            //Zadanie 1
            Console.WriteLine("Zadanie 1");
            Console.WriteLine("Fields:");
            Console.WriteLine("-- Public: ");
            var publicFields = type.GetFields(BindingFlags.Public|BindingFlags.Instance);
            foreach (var field in publicFields)
            {
                Console.WriteLine(field);
            }
            Console.WriteLine("-- Non-public: ");
            var nonpublicFields = type.GetFields(BindingFlags.NonPublic|BindingFlags.Instance);

            foreach (var field in nonpublicFields)
            { 
                Console.WriteLine(field);
            }
            
            Console.WriteLine("Methods: ");
            var methods = type.GetMethods(BindingFlags.Public|BindingFlags.DeclaredOnly|BindingFlags.Instance);
            foreach (var method in methods)
            { 
                Console.WriteLine(method);
            }
            Console.WriteLine("Nested types: ");
            var nested = type.GetNestedTypes();
            foreach (var nest in nested)
            { 
                Console.WriteLine(nest);
            }
            Console.WriteLine("Properties: ");
            var properties = type.GetProperties(BindingFlags.Public|BindingFlags.Instance);
            foreach (var property in properties)
            {
                Console.WriteLine(property);
            }
            Console.WriteLine("Members: ");
            var members = type.GetMembers();
            foreach (var member in members)
            {
                Console.WriteLine(member);
            }
            Console.WriteLine();
            //Zadanie 2

            Console.WriteLine("Zadanie 2");
            var addressProperty = type.GetProperty("Address");
            var someValueProperty = type.GetProperty("SomeValue");
            string s = "abc";
            Customer abc = new Customer("John");
            Console.WriteLine("Address:" + abc.Address);
            Console.WriteLine("Somevalue:" + abc.SomeValue);
            addressProperty.SetValue(abc,"Krakow");
            someValueProperty.SetValue(abc,11);
            Console.WriteLine("Address:" + addressProperty.GetValue(abc));
            Console.WriteLine("Somevalue:" + someValueProperty.GetValue(abc));

        }
    }
}