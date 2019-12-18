using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MethodExamples
{
    class Program //Classes are reference types & Structs are value types.
    {
        public const string someText = "This is some text."; //Implicity static. Must be initialised.

        public readonly string someOtherText = "This is some other text"; //Not implicitly static. Cannot be used in program...
        public static readonly string someOtherStaticText = "This is some other text"; //This can be used in the program. Can be initialised at run time.

        public static readonly Person somePerson = new Person("A", "B"); //This will only work with readonly.
        static void Main(string[] args)
        {
            //CallExamples();
        }

        private static void CallExamples()
        {
            ListExample();
            ArrayExample();
            ForLoopExample();
            WhileLoopExample();
            DoWhileLoopExample();
            ForEachLoopExample();
            ValueTypeExample();
            ValueTypeExampleTwo();
            NullExample();
            ReadOnlyConstExample();
            ReadWriteToFile();
            DRYPrincipalBadExample();
            DRYPrincipalGoodExample();
        }

        #region Examples
        private static void ListExample()
        {
            List<int> myList = new List<int>(); //Dynamically adjusted - no initital capacity.

            Console.WriteLine(myList.Count());

            myList.Add(10);
            myList.Add(5);

            Console.WriteLine(myList.Count());
            Console.ReadLine();
        }

        private static void ArrayExample()
        {
            /*int[] myArray = new int[5]; //Needs an initial capacity.
            myArray[0] = 7;
            myArray[1] = 8;
            myArray[2] = 4;
            myArray[3] = 5;
            myArray[4] = 15;*/

            int[] myArray = { 7, 8, 4, 5, 15 };

            Array.Sort(myArray); // Will sort it from smallest to largest.
        }

        private static void ForLoopExample()
        {
            for (int i = 0; i <= 5; i++)
            {
                Console.WriteLine("This is line " + i);
            }
            Console.Read();
        }

        private static void WhileLoopExample() //Repeated code is bad practice.
        {
            Console.WriteLine("Please enter an input: ");
            var input = Console.ReadLine();
            Console.WriteLine("User input is: " + input);

            while (!input.Equals(string.Empty))
            {
                Console.WriteLine("Please enter an input: ");
                input = Console.ReadLine();
                Console.WriteLine("User input is: " + input);
            }
        }

        private static void DoWhileLoopExample()
        {
            var input = string.Empty;

            do
            {
                Console.WriteLine("Please enter an input: ");
                input = Console.ReadLine();
                Console.WriteLine("User input is: " + input);
            }
            while (!input.Equals(string.Empty));
        }

        private static void ForEachLoopExample()
        {
            int[] array = { 1, 5, 7, 9, 10 };

            Console.WriteLine("Using for each loop: ");
            foreach (int element in array)
            {
                Console.WriteLine(element);
            }

            //Same thing can be done with a for loop.
            Console.WriteLine("Using for loop: ");
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }

            //Again can be done with a while loop.
            Console.WriteLine("Using a while loop: ");
            int index = 0;
            while (index < array.Length)
            {
                Console.WriteLine(array[index]);
                index++;
            }
            Console.ReadLine();

        }

        private static void ReferenceTypeExample()
        {
            Person person = new Person("John", "Smith");

            ChangeName(person);

            Console.WriteLine(person.FirstName);
            Console.WriteLine(person.LastName);
            Console.ReadLine();
        } //Person custom type saved on heap.

        static void ChangeName(Person personToChange)
        {
            personToChange.FirstName = "Jane";
            personToChange.LastName = "Doe";
        }

        private static void ValueTypeExample() // PersonStruct custom type saved on stack. All value types are on stack. 
        {
            PersonStruct personStruct = new PersonStruct
            {
                FirstName = "John",
                LastName = "Smith"
            };

            ChangeNameStruct(personStruct);

            Console.WriteLine(personStruct.FirstName);
            Console.WriteLine(personStruct.LastName);
            Console.ReadLine();
        }

        static void ChangeNameStruct(PersonStruct personToChange)
        {
            personToChange.FirstName = "Jane";
            personToChange.LastName = "Doe";
        }

        static void ValueTypeExampleTwo() //Can use ref or out - if you don't initialize a value of "a", use out.
        {
            int a = 10;
            ChangeNumber(ref a);
            Console.WriteLine(a);
            Console.ReadLine();
        }

        static void ChangeNumber(ref int a)
        {
            a = 90;
        }

        private static void NullExample()
        {
            Person person = null; //Won't get a compile error, but will a null exception error at run time.

            Person newPerson = person ?? new Person("Default", "Person"); // ?? = null coalescing operator - if it's null, return default.

            Console.WriteLine(newPerson.FirstName);
        }

        private static void ReadOnlyConstExample()
        {

            Console.WriteLine(someText);
            Console.WriteLine(someOtherStaticText);
            Console.ReadLine();
        }

        private static void ReadWriteToFile()
        {
            string[] lines = { "This is the first line", "This is the second line", "This is the third line" };
            File.WriteAllLines("MyFirstFile.txt", lines);

            foreach (string line in File.ReadLines("MyFirstFile.txt"))
            {
                Console.WriteLine(line);
            }

            string[] fileContent = File.ReadAllLines("MyFirstFile.txt");
            foreach (string item in fileContent)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();

        }

        private static void DRYPrincipalBadExample()
        {
            string[] lines = { "First line", "Second line", "Third line" };
            string[] formattedLines = new string[lines.Length];

            for (int i = 0; i < formattedLines.Length; i++)
            {
                formattedLines[i] = String.Format("{0} {1} {2}", "---", lines[i], "---");
            }

            File.WriteAllLines("FormattedTextFile.txt", formattedLines);

            string[] otherLines = { "Another first line", "Another second line", "Another third line" };
            string[] otherFormattedLines = new string[otherLines.Length];

            for (int i = 0; i < otherFormattedLines.Length; i++)
            {
                otherFormattedLines[i] = String.Format("{0} {1} {2}", "---", otherLines[i], "---");
            }

            File.WriteAllLines("AnotherFormattedFile.txt", otherFormattedLines);
        }

        private static void DRYPrincipalGoodExample()
        {
            string[] lines = { "First line", "Second line", "Third line" };
            File.WriteAllLines("FormattedTextFile.txt", formatLines(lines));

            string[] otherLines = { "Another first line", "Another second line", "Another third line" };
            File.WriteAllLines("AnotherFormattedFile.txt", formatLines(otherLines));

            Console.ReadLine();
        }

        private static string[] formatLines(string[] unformattedLines)
        {
            string[] formattedLines = new string[unformattedLines.Length];

            for (int i = 0; i < unformattedLines.Length; i++)
            {
                formattedLines[i] = String.Format("{0} {1} {2}", "---", unformattedLines[i], "---");
            }
            return formattedLines;

        }

        #endregion
    }
}
