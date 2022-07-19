using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Zadanie_5;
using Cw05_Przyklad;
using IDiscountFromPeselComputer = Cw05_Przyklad.IDiscountFromPeselComputer;
using InvalidPeselException = Zadanie_5.InvalidPeselException;

namespace Testy
{
    public class Tests
    {
        //Zadanie 1
        [Test]
        public void Test1()
        {
            MergeString stringMerger = new MergeString();

            string s1 = "abc";
            string s2 = "def";
            string res1 = stringMerger.Merge(s1, s2);
            string res2 = stringMerger.Merge(s1, null);
            string res3 = stringMerger.Merge(s1, "");
            string res4 = stringMerger.Merge("", "");
            
            Assert.AreEqual(s1+s2,res1);
            Assert.Null(res2);
            Assert.AreEqual(s1,res3);
            Assert.AreEqual("",res4);
        }
        //Zadanie 2
        [Test]
        public void Test2()
        {
            MergeStringWithEx stringMerger = new MergeStringWithEx();
            
            string s1 = "abc";
            string s2 = "def";
            string res1 = stringMerger.Merge(s1, s2);
            string res3 = stringMerger.Merge(s1, "");
            string res4 = stringMerger.Merge("", "");

            Assert.AreEqual(s1 + s2, res1);
            Assert.Throws<ArgumentNullException>( () => stringMerger.Merge(s1, null));
            Assert.AreEqual(s1,res3);
            Assert.AreEqual("",res4);
            
        }
        //Zadanie 3
        [TestFixture]
        public class TestAnagramChecker : IAnagramChecker
        {
            public bool IsAnagram(string w1, string w2)
            {
                if (w1 == null || w2 == null || w1 == "" || w2 == "")
                {
                    return false;
                }
                if (w1 == w2)
                {
                    return true;
                }

                if (w1 == "abc" && w2 == "cab")
                    return true;
                return false;
            }
        }
        [Test]
        public void Test3()
        {
            //Z tego co rozumiem mam testowac interfejs bez znania poprawnego algorytmu
            //wiec stworzylem klase implementujaca ktora posiada na sztywno
            //wpisane poprawne zwracane wartosci dla okreslonych danych wejsciowcyh
            //wtedy jesli zmienimy konstruktor tej klasy textowej na wlasciwa
            //implementacje te testy powinny przechodzic, jednak rozbudowanie testow
            //bedzie wymagac dopisanie tych przypadkow do tej testowej klasy
            IAnagramChecker iAnagramChecker = new TestAnagramChecker();
            string s1 = "abc";
            string s2 = "def";
            string s3 = "cab";
            bool res1 = iAnagramChecker.IsAnagram(s1, s2);
            bool res2 = iAnagramChecker.IsAnagram(s1, null);
            bool res3 = iAnagramChecker.IsAnagram(s1, "");
            bool res4 = iAnagramChecker.IsAnagram("", "");
            bool res5 = iAnagramChecker.IsAnagram("", null);
            bool res6 = iAnagramChecker.IsAnagram(null,null );
            bool res7 = iAnagramChecker.IsAnagram(s1, s3);
            
            Assert.False(res1);
            Assert.False(res2);
            Assert.False(res3);
            Assert.False(res4);
            Assert.False(res5);
            Assert.False(res6);
            Assert.True(res7);
            
            iAnagramChecker = new AnagramChecker();
            
            res1 = iAnagramChecker.IsAnagram(s1, s2);
            res2 = iAnagramChecker.IsAnagram(s1, null);
            res3 = iAnagramChecker.IsAnagram(s1, "");
            res4 = iAnagramChecker.IsAnagram("", "");
            res5 = iAnagramChecker.IsAnagram("", null);
            res6 = iAnagramChecker.IsAnagram(null,null );
            res7 = iAnagramChecker.IsAnagram(s1, s3);

            Assert.False(res1);
            Assert.False(res2);
            Assert.False(res3);
            Assert.False(res4);
            Assert.False(res5);
            Assert.False(res6);
            Assert.True(res7);
            
        }
        
