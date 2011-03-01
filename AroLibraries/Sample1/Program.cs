using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AroLibraries.ExtensionMethods;
using AroLibraries.ExtensionMethods.Enumerable;
using AroLibraries.ExtensionMethods.Objects;
using AroLibraries.ExtensionMethods.Strings;
using AroLibraries.ExtensionMethods.Strings.Validator;

namespace Sample1
{
    class Program
    {
        static void Main(string[] args)
        {
            Sample1();
            Sample2();
            Sample3();
            Sample4();
            Sample5();
        }

        private static void Sample2()
        {
            var numbers2 = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var numbers = 1.IterateTo(10); //same as new[] {1, 2, 3, 4, 5, 6, 7, 8, 9};

            var index = numbers.IndexOf(6); //return number of index nr 5
            var numbors2x1 = numbers.Repeat(2, true); //{1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 6, 7, 8, 9}
            var numbors2x2 = numbers.Repeat(2, false); // {1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9}
            var numbersRandom = numbers.Shuffle(); //Randomise collections
            9.Ext_In(numbers); //Checks if 9 is in colection
            numbers.ToString(";"); // "1; 2; 3; 4; 5; 6; 7; 8; 9"

            ConsoleExt.WriteLine(numbers); //Writes collections of numbers
        }

        private static void Sample5()
        {
            IDictionary<string, string> dictionary = ExpressionExt.ToDictionary(Name => "Arek",
                                                                             Age => "23",
                                                                             LastCheck => DateTime.Now.ToShortDateString());
            var dictionaryName = dictionary["Name"];  //
            var anonPerson =
                dictionary.ToAnonymousType(
                    new
                    {
                        Name = default(string),
                        Age = default(int),
                        LastCheck = default(DateTime)
                    }); //Anonymous Type
        }

        private static void Sample4()
        {
            bool p = true;
            bool q = false;
            bool notP = p.Not();
            int boolResult = p.Ext_ToInt(); //if true return 1 else return 0
            if (p == true)
            {
                Console.WriteLine("TRUE");
            }
            else
            {
                Console.WriteLine("FALSE");
            }
            p.Do(() => Console.WriteLine("TRUE"), () => Console.WriteLine("FALSE"));
            LogicCheck(p, q);
        }

        private static void Sample3()
        {
            var persons = new[]
                              {
                                  new Person {FirstName = "Arek", LastName = "XYZ", Age = 23},
                                  new Person {FirstName = "David", LastName = "XYZ", Age = 19},
                              };
            var personsDataTable = persons.ToDataTable();
            ConsoleExt.WriteLine(personsDataTable);
            var personsOrderd = persons.OrderBy(x => x.Age); //Ordering by Age
            ConsoleExt.WriteLine(persons, x => new { x.FirstName, x.LastName });
        }

        private static void Sample1()
        {
            var number = 9.8F;
            var numberBetween = number.Ext_IsBetween(9, 10); //is Betwen 9, 10

            int? nullableInt = null;
            var nullEqual = nullableInt.HasValueAndEquals(1);

            char aChar = 'E';
            var vowel = aChar.IsVowel(); //true
            var charBeween = aChar.Ext_IsBetween('A', 'F');
            var charLimit1 = aChar.Ext_Limit('B'); //Return 'B'
            var charLimit2 = aChar.Ext_Limit('F'); //Return 'E'

            string guid = "4AAB7F67-A898-442a-884A-364411D584EC";
            var guidvalid = guid.IsValidGuid(); //Check if string is in GUID format

            string mail = "mymail@zzzzzzz.com";
            var mailValid = mail.IsValidEmailAddress(); //Check if string is in mail format
            mail.GetUsedChars(); //collection of chars {'m','y','a','i','l','z','c','o'}

            string nullString = null;
            nullString.IsNullOrEmpty(); //same as String.IsNullOrEmpty(nullString);

            string word = "Some words about Library";
            var wordRevors = word.Reverse(); // "yrarbiL tuoba sdrow emoS"
            var wordLast = word.GetRightSideOfString(13); //"about Library"

            DateTime.Now.IsWeekend();//
            DateTime.Now.Ext_IsBetween(DateTime.MinValue, DateTime.MaxValue);

            var ex = new Exception("0", new Exception("1", new Exception("2", new Exception("3"))));
            var innerEx = ex.GetMostInner();
        }

        private static void LogicCheck(bool p, bool q)
        {
            Console.WriteLine("p AND q <=> {0}", p.AND(q));
            Console.WriteLine("p OR q <=> {0}", p.OR(q));
            Console.WriteLine("p XOR q <=> {0}", p.XOR(q));
            Console.WriteLine("p IMP q <=> {0}", p.IMP(q));
            Console.WriteLine("p NAND q <=> {0}", p.NAND(q));
            Console.WriteLine("p EQ q <=> {0}", p.EQ(q));
        }
    }
}