using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodExamples
{
    class Person
    {
        public Person(string argFirstName, string argLastName)
        {
            FirstName = argFirstName;
            LastName = argFirstName;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