        //Zadanie 4
        [TestFixture]
        public class TestDiscountComputer : IDiscountFromPeselComputer
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
                if (pesel == "04301201457" || pesel == "5502100346")
                    return true;
                return false;
            }
        }
        [Test]
        public void Test4()
        {
            // Testy dla interfejsu, tak jak w zadaniu 3
            IDiscountFromPeselComputer testDiscountComputer = new TestDiscountComputer();
            string s1 = "00301202345"; // 21 lat
            string s2 = "def"; // zle znaki
            string s3 = "04301201457";//17 lat
            string s4 = "0430120145";//za krotki
            string s5 = "72021003462";//59 lat
            string s6 = "55021003461";//66 lat
            
            Assert.False(testDiscountComputer.HasDiscount(s1)); // ma 21 lat
            Assert.False(testDiscountComputer.HasDiscount(s5)); // ma 59 lat
            Assert.True(testDiscountComputer.HasDiscount(s3)); // ma 17 lat
            Assert.True(testDiscountComputer.HasDiscount(s3)); // ma 66 lat

            Assert.Throws<InvalidPeselException>( () => testDiscountComputer.HasDiscount(s2)); //zle znaki
            Assert.Throws<InvalidPeselException>( () => testDiscountComputer.HasDiscount(s4)); //za krotki
            Assert.Throws<InvalidPeselException>( () => testDiscountComputer.HasDiscount(null)); //null
            Assert.Throws<InvalidPeselException>( () => testDiscountComputer.HasDiscount("")); //pusty

            // testy dla przykladu
            testDiscountComputer = new Cw05_Przyklad.DiscountFromPeselComputer();
            
            
            //Assert.False(testDiscountComputer.HasDiscount(s1)); // ma 21 lat // Year, Month, and Day parameters describe an un-representable DateTime.
            //Assert.False(testDiscountComputer.HasDiscount(s5)); // ma 59 lat  //to samo co w 1
            //Assert.True(testDiscountComputer.HasDiscount(s3)); // ma 17 lat  //to samo co w 1
            //Assert.True(testDiscountComputer.HasDiscount(s3)); // ma 66 lat  //to samo co w 1
            
            //Wniosek: nie przechodza testy w ktorych dane wejsciowe sa poprawne, dla mojego zestawu testow jest to 4/8 
            
            Assert.Throws<Cw05_Przyklad.InvalidPeselException>( () => testDiscountComputer.HasDiscount(s2)); //zle znaki
            Assert.Throws<Cw05_Przyklad.InvalidPeselException>( () => testDiscountComputer.HasDiscount(s4)); //za krotki
            Assert.Throws<Cw05_Przyklad.InvalidPeselException>( () => testDiscountComputer.HasDiscount(null)); //null
            Assert.Throws<Cw05_Przyklad.InvalidPeselException>( () => testDiscountComputer.HasDiscount("")); //pusty
            
            // Testy dla implementacji
            Zadanie_5.IDiscountFromPeselComputer testDiscountComputer1 = new Zadanie_5.DiscountFromPeselComputer();
            
            Assert.False(testDiscountComputer1.HasDiscount(s1)); // ma 21 lat
            Assert.False(testDiscountComputer1.HasDiscount(s5)); // ma 59 lat
            Assert.True(testDiscountComputer1.HasDiscount(s3)); // ma 17 lat
            Assert.True(testDiscountComputer1.HasDiscount(s3)); // ma 66 lat
            Assert.Throws<InvalidPeselException>( () => testDiscountComputer1.HasDiscount(s2)); //zle znaki
            Assert.Throws<InvalidPeselException>( () => testDiscountComputer1.HasDiscount(s4)); //za krotki
            Assert.Throws<InvalidPeselException>( () => testDiscountComputer1.HasDiscount(null)); //null
            Assert.Throws<InvalidPeselException>( () => testDiscountComputer1.HasDiscount("")); //pusty
        }
    }
}