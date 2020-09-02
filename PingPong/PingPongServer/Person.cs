using System;
using System.Collections.Generic;
using System.Text;

namespace PingPongServer
{
    public class Person
    {
        private string _name;
        private int _age;
        public Person(string name, int age)
        {
            _name = name;
            _age = age;
        }

        public override string ToString()
        {
            return $"{_name} is {_age} years old";
        }

    }
}
