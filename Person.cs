using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVersion2
{
    public abstract class Person
    {
        public string ID;
        public string name;
        public string surname;
        public int age;
        public string sex;
        public string email;
        public string phoneNumber;
        public string status;
        public string password;

        public Person(string _ID,string _name, string _surname, int _age, string _sex, string _email, string _phoneNumber, string _password)
        {
            this.ID = _ID;
            this.name = _name;
            this.surname = _surname;
            this.age = _age;
            this.sex = _sex;
            this.email = _email;
            this.phoneNumber = _phoneNumber;
            this.password = _password;
        }
        public abstract void Tostring();
        public abstract bool ConnectionCheck();

        public bool EqualPerson(Person obj) //this method enalbe comparison between two people. they are equals if their informations are the same.
        {
            bool same = true;
            if (obj.name != name || obj.surname != surname || obj.sex != sex || obj.ID != ID || obj.phoneNumber != phoneNumber || obj.age != age || obj.password != password || obj.status!=status || obj.email != email)
            {
                same = false;
            }
            return same;
        }
        //public abstract bool Equal(Person person);

    }
}
