using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project_Team13
{
    public abstract class Person
    {
        public string ID;
        public string name;
        public string surname;
        public int age;
        public char sex;
        public string email;
        public string phoneNumber;
        public string status;

        public Person(string _ID,string _name, string _surname, int _age, char _sex, string _email, string _phoneNumber)
        {
            this.ID = _ID;
            this.name = _name;
            this.surname = _surname;
            this.age = _age;
            this.sex = _sex;
            this.email = _email;
            this.phoneNumber = _phoneNumber;
            
        }
        public abstract void Tostring();
        
    }
}
