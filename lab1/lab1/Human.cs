using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    class Human
    {
        public string Name{ get; set;}
        public string Surname { get; set; }
        public int Age { get; set; }

        public Human(string Name = "NoName", string Surname = "NoSurname", int Age = 0)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.Age = Age;
        }
    }
}
